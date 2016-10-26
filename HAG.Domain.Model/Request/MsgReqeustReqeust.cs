using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Domain.Model.Request
{
    public class MsgReqeustReqeust
    {
        /// <summary>
        /// 會員ID
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// 訊息標題
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 訊息內容
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// 對應任務ID
        /// </summary>
        public int MissionId { get; set; }

        /// <summary>
        /// 回應訊息ID
        /// </summary>
        public int Ref_MsgReqeustId { get; set; }

        /// <summary>
        /// 是否接受請求 [0] - 等待回復  [-1] - 拒絕  [1] - 接受
        /// </summary>
        public int Accept { get; set; }
    }
}
