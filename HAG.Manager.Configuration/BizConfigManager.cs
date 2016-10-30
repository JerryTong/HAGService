using Fox.Framework.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HAG.Manager.Configuration
{
    public class BizConfigManager
    {
        private static BizConfig m_BizConfig = null;

        public static BizConfig Current
        {
            get
            {
                if (m_BizConfig == null)
                {
                    m_BizConfig = ConfigAccessor.LoadConfig<BizConfig>("Biz.config");
                }

                return m_BizConfig;
            }
        }
    }

    [XmlRoot("bizconfiguration")]
    public class BizConfig
    {
        [XmlElement("registerEgg")]
        public int RegisterEgg { get; set; }
    }
}
