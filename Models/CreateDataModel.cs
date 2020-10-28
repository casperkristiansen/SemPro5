using Microsoft.Data.SqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemesterProject5.Models
{
    public class CreateDataModel
    {
        public int companyId { get; set; }
        public String topic { get; set; }
        public String degree { get; set; }
        public String description { get; set; }
        public String url { get; set; }

        public int SaveDetails()
        {
            NpgsqlConnection con = new NpgsqlConnection(GetConString.ConString());
            String query = "INSERT INTO public.Posts(companyId, topic, degree, description, imageURL) values ('" + companyId + "','" + topic + "','" + degree + "','" + description + "','" + url + "')";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }

    }
}
