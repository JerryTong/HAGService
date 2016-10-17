using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Service.Mission
{
    public class MissionBusiness
    {
        private MissionDataAccess missionDA = new MissionDataAccess();

        public MissionCreateResponse Create(MissionCreateRequest request)
        {
            if (request == null)
            {
                return new MissionCreateResponse
                {
                    StatusCode = HAG.Domain.Model.Enum.StatusCode.Illegal,
                    Message = "Request body was null."
                };
            }

            var missionInfo = EntityModelExtesion.EntityMissinInfo(request);
            var code = missionDA.InsertMission(missionInfo);

            if (code == 1)
            {
                return new MissionCreateResponse
                {
                    StatusCode = Domain.Model.Enum.StatusCode.Success
                };
            }

            return new MissionCreateResponse
            {
                StatusCode = Domain.Model.Enum.StatusCode.Failure,
                Message = string.Format("Insert Database Error. Error Code {0}", code)
            };
        }

        public MissionResponse Get(string missionIds)
        {
            if (string.IsNullOrEmpty(missionIds))
            {
                return null;
            }

            var result = new MissionResponse();
            result.MissionCollection = missionDA.GetMissionInfo(missionIds);

            return result;
        }
    }
}
