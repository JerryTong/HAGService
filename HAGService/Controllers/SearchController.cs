using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using HAG.Service.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HAGService.Controllers
{
    [EnableCors("*", "*", "*")]
    public class SearchController : ApiController
    {
        [HttpGet]
        [Route("api/search/search")]
        public SearchResponse Search([FromUri]SearchReqeust request)
        {
            return new SearchBusiness().Search(request);
        }
    }
}
