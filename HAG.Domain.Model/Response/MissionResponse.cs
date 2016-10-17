using HAG.Domain.Model.Mission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Domain.Model.Response
{
    public class MissionResponse
    {
        public List<MissionInfo> MissionCollection { get; set; }
    }
}
