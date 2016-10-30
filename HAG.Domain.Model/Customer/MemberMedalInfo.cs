using Fox.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Domain.Model.Customer
{
    public class MemberMedalInfo
    {
        public string MemberId { get; set; }

        public int MedalGroupId { get; set; }

        public int Score { get; set; }

        public bool Achieve { get; set; }

        public MedalInfo MedalInfo { get; set; }
    }
}
