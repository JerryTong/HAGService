using HAG.Domain.Model.Mission;
using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using HAG.Interface;
using HAG.Service.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Service.Mission
{
    public class MissionService : IMissionService
    {
        private MissionDataAccess missionDA = new MissionDataAccess();

        private readonly IAssistanceService assistanceService;
        public MissionService(IAssistanceService _assistanceService)
        {
            assistanceService = _assistanceService;
        }

        /// <summary>
        /// 建立任務
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MissionStatusResponse Create(MissionCreateRequest request)
        {
            if (request == null)
            {
                return new MissionStatusResponse
                {
                    Status = new ResponseStatus
                    {
                        StatusCode = HAG.Domain.Model.Enum.StatusCode.Illegal,
                        Message = "Request body was null."
                    }
                };
            }

            // 檢查會員星星是否足夠
            var starStatus = assistanceService.UpdateMemberStar(request.MemberId, 0 - request.Star);
            if(starStatus.StatusCode != 0)
            {
                // 星星不足 返回錯誤訊息
                return new MissionStatusResponse
                {
                    Status = starStatus
                };
            }

            var missionInfo = EntityModelExtesion.EntityMissinInfo(request);
            var response = missionDA.InsertMission(missionInfo);

            if (response != null)
            {
                //任務建立成功, 檢查有無使用道具
                if (!string.IsNullOrEmpty(request.PropIds) && request.PropIds == "[5001]")
                {
                    try
                    {
                        var response2 = new ShopService().UseEffect(new ShopUseEffectRequest
                        {
                            MemberId = request.MemberId,
                            MissionId = response.MissionId,
                            EffectId = 5001
                        });
                    }
                    catch (Exception ex)
                    {

                    }
                }

                return new MissionStatusResponse
                {
                    MissionId = response.MissionId,
                    MissionStatus = response.Status.ToString(),
                    
                    Status = new ResponseStatus
                    {
                        StatusCode = Domain.Model.Enum.StatusCode.Success
                    }
                };
            }

            return new MissionStatusResponse
            {
                Status = new ResponseStatus
                {
                    StatusCode = Domain.Model.Enum.StatusCode.Failure,
                    Message = string.Format("Insert Database Error. Error Code {0}", -1)
                }
            };
        }

        /// <summary>
        /// 獲取任務
        /// </summary>
        /// <param name="missionIds"></param>
        /// <returns></returns>
        public MissionResponse Get(string missionIds)
        {
            if (string.IsNullOrEmpty(missionIds))
            {
                return null;
            }

            var result = new MissionResponse();
            result.MissionCollection = missionDA.GetMissionInfo(missionIds);

            if (result.MissionCollection != null && result.MissionCollection.Count > 0)
            {
                var memberList = assistanceService.GetMemberListInfo(result.MissionCollection.Select(m => m.MemberId).ToList());
                if(memberList != null)
                {
                    foreach(var mission in result.MissionCollection)
                    {
                        mission.MemberInfo = memberList.FirstOrDefault(member => member.MemberId == mission.MemberId);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 任務完成 'R'
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MissionStatusResponse Start(MissionStatusRequest request)
        {
            if (request.MissionId == 0 || string.IsNullOrEmpty(request.MemberId))
            {
                return null;
            }

            var response = missionDA.UpdateMissionStatus(request.MissionId, request.MemberId, "R", request.SuperManId);

            return new MissionStatusResponse
            {
                MissionStatus = response.StatusCode == Domain.Model.Enum.StatusCode.Success ? "R" : string.Empty,
                // 當返回狀態更新成功時, Message將會輸出超人ID
                SuperManId = response.StatusCode == Domain.Model.Enum.StatusCode.Success ? response.Message : string.Empty,
                Status = response,
            };
        }

        /// <summary>
        /// 任務完成 'F'
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MissionStatusResponse Complete(MissionStatusRequest request)
        {
            if (request.MissionId == 0 || string.IsNullOrEmpty(request.MemberId))
            {
                return null;
            }

            var response = missionDA.UpdateMissionStatus(request.MissionId, request.MemberId, "F", request.SuperManId);
            return new MissionStatusResponse
            {
                MissionStatus = response.StatusCode == Domain.Model.Enum.StatusCode.Success ? "F" : string.Empty,
                Status = response,
            };
        }

        /// <summary>
        /// 任務刪除 'D'
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MissionStatusResponse Delete(MissionStatusRequest request)
        {
            if (request.MissionId == 0 || string.IsNullOrEmpty(request.MemberId))
            {
                return null;
            }

            var response = missionDA.UpdateMissionStatus(request.MissionId, request.MemberId, "D");
            return new MissionStatusResponse
            {
                MissionStatus = response.StatusCode == Domain.Model.Enum.StatusCode.Success ? "D" : string.Empty,
                Status = response,
            };
        }

        /// <summary>
        /// 任務評價
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MissionStatusResponse Evaluation(MissionEvaluationRequest request)
        {
            if (request.MissionId == 0 || string.IsNullOrEmpty(request.MemberId))
            {
                return null;
            }

            var response = missionDA.UpdateMemberRatingByMission(request.MissionId, request.MemberId, request.SuperManId, request.Evaluation);
            return new MissionStatusResponse
            {
                MissionStatus = "F",
                Status = response,
            };
        }
    }
}
