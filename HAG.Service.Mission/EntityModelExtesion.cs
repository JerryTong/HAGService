using HAG.Domain.Model.Mission;
using HAG.Domain.Model.Request;
using Fox.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Service.Mission
{
    public class EntityModelExtesion
    {
        public static MissionInfo EntityMissinInfo(MissionCreateRequest request)
        {
            var missionInfo = new MissionInfo
            {
                MemberId = request.MemberId,
                Title = request.Title,
                Description = request.Description,
                MissionType = request.MissionType,
                Address = request.Address,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                ZipCode = request.ZipCode,
                Star = request.Star,
                Contact = request.Contact
            };

            missionInfo.TaxesStar = (int)(missionInfo.Star * 0.2);
            missionInfo.TotalStar = missionInfo.Star + missionInfo.TaxesStar;

            return missionInfo;
        }
    }
}
