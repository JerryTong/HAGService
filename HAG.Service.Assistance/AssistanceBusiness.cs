using HAG.Domain.Model.Customer;
using HAG.Domain.Model.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Service.Assistance
{
    public class AssistanceBusiness
    {
        private AssistanceDataAcces assistanceDA = new AssistanceDataAcces();

        public List<MedalInfo> GetMedalInfo()
        {
            return assistanceDA.GetMedalInfo();
        }

        public List<EffectInfo> GetEffectInfo()
        {
            return assistanceDA.GetEffectInfo();
        }
    }
}
