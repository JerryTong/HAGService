using Fox.Framework.DataAccess;
using Fox.Framework.Entity;
using HAG.Domain.Model.Map;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Service.Search
{
    public class SearchDataAccess
    {
        public List<MapMakerInfo> GetMapMakerInfo(float latitude, float longitude, int maxSize, List<string> missionType)
        {
            var dataCommend = DataCommandAccessor.Get("SearchMission");

            using (SqlConnection connection = new SqlConnection(dataCommend.Environment.ConnectionString))
            {
                SqlCommand command = new SqlCommand(dataCommend.SqlCommend, connection);
                try
                {
                    connection.Open();
                    //command.Parameters.AddWithValue("@MaxSize", maxSize);
                    SqlCommandEntity.AddWithGroupValue(command, "MissionType", missionType);

                    var reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    return DataTableAccessor.ToCollection<MapMakerInfo>(dt);
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
