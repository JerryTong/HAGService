using Fox.Framework.DataAccess;
using HAG.Domain.Model.Response;
using HAG.Domain.Model.Shop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Service.Shop
{
    public class ShopDataAccess
    {
        /// <summary>
        /// 獲取道具列表
        /// </summary>
        /// <returns></returns>
        public List<EffectInfo> GetEffectList()
        {
            List<EffectInfo> effect = XmlDataAccessor.LoadCollectionWithTable<EffectInfo>("Effect.xml").ToList();
            return effect;
        }

        /// <summary>
        /// 使用道具
        /// </summary>
        /// <returns></returns>
        public ResponseStatus UseEffect(string memberId, int effectId, int count)
        {
            var dataCommend = DataCommandAccessor.Get("UpdateMemberEffect");

            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@MemberId", memberId);
                    command.Parameters.AddWithValue("@EffectId", effectId);
                    command.Parameters.AddWithValue("@Count", count);

                    var reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    var tmpInfo = DataTableAccessor.ToCollection<ResponseStatus>(dt);
                    return tmpInfo.First();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
            }

            return null;
        }
    }
}
