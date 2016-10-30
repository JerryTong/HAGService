using HAG.Domain.Model.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Domain.Model.Customer
{
    public class MemberEffectInfo
    {
        public string MemberId { get; set; }

        public int EffectId { get; set; }

        public int Count { get; set; }

        public EffectInfo EffectInfo { get; set; }
    }
}
