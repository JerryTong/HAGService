using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using HAG.Service.Mission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HAGService.Controllers
{
    [EnableCors("*", "*", "*")]
    public class MissionController : ApiController
    {
        [HttpPost]
        [Route("api/mission/create")]
        public MissionCreateResponse Create([FromBody] MissionCreateRequest request)
        {
            return new MissionBusiness().Create(request);
        }

        [HttpGet]
        [Route("api/mission/{missionIds}")]
        public MissionResponse GetMission([FromUri] string missionIds)
        {
            return new MissionBusiness().Get(missionIds);
        }
    }
}
