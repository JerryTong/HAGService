using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Domain.Model.Customer
{
    public class MedalInfo
    {
        public string MedalId { get; set; }

        public string MedalName { get; set; }

        public string MedalDescription { get; set; }

        public int MedalLimit { get; set; }

        public string Image { get; set; }

        public int Active { get; set; }

        public int MedalGroupId { get; set; }

        public int Priority { get; set; }
    }
}
