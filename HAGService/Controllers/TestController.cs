using Fox.Framework.DataAccess;
using Fox.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HAGService.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("api/test/test")]
        public string Test()
        {
            return "Service is okay.";
        }

        [HttpGet]
        [Route("api/test/testdb")]
        public string TestDB()
        {
            string response = string.Empty;
            var dataCommend = DataCommandAccessor.Get("TestDataCommand");
            
            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();
                    response = "DB Connection Success!";
                    
                }
                catch (Exception ex)
                {
                    response = "DB Connection unsuccess! " + ex.Message;
                }
                finally
                {
                    connection.Close();
                }
            }
            
            return response;
        }
    }
}
