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
                response.AddRange(memberMedalInfo);
            }

            response = response.OrderBy(m => m.Priority).ThenBy(m => m.MedalLimit).ToList();
            return response;
        }

        /// <summary>
        /// 獲取會員詳細資訊
        /// </summary>
        /// <param name="memberIds"></param>
        /// <returns></returns>
        public List<MemberInfo> GetMemberDetailInfo(List<string> memberIds)
        {

            return null;
        }

        /// <summary>
        /// 會員登入
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public int Login(string memberId, string email)
        {
            if(string.IsNullOrEmpty(memberId) && string.IsNullOrEmpty(email))
            {
                return -1;
            }

            return customerDA.Login(memberId, email);
        }
    }
}
