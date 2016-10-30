using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Domain.Model.Request
{
    public class MissionStatusRequest
    {
        public int MissionId { get; set; }

        public string MemberId { get; set; }

        public string SuperManId { get; set; }
    }
}
