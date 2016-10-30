using Fox.Framework.Entity;
using HAG.Domain.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Domain.Model.Response
{
    public class ResponseStatus
    {
        [DataMapping("Code")]
        public int DataCode
        {
            set
            {
                switch (value)
                {
                    case -2:
                        this.StatusCode = StatusCode.Illegal;
                        break;
                    case -1:
                        this.StatusCode = StatusCode.Failure;
                        break;
                    case 1:
                        this.StatusCode = StatusCode.Success;
                        break;
                    default:
                        this.StatusCode = StatusCode.Illegal;
                        break;
                }
            }
        }

        /// <summary>
        /// 接口狀態
        /// </summary>
        public StatusCode StatusCode { get; set; }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        [DataMapping("Message")]
        public string Message { get; set; }
    }
}
