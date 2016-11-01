using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Domain.Model.Request
{
    public class SearchReqeust
    {
        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public int ZoomLevel { get; set; }

        public string MissionType { get; set; }

        public int MaxSize { get; set; }

        public string Keyword { get; set; }
    }
}
