using HAG.Domain.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Domain.Model.Response
{
    public class MissionStatusResponse
    {
        /// <summary>
        /// 任務ID
        /// </summary>
        public int MissionId { get; set; }

        /// <summary>
        /// 任務狀態
        /// </summary>
        public string MissionStatus { get; set; }

        /// <summary>
        /// 已媒合超人ID
        /// </summary>
        public string SuperManId { get; set; }

        /// <summary>
        /// 任務是否可以開始
        /// </summary>
        public bool CanStart
        {
            get { return !string.IsNullOrEmpty(SuperManId); }
        }

        /// <summary>
        /// 任務是否可完成
        /// </summary>
        public bool CanComplete
        {
            get { return this.MissionStatus == "R"; }
        }

        /// <summary>
        /// 任務是否可刪除
        /// </summary>
        public bool CanDelete
        {
            get { return this.MissionStatus != "F" && this.MissionStatus != "D"; }
        }

        public ResponseStatus Status { get; set; }
    }
}
