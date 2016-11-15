using HAG.Domain.Model.MissionMessage;
using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using HAG.Entity;
using HAG.Service.Assistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Service.MsgReqeust
{
    public class MsgReqeustService
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

            if(response.MsgReqeustList != null)
            {
                AssistanceService assistanceBL = new AssistanceService();
                var memberList = response.MsgReqeustList.Select(m => m.MemberId).ToList();
                var memberListInfo = assistanceBL.GetMemberListInfo(memberList);
                var memberMedalInfo = assistanceBL.GetMemberMedalListInfo(memberList);

                if(memberListInfo != null && memberListInfo.Count > 0)
                {
                    response.MsgReqeustList.ForEach(m =>
                    {
                        m.MemberInfo = memberListInfo.FirstOrDefault(me => me.MemberId == m.MemberId);
                    });
                }

                if (memberMedalInfo != null && memberMedalInfo.Count > 0)
                {
                    response.MsgReqeustList.ForEach(m =>
                    {
                        if (m.MemberInfo != null)
                        {
                            m.MemberInfo.MemberMedalInfo = memberMedalInfo.ContainsKey(m.MemberId) ? memberMedalInfo[m.MemberId] : null;
                            m.MemberInfo.MemberMedalInfo = m.MemberInfo.MemberMedalInfo.OrderBy(mm => mm.Priority).ThenBy(mm => mm.MedalLimit).ToList();
                        }
                    });
                }
            }

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

            // 更新"請求ASK"訊息狀態
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

            MissionMsgReqesutResponse response = new MissionMsgReqesutResponse();
            response.StatusCode = Domain.Model.Enum.StatusCode.Success;
            response.MissionStatus = checkInfo.Status;

            return response;
        }

        /// <summary>
        /// 獲取未讀訊息數量
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public MessageNoticeResponse GetNoticeMsgReqeust(string memberId)
        {
            DateTime askDateTime = new DateTime(2000, 1, 1);
            DateTime answerDateTime = new DateTime(2000, 1, 1); 

            string askCache = RedisClient.GetValue("NoitcAsk_" + memberId);
            string answerCache = RedisClient.GetValue("NoitcAnswer_" + memberId);
            if (!string.IsNullOrEmpty(askCache))
            {
                askDateTime = DateTime.Parse(askCache);
            }

            if (!string.IsNullOrEmpty(answerCache))
            {
                answerDateTime = DateTime.Parse(answerCache);
            }

            MsgReqeustDataAccess msgReqeustDataAccess = new MsgReqeustDataAccess();
            var msgResponse = msgReqeustDataAccess.GetNoticeMsgRequest(memberId, askDateTime, answerDateTime);

            if(msgResponse == null || msgResponse.Count == 0)
            {
                return new MessageNoticeResponse();
            }

            var response = new MessageNoticeResponse();
            response.Help = msgResponse.Where(m => m.MessageType == 2).Count();
            response.Give = msgResponse.Where(m => m.MessageType == 1).Count();

            return response;
        }
    }
}
