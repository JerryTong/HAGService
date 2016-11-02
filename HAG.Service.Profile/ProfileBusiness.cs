using HAG.Domain.Model.Customer;
using HAG.Domain.Model.Response;
using HAG.Entity;
using HAG.Manager.Configuration;
using HAG.Service.Assistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Service.Profile
{
    public class ProfileBusiness
    {
        private ProfileDataAccess profileDA = new ProfileDataAccess();

        /// <summary>
        /// 獲取會員[發起的任務]
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public MissionResponse GetHelpMissionByMemberId(string memberId, List<string> status)
        {
            if (string.IsNullOrEmpty(memberId) || status == null || status.Count == 0)
            {
                return null;
            }

            var response = new MissionResponse();
            response.MissionCollection = profileDA.GetHelpMissionByMemberId(memberId, status);
            UpdateReadDateTime("NoitcAsk", memberId, DateTime.Now);

            return response;
        }

        /// <summary>
        /// 獲取會員[幫忙的任務]
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public MissionResponse GetGiveMissionByMemberId(string memberId, List<string> status)
        {
            if (string.IsNullOrEmpty(memberId) || status == null || status.Count == 0)
            {
                return null;
            }

            var response = new MissionResponse();
            response.MissionCollection = profileDA.GetGiveMissionByMemberId(memberId, status);
            UpdateReadDateTime("NoitcAnswer", memberId, DateTime.Now);

            return response;
        }

        /// <summary>
        /// 獲取會員獎章
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public List<MemberMedalInfo> GetProfileMemberMedalInfo(string memberId)
        {
            if (string.IsNullOrEmpty(memberId))
            {
                return null;
            }

            var medalInfoList = new AssistanceBusiness().GetMedalInfo();
            var memberMedalInfo = profileDA.GetProfileMemberMedalInfo(new List<string> { memberId });

            var response = new List<MemberMedalInfo>();
            if (memberMedalInfo != null && memberMedalInfo.Count > 0)
            {
                foreach (var memberMedal in memberMedalInfo)
                {
                    memberMedal.Achieve = memberMedal.Score >= memberMedal.MedalLimit;
                    response.Add(memberMedal);
                }
            }

            medalInfoList = medalInfoList.OrderBy(mm => mm.Priority).ThenBy(mm => mm.MedalLimit).ToList();
            medalInfoList.ForEach(m =>
            {
                if (!response.Any(r => r.MedalId == m.MedalId))
                {
                    response.Add(new MemberMedalInfo
                    {
                        MedalGroupId = m.MedalGroupId,
                        MemberId = memberId,
                        Score = 0,
                        MedalId = m.MedalId,
                        MedalLimit = m.MedalLimit,
                        MedalDescription = m.MedalDescription,
                        MedalName = m.MedalName,
                        Active = m.Active,
                        Image = "padlock.svg",
                        Reward = m.Reward,
                        Priority = m.Priority,
                    });
                }
            });
          
            response = response.OrderByDescending(r => r.Achieve).ToList();
            return response;
        }

        /// <summary>
        /// 獲取會員道具
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public List<MemberEffectInfo> GetMemberEffectInfo(string memberId)
        {
            if (string.IsNullOrEmpty(memberId))
            {
                return null;
            }

            var effectInfoList = new AssistanceBusiness().GetEffectInfo();
            var membereffectInfo = profileDA.GetMemberEffectInfo(memberId);

            var response = new List<MemberEffectInfo>();
            if (effectInfoList != null && effectInfoList.Count > 0)
            {
                foreach (var effect in effectInfoList)
                {
                    var tmpMemberEffect = membereffectInfo.Where(e => e.EffectId == effect.EffectId);

                    response.Add(new MemberEffectInfo
                    {
                        MemberId = memberId,
                        Count = tmpMemberEffect != null && tmpMemberEffect.Count() > 0 ? tmpMemberEffect.First().Count : 0,
                        EffectId = effect.EffectId,
                        EffectInfo = effect,
                    });

                }
            }

            return response;
        }

        private void UpdateReadDateTime(string type, string memberId, DateTime datetime)
        {
            RedisClient.SetValue(string.Format("{0}_{1}", type, memberId), datetime.AddHours(BizConfigManager.Current.ServerTime).ToString());
        }
    }
}
