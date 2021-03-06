﻿using HAG.Domain.Model.Request;
using HAG.Domain.Model.Response;
using HAG.Interface;
using HAG.Service.MsgReqeust;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HAGService.Controllers
{
    [EnableCors("*", "*", "*")]
    public class MessageController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IMsgReqeustService msgReqeustService;
        public MessageController(IMsgReqeustService _msgReqeustService)
        {
            msgReqeustService = _msgReqeustService;
        }

        /// <summary>
        /// 請求
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        [Route("api/request/ask")]
        public MissionMsgReqesutResponse Ask([FromBody]MsgReqeustReqeust request)
        {
            return msgReqeustService.SentMsgAskReqeust(request);
        }

        /// <summary>
        /// 回應
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        [Route("api/request/answer")]
        public MissionMsgReqesutResponse Answer([FromBody]MsgReqeustReqeust request)
        {
            return msgReqeustService.SentMsgAnswerReqeust(request);
        }

        /// <summary>
        /// 獲取指定任務下所有請求/回應
        /// </summary>
        /// <param name="missionId"></param>
        [HttpGet]
        [Route("api/request/{missionId}")]
        public MissionMsgReqesutResponse Messages(int missionId)
        {
            return msgReqeustService.GetMsgReqeustList(missionId);
        }

        [HttpGet]
        [Route("api/request/notice/{memberId}")]
        public MessageNoticeResponse Notic(string memberId)
        {
            return msgReqeustService.GetNoticeMsgReqeust(memberId);
        }
    }
}
