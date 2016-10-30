using HAG.Domain.Model.Customer;
using HAG.Domain.Model.Response;
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

            return response;
        }

        /// <summary>
        /// 獲取會員獎章
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public List<MemberMedalInfo> GetMemberMedalInfo(string memberId)
        {
            if (string.IsNullOrEmpty(memberId))
            {
                return null;
            }

            var medalInfoList = new AssistanceBusiness().GetMedalInfo();
            var memberMedalInfo = profileDA.GetMemberMedalInfo(new List<string> { memberId });

            var response = new List<MemberMedalInfo>();
            if (memberMedalInfo != null && memberMedalInfo.Count > 0)
            {
                medalInfoList.ForEach(m =>
                {
                    //檢查會員是否有此徽章積分
                    var tmpMemberMedal = memberMedalInfo.Where(mm => mm.MedalGroupId == m.MedalGroupId);
                    if(tmpMemberMedal != null && tmpMemberMedal.Count() > 0)
                    {
                        var tmp = tmpMemberMedal.First();
                        if (tmp.Score >= m.MedalLimit)
                        {
                            response.Add(new MemberMedalInfo
                            {
                                MedalGroupId = m.MedalGroupId,
                                MemberId = memberId,
                                Score = tmp.Score,
                                MedalInfo = new MedalInfo
                                {
                                    MedalId = m.MedalId,
                                    MedalLimit = m.MedalLimit,
                                    MedalDescription = m.MedalDescription,
                                    MedalGroupId = m.MedalGroupId,
                                    MedalName = m.MedalName,
                                    Active = m.Active,
                                    Image = m.Image
                                },
                            });
                        }
                    }
                    else
                    {
                        // 沒有此積分 設為0
                        response.Add(new MemberMedalInfo
                        {
                            MedalGroupId = m.MedalGroupId,
                            MemberId = memberId,
                            Score = 0,
                            MedalInfo = new MedalInfo
                            {
                                MedalId = m.MedalId,
                                MedalLimit = m.MedalLimit,
                                MedalDescription = m.MedalDescription,
                                MedalGroupId = m.MedalGroupId,
                                MedalName = m.MedalName,
                                Active = m.Active,
                                Image = "padlock.svg"
                            },
                        });
                    }
                });

                memberMedalInfo.ForEach(r =>
                {
                    var targetMedal = medalInfoList.Where(m => m.MedalGroupId == r.MedalGroupId);
                    if (targetMedal != null && targetMedal.Count() > 0)
                    {
                        targetMedal.ToList().ForEach(t =>
                        {
                            // 比分數
                            if (r.Score >= t.MedalLimit)
                            {
                                response.Add(new MemberMedalInfo
                                {
                                    MedalGroupId = r.MedalGroupId,
                                    MemberId = r.MemberId,
                                    Score = r.Score,
                                    Achieve = true,
                                    MedalInfo = new MedalInfo
                                    {
                                        MedalId = t.MedalId,
                                        MedalLimit = t.MedalLimit,
                                        MedalDescription = t.MedalDescription,
                                        MedalGroupId = t.MedalGroupId,
                                        MedalName = t.MedalName,
                                        Active = t.Active,
                                        Image = t.Image
                                    },
                                });
                            }
                        });
                    }
                });
            }

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
    }
}
