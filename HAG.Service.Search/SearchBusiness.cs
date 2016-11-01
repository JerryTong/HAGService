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
        private SearchDataAccess searchDA = new SearchDataAccess();
        public SearchResponse Search(SearchReqeust request)
        {
            SearchResponse response = new SearchResponse();
            response.MapMakers = GetMapMakerInfo(request.Latitude, request.Longitude, request.MaxSize, request.MissionType);
            response.TotalResult = response.MapMakers != null ? response.MapMakers.Count : 0;
            response.StatusCode = Domain.Model.Enum.StatusCode.Success;

            return response;
        }

        public SearchResponse SearchTest(SearchReqeust request)
        {
            SearchResponse response = new SearchResponse();
            response.MapMakers = GetMapMakerInfo(request.MaxSize);
            response.TotalResult = response.MapMakers.Count;
            response.StatusCode = Domain.Model.Enum.StatusCode.Success;

            return response;
        }

        public List<MapMakerInfo> GetMapMakerInfo(float Latitude, float Longitude, int maxSize, string missionType)
        {
            List<string> pameterMissionType = null;
            if (!string.IsNullOrEmpty(missionType))
            {
                pameterMissionType = missionType.Split(',').ToList();
            }
            else
            {
                pameterMissionType = new List<string>() { "1001", "1002", "1003", "1004", "1005" };
            }

            var response = searchDA.GetMapMakerInfo(Latitude, Longitude, maxSize, pameterMissionType);
            if (response == null)
            {
                return null;
            }

            response.ForEach(r =>
            {
                if(r.Highlight.AddMinutes(3) > DateTime.Now)
                {
                    r.IsHighlight = true;
                }
            });

            response = response.OrderBy(r => r.IsHighlight).Take(maxSize).ToList();

            return response;
        }

        private List<MapMakerInfo> GetMapMakerInfo(int maxSize)
        {
            //24.236182, 120.724037
            //    24.125003, 120.713217

            //    24.131523, 120.541127

            //    24.241394, 120.550109

            List<MapMakerInfo> result = new List<MapMakerInfo>();

            Random ranLat = new Random(Guid.NewGuid().GetHashCode());
            Random ranLon = new Random(Guid.NewGuid().GetHashCode());
            Random ranType = new Random(Guid.NewGuid().GetHashCode());
            Random ranHigh = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < maxSize; i++)
            {
                float tmpLatitude = (float)(24 + (ranLat.Next(125003, 241394) * 0.000001));
                float Longitude = (float)(120 + (ranLat.Next(541127, 724039) * 0.000001));

                result.Add(new MapMakerInfo
                {
                    Latitude = tmpLatitude,
                    Longitude = Longitude,
                    MissionId = 100000,
                    MissionType = ranType.Next(1, 4),
                    IsHighlight = ranHigh.Next(1, 10) > 4 ? true : false,
                    Priority = 1,
                });
            }

            result = result.OrderBy(r => r.IsHighlight).ToList();
            return result;
        }
    }
}
