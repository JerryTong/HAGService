using Fox.Framework.DataAccess;
using HAG.Domain.Model.Customer;
using HAG.Domain.Model.Request;
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
    }
}
