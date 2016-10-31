using Fox.Framework.DataAccess;
using Fox.Framework.Entity;
using HAG.Domain.Model.Customer;
using HAG.Domain.Model.Mission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Service.Profile
{
    public class ProfileDataAccess
    {
        /// <summary>
        /// 獲取會員[發起的任務] 
        /// </summary>
        /// <param name="missionIds"></param>
        /// <returns></returns>
        public List<MissionInfo> GetHelpMissionByMemberId(string memberId, List<string> status)
        {
            var dataCommend = DataCommandAccessor.Get("GetHelpMissionByMemberId");

            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@MemberId", memberId);
                    SqlCommandEntity.AddWithGroupValue(command, "Status", status);

                    var reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    return DataTableAccessor.ToCollection<MissionInfo>(dt);
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
        /// 獲取會員[接受的任務] 
        /// </summary>
        /// <param name="missionIds"></param>
        /// <returns></returns>
        public List<MissionInfo> GetGiveMissionByMemberId(string memberId, List<string> status)
        {
            var dataCommend = DataCommandAccessor.Get("GetGiveMissionByMemberId");

            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@MemberId", memberId);
                    SqlCommandEntity.AddWithGroupValue(command, "Status", status);

                    var reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    return DataTableAccessor.ToCollection<MissionInfo>(dt);
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
        /// 獲取會員獎章(含未獲得獎章)
        /// </summary>
        /// <param name="memberIds"></param>
        /// <returns></returns>
        public List<MemberMedalInfo> GetProfileMemberMedalInfo(List<string> memberIds)
        {
            var dataCommend = DataCommandAccessor.Get("GetProfileMemberMedalInfo");

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
        /// 獲取會員道具
        /// </summary>
        /// <param name="memberIds"></param>
        /// <returns></returns>
        public List<MemberEffectInfo> GetMemberEffectInfo(string memberId)
        {
            var dataCommend = DataCommandAccessor.Get("GetMemberEffectInfo");

            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@MemberId", memberId);
                    
                    var reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    return DataTableAccessor.ToCollection<MemberEffectInfo>(dt);
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
