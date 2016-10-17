using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Domain.Model.Request
{
    public class MissionCreateRequest
    {
        public int MemberId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int MissionType { get; set; }

        public string Address { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public string ZipCode { get; set; }

        public int Star { get; set; }

        public string Contact { get; set; }
    }
}
