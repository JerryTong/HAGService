using HAG.Domain.Model.Customer;
using HAG.Domain.Model.Response;
using HAG.Domain.Model.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Interface
{
    public interface IAssistanceService
    {
        List<MedalInfo> GetMedalInfo();

        List<EffectInfo> GetEffectInfo();

        /// <summary>
        /// 獲取多筆會員資訊
        /// </summary>
        /// <param name="memberIds"></param>
        /// <returns></returns>
        List<MemberInfo> GetMemberListInfo(List<string> memberIds);

        /// <summary>
        /// 獲取多筆會員獎章資訊
        /// </summary>
        /// <param name="memberIds"></param>
        /// <returns></returns>
        Dictionary<string, List<MemberMedalInfo>> GetMemberMedalListInfo(List<string> memberIds);

        /// <summary>
        /// 更新會員星星數
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="star"></param>
        /// <returns></returns>
        ResponseStatus UpdateMemberStar(string memberId, int star);
    }
}
