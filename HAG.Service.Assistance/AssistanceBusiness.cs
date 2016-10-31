using HAG.Domain.Model.Customer;
using HAG.Domain.Model.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Service.Assistance
{
    public class AssistanceBusiness
    {
        private AssistanceDataAcces assistanceDA = new AssistanceDataAcces();

        public List<MedalInfo> GetMedalInfo()
        {
            return assistanceDA.GetMedalInfo();
        }

        public List<EffectInfo> GetEffectInfo()
        {
            return assistanceDA.GetEffectInfo();
        }

        /// <summary>
        /// 獲取多筆會員資訊
        /// </summary>
        /// <param name="memberIds"></param>
        /// <returns></returns>
        public List<MemberInfo> GetMemberListInfo(List<string> memberIds)
        {
            return assistanceDA.GetMemberListInfo(memberIds);
        }

        /// <summary>
        /// 獲取多筆會員獎章資訊
        /// </summary>
        /// <param name="memberIds"></param>
        /// <returns></returns>
        public Dictionary<string, List<MemberMedalInfo>> GetMemberMedalListInfo(List<string> memberIds)
        {
            if(memberIds == null || memberIds.Count == 0)
            {
                return null;
            }

            var medalList = assistanceDA.GetMedalInfo();
            var memberMedalList = assistanceDA.GetMemberMedalListInfo(memberIds);

            Dictionary<string, List<MemberMedalInfo>> response = new Dictionary<string, List<MemberMedalInfo>>();
            if(memberMedalList != null && memberMedalList.Count > 0)
            {
                foreach(var member in memberIds)
                {
                    // 檢查會員是否有獎章積分
                    var tmpMemberMedalList = memberMedalList.Where(me => me.MemberId == member);
                    if (tmpMemberMedalList != null && tmpMemberMedalList.Count() > 0)
                    {
                        response.Add(member, new List<MemberMedalInfo>());
                        response[member] = tmpMemberMedalList.OrderBy(m => m.Priority).ThenBy(m => m.MedalLimit).ToList();
                    }
                }
            }

            return response;
        }
    }
}
