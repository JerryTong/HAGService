using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Domain.Model.Mission
{
    public class MissionInfo
    {
        public int MissionId { get; set; }

        public int MemberId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int MissionType { get; set; }

        //public int MissionSubType { get; set; }

        public string ZipCode { get; set; }

        //public string Country { get; set; }

        public string Address { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public int Star { get; set; }

        public int TotalStar { get; set; }

        public int TaxesStar { get; set; }

        //public string ImageList { get; set; }

        public string Contact { get; set; }

        //public DateTime MissionStartTime { get; set; }

        //public DateTime MissionEndTime { get; set; }

        public DateTime InDate { get; set; }

        public DateTime LastEdit { get; set; }

        public int IsBlock { get; set; }

        public DateTime HelpStartTime { get; set; }

        public DateTime ResolveTime { get; set; }
    }
}
