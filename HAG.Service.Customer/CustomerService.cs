using Autofac;
using HAG.Domain.Model.Customer;
using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using HAG.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Service.Customer
{
    public class CustomerService : ICustomerService
    {
        private CustomerDataAccess customerDA = new CustomerDataAccess();

        /// <summary>
        /// Assistance Service.
        /// </summary>
        private readonly IAssistanceService assistanceService;
        public CustomerService(IAssistanceService assistance)
        {
            assistanceService = assistance;
        }

        /// <summary>
        /// 會員註冊
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseStatus Register(MemberRegisterRequest request)
        {
            if(request == null 
                || (string.IsNullOrEmpty(request.MemberId) && string.IsNullOrEmpty(request.Email))
                || string.IsNullOrEmpty(request.Phone)
                || string.IsNullOrEmpty(request.Line)
                || string.IsNullOrEmpty(request.Name))
            {
                return new ResponseStatus
                {
                    StatusCode = Domain.Model.Enum.StatusCode.Failure,
                    Message = "欄位錯誤."
                };
            }

            // 使用 email 登入
            if (string.IsNullOrEmpty(request.MemberId) && !string.IsNullOrEmpty(request.Email))
            {
                request.MemberId = "100000" + new Random(Guid.NewGuid().GetHashCode()).Next(10000000, 99999999).ToString();
            }
            
            // 不為0表示無法註冊(已存在email or 存取錯誤)
            var existMember = customerDA.InternalLogin(request.Email);
            if(existMember != 1)
            {
                return new ResponseStatus
                {
                    StatusCode = Domain.Model.Enum.StatusCode.Failure,
                    Message = "信箱已註冊."
                };
            }

            int code = customerDA.Register(request);

            var response = new ResponseStatus();
            if (code == 1)
            {
                response = customerDA.RegisterMemberExtra(request);
            }
            else
            {
                return new ResponseStatus
                {
                    StatusCode = Domain.Model.Enum.StatusCode.Failure,
                    Message = "Register Error."
                };
            }

            return response;
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

            var medalInfoList = assistanceService.GetMedalInfo();
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
        public MemberInfo Login(string memberId, string email)
        {
            if(string.IsNullOrEmpty(memberId) && string.IsNullOrEmpty(email))
            {
                return null;
            }

            return customerDA.Login(memberId, email);
        }
    }
}
