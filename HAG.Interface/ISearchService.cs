using HAG.Domain.Model.Map;
using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using System;
using System.Collections.Generic;

namespace HAG.Interface
{
    public interface ISearchService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Latitude"></param>
        /// <param name="Longitude"></param>
        /// <param name="maxSize"></param>
        /// <param name="missionType"></param>
        /// <returns></returns>
        List<MapMakerInfo> GetMapMakerInfo(float Latitude, float Longitude, int maxSize, string missionType);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        SearchResponse Search(SearchReqeust request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        SearchResponse SearchTest(SearchReqeust request);
    }
}
