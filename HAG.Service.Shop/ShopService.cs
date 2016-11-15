using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using HAG.Domain.Model.Shop;
using HAG.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Service.Shop
{
    public class ShopService : IShopService
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

            var response = shopDA.BuyEffect(request.MemberId, request.EffectId, 1);
            return response;
        }

        public ResponseStatus UseEffect(ShopUseEffectRequest request)
        {
            if (string.IsNullOrEmpty(request.MemberId))
            {
                return null;
            }

            var response = new ResponseStatus();
            if (request.EffectId == 5001)
            {
                var code = shopDA.UseEffect(request.MissionId, request.MemberId, request.EffectId);
                if(code == 1)
                {
                    response.StatusCode = Domain.Model.Enum.StatusCode.Success;
                }
                else
                {
                    response.StatusCode = Domain.Model.Enum.StatusCode.Failure;
                }
            }
            else
            {
                response.StatusCode = Domain.Model.Enum.StatusCode.Illegal;
                response.Message = "NOT WORK.";
            }
            
            return response;
        }
    }
}
