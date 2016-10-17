using System;
using System.Collections.Generic;
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
    }
}
