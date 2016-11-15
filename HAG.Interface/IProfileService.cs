using HAG.Domain.Model.Customer;
using HAG.Domain.Model.Response;
using System;
using System.Collections.Generic;

namespace HAG.Interface
{
    public interface IProfileService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        MissionResponse GetGiveMissionByMemberId(string memberId, List<string> status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        MissionResponse GetHelpMissionByMemberId(string memberId, List<string> status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        List<MemberEffectInfo> GetMemberEffectInfo(string memberId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        List<MemberMedalInfo> GetProfileMemberMedalInfo(string memberId);
    }
}
