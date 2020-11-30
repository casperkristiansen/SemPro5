using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Npgsql;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SemesterProject5.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace SemesterProject5.Controllers
{
    public class HomeController : Controller
    {
        NpgsqlCommand com = new NpgsqlCommand();
        NpgsqlDataReader dr;
        NpgsqlConnection con = new NpgsqlConnection();
        List<Post> posts = new List<Post>();
        List<CompanyID> companys = new List<CompanyID>();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            con.ConnectionString = GetConString.ConString();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Reviews()
        {
            return View();
        }
        public IActionResult Search()
        {
            FetchData();
            return View(posts);
        }
        private void FetchData()
        {
            if (posts.Count > 0)
            {
                posts.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT * FROM public.posts NATURAL JOIN company;";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    posts.Add(new Post()
                    {
                        CompanyName = dr["name"].ToString()
                    ,
                        Topic = dr["topic"].ToString()
                    ,
                        Degree = dr["degree"].ToString()
                    ,
                        Description = dr["description"].ToString()
                    ,
                        Email = dr["email"].ToString()
                    ,
                        Web = dr["weblink"].ToString()
                    ,
                        Date = dr["my_date"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void FetchID()
        {
            if (companys.Count > 0)
            {
                companys.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT * FROM public.UserCompany;";
                dr = com.ExecuteReader();
                while(dr.Read())
                {
                    companys.Add(new CompanyID()
                    {
                        CompanyId = dr["CompanyID"].ToString()
                        ,
                        userId = dr["UserID"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = "Company, Admin")]
        public ActionResult Create()
        {
            FetchID();
            return View(companys);
        }

        [HttpPost]
        public IActionResult GetDetails()
        {
            CreateDataModel umodel = new CreateDataModel();
            umodel.companyId = Convert.ToInt32(HttpContext.Request.Form["txtCompanyId"]);
            umodel.topic = HttpContext.Request.Form["txtTopic"].ToString();
            umodel.degree = HttpContext.Request.Form["txtDegree"].ToString();
            umodel.description = HttpContext.Request.Form["txtDescription"].ToString();
            int result = umodel.SaveDetails();
            if (result > 0)
            {
                ViewBag.Result = "Data Saved Successfully";
            }
            else
            {
                ViewBag.Result = "Something Went Wrong";
            }
            return View("Create");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
