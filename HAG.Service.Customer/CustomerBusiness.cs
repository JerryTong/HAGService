using HAG.Domain.Model.Customer;
using HAG.Domain.Model.Request;
using HAG.Service.Assistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Service.Customer
{
    public class CustomerBusiness
    {
        private CustomerDataAccess customerDA = new CustomerDataAccess();

        /// <summary>
        /// 會員註冊
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int Register(MemberRegisterRequest request)
        {
            if(request == null || string.IsNullOrEmpty(request.MemberId))
            {
                return -1;
            }

            int code = customerDA.Register(request);

            if(code == 1)
            {
                code = customerDA.RegisterMemberExtra(request);
            }

            return code;
        }

        /// <summary>
        /// 獲取會員資訊
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public MemberInfo GetMemberBaseInfo(string memberId)
        {
            if (string.IsNullOrEmpty(memberId))
            {
                return null;
            }

            return customerDA.GetMemberBaseInfo(memberId);
        }

        /// <summary>
        /// 獲取會員獎章
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public List<MemberMedalInfo> GetMebmerMedalInfo(string memberId)
        {
            if (string.IsNullOrEmpty(memberId))
            {
                return null;
            }

            var medalInfoList = new AssistanceBusiness().GetMedalInfo();
            var memberMedalInfo = customerDA.GetMemberMedalInfo(new List<string> { memberId });

            var response = new List<MemberMedalInfo>();
            if (memberMedalInfo != null && memberMedalInfo.Count > 0)
            {
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

            return response;
        }
    }
}
