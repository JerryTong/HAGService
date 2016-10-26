using HAG.Domain.Model.MissionMessage;
using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using HAG.Service.Mission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Service.MsgReqeust
{
    public class MsgReqeustBusiness
    {
        /// <summary>
        /// 獲取任務 訊息列表
        /// </summary>
        /// <param name="missionId"></param>
        /// <returns></returns>
        public MissionMsgReqesutResponse GetMsgReqeustList(int missionId)
        {
            MsgReqeustDataAccess msgReqeustDataAccess = new MsgReqeustDataAccess();
            var msgList = msgReqeustDataAccess.GetMsgRequestAll(missionId);

            MissionMsgReqesutResponse response = new MissionMsgReqesutResponse();
            response.StatusCode = Domain.Model.Enum.StatusCode.Success;
            if (msgList == null || msgList.Count == 0)
            {
                return response;
            }

            // 任務訊息組合
            var askMsg = msgList.Where(m => m.MessageType == 1);
            askMsg.ToList().ForEach(a =>
            {
                var tmp = msgList.Where(m => m.ParentMessageId == a.MessageId); 
                if(tmp != null)
                {
                    a.MsgAnswerInfo = tmp.ToList();
                }
            });

            response.MsgReqeustList = askMsg.ToList();

            return response;
        }

        /// <summary>
        /// 提交任務幫忙請求
        /// </summary>
        /// <param name="missionId"></param>
        /// <returns></returns>
        public MissionMsgReqesutResponse SentMsgAskReqeust(MsgReqeustReqeust request)
        {
            MsgReqeustDataAccess msgReqeustDataAccess = new MsgReqeustDataAccess();
            var checkInfo = msgReqeustDataAccess.GetCheckMissionMsgAsk(request.MissionId, request.MemberId);

            if (checkInfo == null)
            {
                return new MissionMsgReqesutResponse
                {
                    StatusCode = Domain.Model.Enum.StatusCode.Illegal,
                    Message = string.Format("Unknow Error. MissionId: {0}", request.MissionId)
                };
            }

            if (checkInfo.Code != 1)
            {
                MissionMsgReqesutResponse errorResponse = new MissionMsgReqesutResponse();
                errorResponse.StatusCode = Domain.Model.Enum.StatusCode.Failure;
                errorResponse.MissionStatus = checkInfo.Status;

                switch (checkInfo.Code)
                {
                    case -2:
                        errorResponse.Message = string.Format("Not Found Mission id: {0}", request.MissionId);
                        break;
                    case -1:
                        errorResponse.Message = string.Format("Mission status was not allow. Mission id: {0}", request.MissionId);
                        break;
                    case 9:
                        errorResponse.Message = string.Format("Has been Send. Mission id: {0}", request.MissionId);
                        break;
                }

                return errorResponse;
            }

            // 允許發送請求, 執行寫入
            MissionMessageInfo missionMsg = new MissionMessageInfo
            {
                MemberId = request.MemberId,
                MessageType = 1,
                MessageTitle = request.Title,
                MessageDetail = request.Detail,
                MissionId = request.MissionId,
            };

            int code =msgReqeustDataAccess.InsertMsgReqeustAsk(missionMsg);

            if(code != 1)
            {
                return new MissionMsgReqesutResponse
                {
                    MissionStatus = checkInfo.Status,
                    StatusCode = Domain.Model.Enum.StatusCode.Failure,
                    Message = "Insert Error.",
                };
            }

            MissionMsgReqesutResponse response = new MissionMsgReqesutResponse();
            response.StatusCode = Domain.Model.Enum.StatusCode.Success;
            response.MissionStatus = checkInfo.Status;

            return response;
        }

        public MissionMsgReqesutResponse SentMsgAnswerReqeust(MsgReqeustReqeust request)
        {
            MsgReqeustDataAccess msgReqeustDataAccess = new MsgReqeustDataAccess();
            var checkInfo = msgReqeustDataAccess.GetCheckMissionMsgAnswer(request.MissionId, request.Ref_MsgReqeustId);
            if (checkInfo == null)
            {
                return new MissionMsgReqesutResponse
                {
                    StatusCode = Domain.Model.Enum.StatusCode.Illegal,
                    Message = string.Format("Unknow Error. MissionId: {0}", request.MissionId)
                };
            }

            // 當發送'接受'請求時, 需檢查任務狀態是否為'W'
            if (request.Accept == 1)
            {
                if (checkInfo.Code != 1)
                {
                    MissionMsgReqesutResponse errorResponse = new MissionMsgReqesutResponse();
                    errorResponse.StatusCode = Domain.Model.Enum.StatusCode.Failure;
                    errorResponse.MissionStatus = checkInfo.Status;

                    switch (checkInfo.Code)
                    {
                        case -2:
                            errorResponse.Message = string.Format("Not Found Mission id: {0}", request.MissionId);
                            break;
                        case -1:
                            errorResponse.Message = string.Format("Mission status was not allow. Mission id: {0}", request.MissionId);
                            break;
                        case 8:
                            errorResponse.Message = string.Format("Has been response. Mission id: {0} / Message Id", request.MissionId, request.Ref_MsgReqeustId);
                            break;
                    }

                    return errorResponse;
                }
            }

            // 允許發送請求, 執行寫入
            MissionMessageInfo missionMsg = new MissionMessageInfo
            {
                MemberId = request.MemberId,
                MessageType = 2,
                MessageTitle = request.Title,
                MessageDetail = request.Detail,
                MissionId = request.MissionId,
                ParentMessageId = request.Ref_MsgReqeustId,
                Accept = request.Accept,
            };

            int code = msgReqeustDataAccess.InsertMsgReqeustAsk(missionMsg);
            if (code != 1)
            {
                return new MissionMsgReqesutResponse
                {
                    MissionStatus = checkInfo.Status,
                    StatusCode = Domain.Model.Enum.StatusCode.Failure,
                    Message = "Insert Error.",
                };
            }

            // 更新請求訊息狀態
            int updateCode = msgReqeustDataAccess.UpdateMsgReqeustAsk(request.Ref_MsgReqeustId, request.Accept);
            if (code != 1)
            {
                return new MissionMsgReqesutResponse
                {
                    MissionStatus = checkInfo.Status,
                    StatusCode = Domain.Model.Enum.StatusCode.Failure,
                    Message = "Update Message Error.",
                };
            }


            if (request.Accept == 1)
            {
                // 更新任務狀態 'A'
                int updateMissionCode = msgReqeustDataAccess.UpdateMissionStatus(request.MissionId, "A");
                if (code != 1)
                {
                    return new MissionMsgReqesutResponse
                    {
                        MissionStatus = checkInfo.Status,
                        StatusCode = Domain.Model.Enum.StatusCode.Failure,
                        Message = "Update mission status Error.",
                    };
                }
            }

            MissionMsgReqesutResponse response = new MissionMsgReqesutResponse();
            response.StatusCode = Domain.Model.Enum.StatusCode.Success;
            response.MissionStatus = checkInfo.Status;

            return response;
        }
    }
}
