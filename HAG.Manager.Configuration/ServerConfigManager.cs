using Fox.Framework.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HAG.Manager.Configuration
{
    public class ServerConfigManager
    {
        public static ServerConfig m_ServerConfig = null;

        public static ServerConfig Current
        {
            get
            {
                if(m_ServerConfig == null)
                {
                    m_ServerConfig = ConfigAccessor.LoadConfig<ServerConfig>("Server.config");
                }

                return m_ServerConfig;
            }
        }
    }

    [XmlRoot("serverconfiguration")]
    public class ServerConfig
    {
        [XmlElement("redis")]
        public string RedisServer { get; set; }
    }
}
