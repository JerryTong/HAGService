using Fox.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Domain.Model.Customer
{
    public class MemberInfo
    {
        public string MemberId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Line { get; set; }

        public string Image { get; set; }

        [DataMapping("InDate")]
        public DateTime RegisterDate { get; set; }

        public int Star { get; set; }

        public int Good { get; set; }

        public int Bad { get; set; }

        public int Login { get; set; }

        public List<MemberMedalInfo> MemberMedalInfo { get; set; }

        public bool IsBest { get; set; }
    }
}
