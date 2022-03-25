using Domain.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Data.repos
{
    public class EwonRepo
    {
        public void AddEwon(Ewon ewon)
        {
            string connectionString = @"Data Source=SRV-SKYSPARK\SQLEXPRESS;Initial Catalog=Trevi;Integrated Security=True";
            string query = "INSERT INTO Ewons(Name,Description)" + "VALUES(@Name,@Description)";

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 80).Value = ewon.name;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar, 128).Value = ewon.description;

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

            }
        }
    }
}
