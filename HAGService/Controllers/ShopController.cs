using HAG.Domain.Model.Shop;
using HAG.Service.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HAGService.Controllers
{
    public class ShopController : ApiController
    {
        [HttpGet]
        [Route("api/shop/effect")]
        public List<EffectInfo> EffectList()
        {
            return new ShopBusiness().GetEffectList();
        }
    }
}
