using Fox.Framework.DataAccess;
using Fox.Framework.Entity;
using HAG.Domain.Model.MissionMessage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Service.MsgReqeust
{
    public class MsgReqeustDataAccess
    {
        /// <summary>
        /// 檢查是否可發出Ask 請求.
        /// </summary>
        /// <param name="missionId"></param>
        /// <param name="memberId"></param>
        public CheckMissionInfo GetCheckMissionMsgAsk(int missionId, string memberId)
        {
            var dataCommend = DataCommandAccessor.Get("CheckMissionMsgAsk");

            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@MissionId", missionId);
                    command.Parameters.AddWithValue("@MemberId", memberId);

                    var reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    var tmpInfo = DataTableAccessor.ToCollection<CheckMissionInfo>(dt);
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

        /// <summary>
        /// 檢查是否可回復Ask 請求.
        /// </summary>
        /// <param name="missionId"></param>
        /// <param name="memberId"></param>
        public CheckMissionInfo GetCheckMissionMsgAnswer(int missionId, int missionMessageId)
        {
            var dataCommend = DataCommandAccessor.Get("CheckMissionMsgAnswer");

            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@MissionId", missionId);
                    command.Parameters.AddWithValue("@MissionMessageId", missionMessageId);

                    var reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    var tmpInfo = DataTableAccessor.ToCollection<CheckMissionInfo>(dt);
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

        /// <summary>
        /// 獲取Ask / Answer msg.
        /// </summary>
        /// <param name="missionId"></param>
        /// <returns></returns>
        public List<MissionMessageInfo> GetMsgRequestAll(int missionId)
        {
            var dataCommend = DataCommandAccessor.Get("GetMsgReqeustList");

            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@MissionId", missionId);

                    var reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    return DataTableAccessor.ToCollection<MissionMessageInfo>(dt);
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
        /// 寫入Ask Msg.
        /// </summary>
        /// <param name="reqInfo"></param>
        /// <returns></returns>
        public int InsertMsgReqeustAsk(MissionMessageInfo reqInfo)
        {
            var dataCommend = DataCommandAccessor.Get("InsertMsgReqeustAsk");

            int rowsAffected;
            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@MemberId", reqInfo.MemberId);
                    command.Parameters.AddWithValue("@MessageType", reqInfo.MessageType);
                    command.Parameters.AddWithValue("@MessageTitle", reqInfo.MessageTitle);
                    command.Parameters.AddWithValue("@MessageDetail", reqInfo.MessageDetail);
                    command.Parameters.AddWithValue("@MissionId", reqInfo.MissionId);
                    command.Parameters.AddWithValue("@ParentMessageId", reqInfo.ParentMessageId);

                    rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    rowsAffected = -1;
                }
                finally
                {
                    connection.Close();
                }
            }

            return rowsAffected;
        }

        /// <summary>
        /// 寫入Ask Msg.
        /// </summary>
        /// <param name="reqInfo"></param>
        /// <returns></returns>
        public int InsertMsgReqeustAnswer(MissionMessageInfo reqInfo)
        {
            var dataCommend = DataCommandAccessor.Get("InsertMsgReqeustAnswer");

            int rowsAffected;
            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@MemberId", reqInfo.MemberId);
                    command.Parameters.AddWithValue("@MessageType", reqInfo.MessageType);
                    command.Parameters.AddWithValue("@MessageTitle", reqInfo.MessageTitle);
                    command.Parameters.AddWithValue("@MessageDetail", reqInfo.MessageDetail);
                    command.Parameters.AddWithValue("@MissionId", reqInfo.MissionId);
                    command.Parameters.AddWithValue("@ParentMessageId", reqInfo.ParentMessageId);

                    rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    rowsAffected = -1;
                }
                finally
                {
                    connection.Close();
                }
            }

            return rowsAffected;
        }

        /// <summary>
        /// 更新Ask 狀態.
        /// </summary>
        /// <param name="reqInfo"></param>
        /// <returns></returns>
        public int UpdateMsgReqeustAsk(int missionMessageId, int accept)
        {
            var dataCommend = DataCommandAccessor.Get("UpdateMissionMsgAsk");

            int rowsAffected;
            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@Accept", accept);
                    command.Parameters.AddWithValue("@MissionMessageId", missionMessageId);

                    rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    rowsAffected = -1;
                }
                finally
                {
                    connection.Close();
                }
            }

            return rowsAffected;
        }
        
        /// <summary>
        /// 獲取特定時間點後的消息(未讀訊息)
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<MissionMessageInfo> GetNoticeMsgRequest(string memberId, DateTime askDate, DateTime answerDate)
        {
            var dataCommend = DataCommandAccessor.Get("GetNoticeMsgReqeust");

            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@MemberId", memberId);
                    command.Parameters.AddWithValue("@AskDate", askDate);
                    command.Parameters.AddWithValue("@AnswerDate", answerDate);

                    var reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    return DataTableAccessor.ToCollection<MissionMessageInfo>(dt);
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
