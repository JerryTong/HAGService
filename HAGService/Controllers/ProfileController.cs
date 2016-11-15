using HAG.Domain.Model.Customer;
using HAG.Domain.Model.Response;
using HAG.Service.Profile;
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
    public class ProfileController : ApiController
    {
        [HttpGet]
        [Route("api/profile/help/{memberId}/active")]
        public MissionResponse GetHelpMissionByMemberIdActive([FromUri] string memberId)
        {
            return new ProfileService().GetHelpMissionByMemberId(memberId, new List<string>() { "W", "R" });
        }

        [HttpGet]
        [Route("api/profile/help/{memberId}/complete")]
        public MissionResponse GetHelpMissionByMemberIdComplete([FromUri] string memberId)
        {
            return new ProfileService().GetHelpMissionByMemberId(memberId, new List<string>() { "F", "D" });
        }

        [HttpGet]
        [Route("api/profile/give/{memberId}/active")]
        public MissionResponse GetGiveMissionByMemberIdActive([FromUri] string memberId)
        {
            return new ProfileService().GetGiveMissionByMemberId(memberId, new List<string>() { "W", "R" });
        }

        [HttpGet]
        [Route("api/profile/give/{memberId}/complete")]
        public MissionResponse GetGiveMissionByMemberIdComplete([FromUri] string memberId)
        {
            return new ProfileService().GetGiveMissionByMemberId(memberId, new List<string>() { "F", "D" });
        }

        [HttpGet]
        [Route("api/profile/medal/{memberId}")]
        public List<MemberMedalInfo> GetMemberMedalInfo([FromUri] string memberId)
        {
            return new ProfileService().GetProfileMemberMedalInfo(memberId);
        }

        [HttpGet]
        [Route("api/profile/effect/{memberId}")]
        public List<MemberEffectInfo> GetMemberEffectInfo([FromUri] string memberId)
        {
            return new ProfileService().GetMemberEffectInfo(memberId);
        }
    }
}
