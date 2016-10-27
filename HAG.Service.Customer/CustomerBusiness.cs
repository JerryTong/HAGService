using HAG.Domain.Model.Customer;
using HAG.Domain.Model.Request;
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
    }
}
