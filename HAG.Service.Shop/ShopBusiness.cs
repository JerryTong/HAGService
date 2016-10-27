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
    }
}
