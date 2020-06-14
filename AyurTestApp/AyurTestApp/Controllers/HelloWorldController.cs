using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace AyurTestApp.Controllers
{
    public class HelloWorldController : Controller
    {
        static SqlConnection sqlConnHomeDAL;

        public HelloWorldController()
        {
            sqlConnHomeDAL = new SqlConnection(Startup.ConnectionString);
        }

        public string Index()
        {
            Welcome();
            return "This is my default action...";
        }

        // 
        // GET: /HelloWorld/Welcome/ 

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
                        countdb= (int)sqlCmd.ExecuteScalar();
                        sqlConnHomeDAL.Close();
                    }
                }
                return "This is the Welcome action method..."+ "number of databases: "+ countdb.ToString();
            }
            catch (Exception e)
            {
                return e.Message;
            }

           
        }
    }
}
