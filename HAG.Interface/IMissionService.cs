using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using System;

namespace HAG.Interface
{
    public interface IMissionService
    {
        /// <summary>
        /// 任務完成
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MissionStatusResponse Complete(MissionStatusRequest request);

        /// <summary>
        /// 創建任務
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MissionStatusResponse Create(MissionCreateRequest request);

        /// <summary>
        /// 刪除任務
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MissionStatusResponse Delete(MissionStatusRequest request);

        /// <summary>
        /// 評價
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MissionStatusResponse Evaluation(MissionEvaluationRequest request);

        /// <summary>
        /// 獲取任務
        /// </summary>
        /// <param name="missionIds"></param>
        /// <returns></returns>
        MissionResponse Get(string missionIds);

        /// <summary>
        /// 任務開始
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MissionStatusResponse Start(MissionStatusRequest request);
    }
}
