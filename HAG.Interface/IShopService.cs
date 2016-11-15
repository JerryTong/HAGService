using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using HAG.Domain.Model.Shop;
using System;
using System.Collections.Generic;

namespace HAG.Interface
{
    public interface IShopService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ResponseStatus BuyEffect(ShopByEffectRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<EffectInfo> GetEffectList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ResponseStatus UseEffect(ShopUseEffectRequest request);
    }
}
