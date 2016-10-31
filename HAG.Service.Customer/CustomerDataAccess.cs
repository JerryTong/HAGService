using Fox.Framework.DataAccess;
using Fox.Framework.Entity;
using HAG.Domain.Model.Customer;
using HAG.Domain.Model.Request;
using HAG.Manager.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Service.Customer
{
    public class CustomerDataAccess
    {
        /// <summary>
        /// 註冊會員
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int Register(MemberRegisterRequest request)
        {
            var dataCommend = DataCommandAccessor.Get("RegisterMember");

            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@MemberId", request.MemberId);
                    command.Parameters.AddWithValue("@Name", request.Name);
                    command.Parameters.AddWithValue("@Description", request.Description);
                    command.Parameters.AddWithValue("@Phone", request.Phone);
                    command.Parameters.AddWithValue("@Line", request.Line);
                    command.Parameters.AddWithValue("@Email", request.Email);
                    command.Parameters.AddWithValue("@Image", request.Image);

                    int rowsAffected = command.ExecuteNonQuery();

                    connection.Close();
                    return rowsAffected;
                }
                catch (Exception ex)
                {
                    connection.Close();
                }
            }

            return -1;
        }

        public int Login(string memberId, string email)
        {
            var dataCommend = DataCommandAccessor.Get("GetMemberLogin");

            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@MemberId", memberId);
                    command.Parameters.AddWithValue("@Email", email);

                    var reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    var tmp = DataTableAccessor.ToCollection<MemberInfo>(dt)[0];
                    if(tmp == null)
                    {
                        return -1;
                    }

                    return 1;
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

            return -1;
        }
        
        /// <summary>
        /// 建構會員相關基礎表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int RegisterMemberExtra(MemberRegisterRequest request)
        {
            var dataCommend = DataCommandAccessor.Get("CreateHAGMemberExtra");

            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@MemberId", request.MemberId);
                    command.Parameters.AddWithValue("@Star", BizConfigManager.Current.RegisterEgg);

                    int rowsAffected = command.ExecuteNonQuery();

                    connection.Close();
                    return rowsAffected;
                }
                catch (Exception ex)
                {
                    connection.Close();
                }
            }

            return -1;
        }

        /// <summary>
        /// GetMemberBaseInfo
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public MemberInfo GetMemberBaseInfo(string memberId)
        {
            var dataCommend = DataCommandAccessor.Get("GetMemberBaseInfo");

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

                    return DataTableAccessor.ToCollection<MemberInfo>(dt)[0];
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
        public List<MemberMedalInfo> GetMemberMedalInfo(List<string> memberIds)
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
    }
}
