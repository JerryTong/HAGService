using HAG.Manager.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Entity
{
    public class RedisClient
    {
        private static ConnectionMultiplexer conn;
        private static object myLock = new object();
        public static void RedisCacheProvider()
        {
            if(conn == null)
            {
                lock (myLock)
                {
                    conn = ConnectionMultiplexer.Connect(ServerConfigManager.Current.RedisServer);
                }
            }
        }

        public static void SetValue(string key, string value)
        {
            RedisCacheProvider();

            IDatabase cache = conn.GetDatabase();
            cache.StringSet(key, value);
        }

        public static string GetValue(string key)
        {
            RedisCacheProvider();

            IDatabase cache = conn.GetDatabase();
            return cache.StringGet(key);
        }
    }
}
