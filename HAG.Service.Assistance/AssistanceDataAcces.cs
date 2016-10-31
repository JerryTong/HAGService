using Fox.Framework.DataAccess;
using Fox.Framework.Entity;
using HAG.Domain.Model.Customer;
using HAG.Domain.Model.Shop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Service.Assistance
{
    public class AssistanceDataAcces
    {
        /// <summary>
        /// 獲取獎章
        /// </summary>
        /// <returns></returns>
        public List<MedalInfo> GetMedalInfo()
        {
            var dataCommend = DataCommandAccessor.Get("GetMedalInfo");

            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();
                    
                    var reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    return DataTableAccessor.ToCollection<MedalInfo>(dt);
                }
                catch (Exception ex)
                {
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }
            }

            return null;
        }

        /// <summary>
        /// 獲取會員獎章積分
        /// </summary>
        /// <param name="memberIds"></param>
        /// <returns></returns>
        public List<MemberMedalInfo> GetMemberMedalListInfo(List<string> memberIds)
        {
            var dataCommend = DataCommandAccessor.Get("GetMemberMedalInfo");

            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();
                    SqlCommandEntity.AddWithGroupValue(command, "MemberIds", memberIds);

                    var reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    return DataTableAccessor.ToCollection<MemberMedalInfo>(dt);
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

        /// <summary>
        /// 獲取多筆會員基本資訊
        /// </summary>
        /// <param name="memberIds"></param>
        /// <returns></returns>
        public List<MemberInfo> GetMemberListInfo(List<string> memberIds)
        {
            var dataCommend = DataCommandAccessor.Get("GetMemberBaseListInfo");

            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();
                    SqlCommandEntity.AddWithGroupValue(command, "MemberIds", memberIds);

                    var reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    return DataTableAccessor.ToCollection<MemberInfo>(dt);
                }
                catch (Exception ex)
                {
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }
            }

            return null;
        }

        /// <summary>
        /// 獲取道具
        /// </summary>
        /// <returns></returns>
        public List<EffectInfo> GetEffectInfo()
        {
            var dataCommend = DataCommandAccessor.Get("GetEffectInfo");

            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();

                    var reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    return DataTableAccessor.ToCollection<EffectInfo>(dt);
                }
                catch (Exception ex)
                {
                    connection.Close();
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
