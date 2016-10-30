using HAG.Domain.Model.Customer;
using HAG.Domain.Model.Request;
using HAG.Entity;
using HAG.Service.Customer;
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
    public class CustomerController : ApiController
    {
        /// <summary>
        /// 會員註冊 返回1表示成功.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/member/register")]
        public int Register(MemberRegisterRequest request)
        {
            //RedisClient.SetValue("keyt", "test");
            //string a = RedisClient.GetValue("keyt");

            return new CustomerBusiness().Register(request);
        }

        [HttpGet]
        [Route("api/member/{memberId}")]
        public MemberInfo GetMemberInfo([FromUri] string memberId)
        {
            return new CustomerBusiness().GetMemberBaseInfo(memberId);
        }

        [HttpGet]
        [Route("api/member/medal/{memberId}")]
        public List<MemberMedalInfo> GetMemberMedalInfo([FromUri] string memberId)
        {
            return new CustomerBusiness().GetMebmerMedalInfo(memberId);
        }
    }
}
