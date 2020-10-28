using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SemesterProject5.Models;

namespace SemesterProject5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Search()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult GetDetails()
        {
            CreateDataModel umodel = new CreateDataModel();
            umodel.companyId = Convert.ToInt32(HttpContext.Request.Form["txtCompanyId"]);
            umodel.topic = HttpContext.Request.Form["txtTopic"].ToString();
            umodel.degree = HttpContext.Request.Form["txtDegree"].ToString();
            umodel.description = HttpContext.Request.Form["txtDescription"].ToString();
            umodel.url = HttpContext.Request.Form["urlPicture"].ToString();
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
