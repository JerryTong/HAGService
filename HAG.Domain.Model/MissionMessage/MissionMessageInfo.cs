using Fox.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Domain.Model.MissionMessage
{
    public class MissionMessageInfo
    {
        [DataMapping("TransactionNumber")]
        public int MessageId { get; set; }

        public int MemberId { get; set; }

        /// <summary>
        /// MessageType: [0]-Message, [1]-MissionAsk, [2]-MissionAnswer
        /// </summary>
        public int MessageType { get; set; }

        public string MessageTitle { get; set; }

        public string MessageDetail { get; set; }

        public DateTime InDate { get; set; }

        public DateTime LastEdit { get; set; }

        public int MissionId { get; set; }

        public bool Active { get; set; }

        /// <summary>
        /// Accept: [-1]: Refuse, [0]: Unknow, [1]: Accept
        /// </summary>
        public int Accept { get; set; }

        public int ParentMessageId { get; set; }

        public List<MissionMessageInfo> MsgAnswerInfo { get; set; }
    }
}
