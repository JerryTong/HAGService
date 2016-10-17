using Fox.Framework.DataAccess;
using HAG.Domain.Model.Mission;
using HAG.Domain.Model.Request;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAG.Domain.Model.Response;
using System.Data;
using Fox.Framework.Entity;

namespace HAG.Service.Mission
{
    public class MissionDataAccess
    {
        public int InsertMission(MissionInfo missionInfo)
        {
            var dataCommend = DataCommandAccessor.Get("CreateHAGMember");

            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@MemberId", missionInfo.MemberId);
                    command.Parameters.AddWithValue("@Title", missionInfo.Title);
                    command.Parameters.AddWithValue("@Description", missionInfo.Description);
                    command.Parameters.AddWithValue("@MissionType", missionInfo.MissionType);
                    command.Parameters.AddWithValue("@ZipCode", missionInfo.ZipCode);
                    command.Parameters.AddWithValue("@Address", missionInfo.Address);
                    command.Parameters.AddWithValue("@Latitude", missionInfo.Latitude);
                    command.Parameters.AddWithValue("@Longitude", missionInfo.Latitude);           
                    command.Parameters.AddWithValue("@TotalStar", missionInfo.TotalStar);
                    command.Parameters.AddWithValue("@TaxesStar", missionInfo.TaxesStar);
                    command.Parameters.AddWithValue("@Star", missionInfo.Star);
                    command.Parameters.AddWithValue("@Contact", missionInfo.Contact);
                    
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

        public List<MissionInfo> GetMissionInfo(string missionIds)
        {
            var dataCommend = DataCommandAccessor.Get("GetHAGMissionById");
            List<string> missionIdList = missionIds.Split(',').ToList();

            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();

                    SqlCommandEntity.AddWithGroupValue(command, "MissionIds", missionIdList);
                    
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
    }
}
