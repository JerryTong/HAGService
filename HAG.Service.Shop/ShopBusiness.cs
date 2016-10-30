using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using HAG.Domain.Model.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Service.Shop
{
    public class ShopBusiness
    {
        private ShopDataAccess shopDA = new ShopDataAccess();

        /// <summary>
        /// 獲取道具列表
        /// </summary>
        /// <returns></returns>
        public List<EffectInfo> GetEffectList()
        {
            return shopDA.GetEffectList();
        }

        /// <summary>
        /// 購買道具
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseStatus BuyEffect(ShopByEffectRequest request)
        {
            if (string.IsNullOrEmpty(request.MemberId))
            {
                return null;
            }

            var response = shopDA.UseEffect(request.MemberId, request.EffectId, 1);
            return response;
        }
    }
}
