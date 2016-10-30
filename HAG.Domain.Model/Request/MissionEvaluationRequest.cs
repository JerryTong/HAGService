using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Domain.Model.Request
{
    public class MissionEvaluationRequest : MissionStatusRequest
    {
        public int Evaluation { get; set; }
    }
}
