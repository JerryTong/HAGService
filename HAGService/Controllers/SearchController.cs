using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using HAG.Interface;
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
        /// <summary>
        /// Search Service.
        /// </summary>
        private readonly ISearchService searchService;
        public SearchController(ISearchService _searchService)
        {
            searchService = _searchService;
        }

        [HttpGet]
        [Route("api/search/searchtest")]
        public SearchResponse SearchTest([FromUri]SearchReqeust request)
        {
            return searchService.SearchTest(request);
        }

        [HttpGet]
        [Route("api/search/search")]
        public SearchResponse Search([FromUri]SearchReqeust request)
        {
            return searchService.Search(request);
        }
    }
}
