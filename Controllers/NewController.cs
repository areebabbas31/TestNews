using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;


namespace Test.Controllers
{
    public class NewController : Controller
    {
        private readonly string connectionString = "Server=localhost,1433;Database=master;User Id=sa;Password=Qaz@xsw12;Encrypt=False;TrustServerCertificate=True";


        [HttpGet("/new")]
        public IActionResult Index()
        {
            var databases = new List<string>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT NewId FROM dbo.News", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        databases.Add(reader.GetString(0));
                    }
                }
            }

            // Pass list to view
            return View(databases);
        }
    }
}
