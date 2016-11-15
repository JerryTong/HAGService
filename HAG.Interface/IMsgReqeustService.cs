using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using System;

namespace HAG.Interface
{
    public interface IMsgReqeustService
    {
        /// <summary>
        /// 獲取任務請求列表
        /// </summary>
        /// <param name="missionId"></param>
        /// <returns></returns>
        MissionMsgReqesutResponse GetMsgReqeustList(int missionId);

        /// <summary>
        /// 獲取訊息即時提示
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        MessageNoticeResponse GetNoticeMsgReqeust(string memberId);

        /// <summary>
        /// 發送任務接受請求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MissionMsgReqesutResponse SentMsgAnswerReqeust(MsgReqeustReqeust request);

        /// <summary>
        /// 發送任務幫忙請求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MissionMsgReqesutResponse SentMsgAskReqeust(MsgReqeustReqeust request);
    }
}
