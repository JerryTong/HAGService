using HAG.Domain.Model.Mission;
using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using HAG.Service.Mission;
using HAG.Interface;
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
        /// <summary>
        /// 
        /// </summary>
        private readonly IMissionService missionService;
        public MissionController(IMissionService _missionService)
        {
            missionService = _missionService;
        }
     
        [HttpPost]
        [Route("api/mission/create")]
        public MissionStatusResponse Create([FromBody] MissionCreateRequest request)
        {
            return missionService.Create(request);
        }

        [HttpGet]
        [Route("api/mission/{missionIds}")]
        public MissionResponse GetMission([FromUri] string missionIds)
        {
            return missionService.Get(missionIds);
        }

        [HttpPost]
        [Route("api/mission/start")]
        public MissionStatusResponse Start([FromBody] MissionStatusRequest request)
        {
            return missionService.Start(request);
        }

        [HttpPost]
        [Route("api/mission/complete")]
        public MissionStatusResponse Complete([FromBody] MissionStatusRequest request)
        {
            return missionService.Complete(request);
        }

        [HttpPost]
        [Route("api/mission/delete")]
        public MissionStatusResponse Delete([FromBody] MissionStatusRequest request)
        {
            return missionService.Delete(request);
        }

        [HttpPost]
        [Route("api/mission/helper/evaluation")]
        public MissionStatusResponse Evaluation([FromBody] MissionEvaluationRequest request)
        {
            return missionService.Evaluation(request);
        }
    }
}
