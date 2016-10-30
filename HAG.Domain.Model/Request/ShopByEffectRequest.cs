using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Domain.Model.Request
{
    public class ShopByEffectRequest
    {
        public string MemberId { get; set; }

        public int EffectId { get; set; }

        public int Count { get; set; }
    }
}
