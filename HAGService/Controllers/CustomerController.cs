using Autofac;
using HAG.Domain.Model.Customer;
using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using HAG.Entity;
using HAG.Interface;
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
        private readonly ICustomerService customerService;
        public CustomerController(ICustomerService _customerService)
        {
            customerService = _customerService;
        }

        /// <summary>
        /// 會員註冊 返回1表示成功.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/member/register")]
        public ResponseStatus Register(MemberRegisterRequest request)
        {
            //RedisClient.SetValue("keyt", "test");
            //string a = RedisClient.GetValue("keyt");

            return customerService.Register(request);
        }

        /// <summary>
        /// 會員登入
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/member/login")]
        public MemberInfo Login(string email, string password)
        {
            return customerService.Login(email, password);
        }

        [HttpGet]
        [Route("api/member/{memberId}")]
        public MemberInfo GetMemberInfo([FromUri] string memberId)
        {
            return customerService.GetMemberBaseInfo(memberId);
        }

        [HttpGet]
        [Route("api/member/medal/{memberId}")]
        public List<MemberMedalInfo> GetMemberMedalInfo([FromUri] string memberId)
        {
            return customerService.GetMebmerMedalInfo(memberId);
        }
    }
}
