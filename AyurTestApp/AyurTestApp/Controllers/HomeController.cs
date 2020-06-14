using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AyurTestApp.Models;
using System.Data.SqlClient;
using System.Data;

namespace AyurTestApp.Controllers
{
    public class HomeController : Controller
    {
      
        private readonly ILogger<HomeController> _logger;

        static SqlConnection sqlConnHomeDAL;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Welcome();
            return View();
        }

        public IActionResult Privacy()
        {



            var text = "<h1>Hello, Response!</h1>";
            byte[] data = System.Text.Encoding.UTF8.GetBytes(text);
            Response.Body.WriteAsync(data, 0, data.Length);
            return View();
            

        }

        public string Welcome()
        {
            int countdb = 0;
            try
            {
                using (sqlConnHomeDAL = new SqlConnection(Startup.ConnectionString))
                {
                    string sqlQuery = "SELECT COUNT(*) FROM SYS.DATABASES";
                    using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConnHomeDAL))
                    {
                        sqlConnHomeDAL.Open();
                        sqlCmd.CommandType = CommandType.Text;
                        countdb = (int)sqlCmd.ExecuteScalar();
                        sqlConnHomeDAL.Close();
                    }
                }
                return "This is the Welcome action method..." + "number of databases: " + countdb.ToString();
            }
            catch (Exception e)
            {
                return e.Message;
            }


        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
