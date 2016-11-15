using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using HAG.Domain.Model.Shop;
using HAG.Interface;
using HAG.Service.Assistance;
using HAG.Service.Shop;
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
    public class ShopController : ApiController
    {
        /// <summary>
        /// Search Service.
        /// </summary>
        private readonly IShopService shopService;
        public ShopController(IShopService _shopService)
        {
            shopService = _shopService;
        }

        [HttpGet]
        [Route("api/shop/effect")]
        public List<EffectInfo> EffectList()
        {
            return shopService.GetEffectList();
        }

        [HttpPost]
        [Route("api/shop/effect/buy")]
        public ResponseStatus BuyEffect([FromBody] ShopByEffectRequest request)
        {
            return shopService.BuyEffect(request);
        }

        [HttpPost]
        [Route("api/shop/effect/Use")]
        public ResponseStatus UseEffect([FromBody] ShopUseEffectRequest request)
        {
            return shopService.UseEffect(request);
        }
    }
}
