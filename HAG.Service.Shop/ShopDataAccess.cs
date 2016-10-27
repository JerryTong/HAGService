using Fox.Framework.DataAccess;
using HAG.Domain.Model.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Service.Shop
{
    public class ShopDataAccess
    {
        /// <summary>
        /// 獲取道具列表
        /// </summary>
        /// <returns></returns>
        public List<EffectInfo> GetEffectList()
        {
            List<EffectInfo> effect = XmlDataAccessor.LoadCollectionWithTable<EffectInfo>("Effect.xml").ToList();
            return effect;
        }
    }
}
