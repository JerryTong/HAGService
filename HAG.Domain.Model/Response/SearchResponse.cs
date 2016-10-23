using HAG.Domain.Model.Enum;
using HAG.Domain.Model.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Domain.Model.Response
{
    public class SearchResponse
    {
        public List<MapMakerInfo> MapMakers { get; set; }

        public int TotalResult { get; set; }

        public StatusCode StatusCode { get; set; }
    }
}
