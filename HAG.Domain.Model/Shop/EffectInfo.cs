using Fox.Framework.Entity;
using HAG.Domain.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HAG.Domain.Model.Shop
{
    public class EffectInfo
    {
        public int EffectId { get; set; }

        public string EffectName { get; set; }

        public string EffectDescription { get; set; }

        public int EffectMinute { get; set; }

        [DataMapping("EffectType")]
        public int InternalEffectType
        {
            set
            {
                switch (value)
                {
                    case 1:
                        this.ReadEffectType = EffectType.MissionOnly;
                        break;
                    case 2:
                        this.ReadEffectType = EffectType.Immediately;
                        break;
                    default:
                        this.ReadEffectType = EffectType.MissionOnly;
                        break;
                }
            }
        }

        public EffectType ReadEffectType { get; set; }

        public string Image { get; set; }

        public int Cost { get; set; }

        public bool Active { get; set; }
    }
}
