using HAG.Domain.Model.Mission;
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
        public MissionStatusResponse Create([FromBody] MissionCreateRequest request)
        {
            return new MissionBusiness().Create(request);
        }

        [HttpGet]
        [Route("api/mission/{missionIds}")]
        public MissionResponse GetMission([FromUri] string missionIds)
        {
            return new MissionBusiness().Get(missionIds);
        }

        [HttpPost]
        [Route("api/mission/start")]
        public MissionStatusResponse Start([FromBody] MissionStatusRequest request)
        {
            return new MissionBusiness().Start(request);
        }

        [HttpPost]
        [Route("api/mission/complete")]
        public MissionStatusResponse Complete([FromBody] MissionStatusRequest request)
        {
            return new MissionBusiness().Complete(request);
        }

        [HttpPost]
        [Route("api/mission/delete")]
        public MissionStatusResponse Delete([FromBody] MissionStatusRequest request)
        {
            return new MissionBusiness().Delete(request);
        }

        [HttpPost]
        [Route("api/mission/helper/evaluation")]
        public MissionStatusResponse Evaluation([FromBody] MissionEvaluationRequest request)
        {
            return new MissionBusiness().Evaluation(request);
        }
    }
}
