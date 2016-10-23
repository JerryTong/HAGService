using HAG.Domain.Model.Map;
using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Service.Search
{
    public class SearchBusiness
    {
        public SearchResponse Search(SearchReqeust request)
        {
            SearchResponse response = new SearchResponse();
            response.MapMakers = GetMapMakerInfo();
            response.TotalResult = response.MapMakers.Count;
            response.StatusCode = Domain.Model.Enum.StatusCode.Success;

            return response;
        }

        private List<MapMakerInfo> GetMapMakerInfo()
        {
            //24.236182, 120.724037
            //    24.125003, 120.713217

            //    24.131523, 120.541127

            //    24.241394, 120.550109

            List<MapMakerInfo> result = new List<MapMakerInfo>();

            Random ranLat = new Random();
            Random ranLon = new Random();
            Random ranType = new Random();
            for (int i = 0; i < 500; i++)
            {
                float tmpLatitude = (float)(24 + (ranLat.Next(125003, 241394) * 0.000001));
                float Longitude = (float)(120 + (ranLat.Next(541127, 724039) * 0.000001));

                result.Add(new MapMakerInfo
                {
                    Latitude = tmpLatitude,
                    Longitude = Longitude,
                    MissionId = 100000,
                    MissionType = ranType.Next(1, 4),
                    Priority = 1,
                });
            }

            return result;
        }
    }
}
