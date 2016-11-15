using HAG.Domain.Model.Customer;
using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using System.Collections.Generic;

namespace HAG.Interface
{
    public interface ICustomerService
    {
        /// <summary>
        /// 會員註冊
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ResponseStatus Register(MemberRegisterRequest request);

        /// <summary>
        /// 獲取會員資訊
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        MemberInfo GetMemberBaseInfo(string memberId);

        /// <summary>
        /// 獲取會員獎章
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        List<MemberMedalInfo> GetMebmerMedalInfo(string memberId);

        /// <summary>
        /// 獲取會員詳細資訊
        /// </summary>
        /// <param name="memberIds"></param>
        /// <returns></returns>
        List<MemberInfo> GetMemberDetailInfo(List<string> memberIds);

        /// <summary>
        /// 會員登入
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        MemberInfo Login(string memberId, string email);
    }
}
