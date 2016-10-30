﻿using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using HAG.Domain.Model.Shop;
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
            return new ShopBusiness().GetEffectList();
        }

        [HttpPost]
        [Route("api/shop/effect/buy")]
        public ResponseStatus Buy([FromBody] ShopByEffectRequest request)
        {
            return new ShopBusiness().BuyEffect(request);
        }
    }
}
