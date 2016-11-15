using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using HAG.Domain.Model.Shop;
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
        [HttpGet]
        [Route("api/shop/effect")]
        public List<EffectInfo> EffectList()
        {
            return new AssistanceService().GetEffectInfo();
        }

        [HttpPost]
        [Route("api/shop/effect/buy")]
        public ResponseStatus BuyEffect([FromBody] ShopByEffectRequest request)
        {
            return new ShopService().BuyEffect(request);
        }

        [HttpPost]
        [Route("api/shop/effect/Use")]
        public ResponseStatus UseEffect([FromBody] ShopUseEffectRequest request)
        {
            return new ShopService().UseEffect(request);
        }
    }
}
