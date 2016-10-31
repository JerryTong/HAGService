using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Domain.Model.Map
{
    public class MapMakerInfo
    {
        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public int MissionType { get; set; }

        public int MissionId { get; set; }

        public int Priority { get; set; }

        public bool IsHighlight { get; set; }

        public DateTime Highlight { get; set; }
    }
}
