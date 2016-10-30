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
