using HAG.Domain.Model.Enum;
using HAG.Domain.Model.MissionMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Domain.Model.Response
{
    public class MissionMsgReqesutResponse
    {
        /// <summary>
        /// 任務狀態 W:Wait, A:已媒合解決中, F:任務已完成, D:任務已過期或刪除
        /// </summary>
        public char MissionStatus { get; set; }

        public List<MissionMessageInfo> MsgReqeustList { get; set; }

        public StatusCode StatusCode { get; set; }

        public string Message { get; set; }
    }
}
