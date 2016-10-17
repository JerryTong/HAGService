using HAG.Domain.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Domain.Model.Response
{
    public class MissionCreateResponse
    {
        public StatusCode StatusCode { get; set; }

        public string Message { get; set; }
    }
}
