using Domain.Data;
using Domain.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Data.repos
{
    public class TagRepo : ITagRepo
    {
       private string connectionString = @"Data Source=SRV-SKYSPARK\sqlexpress;Initial Catalog=toon;Integrated Security=True;Pooling=False";

        private SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        public IReadOnlyList<DateTime> getAllDatesByTagName(string tagName)
        {
            string query = "SELECT TimeStamp from MBR where TAG=@TAG";
            List<DateTime> values = new List<DateTime>();
            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@TAG";
                paramId.DbType = DbType.String;
                paramId.Value = tagName;
                command.Parameters.Add(paramId);
                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        DateTime value = (DateTime)reader["TimeStamp"];
                        values.Add(value);
                    }
                    return values.AsReadOnly();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public List<Tag> getAllValuesByTagName(string tagName, DateTime date)
        {
            Tag t;
            string query = "SELECT * from MBR where TAG=@TAG AND TimeStamp=@TimeStamp";
            List<Tag> values = new List<Tag>();
            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@TAG";
                paramId.DbType = DbType.String;
                paramId.Value = tagName;
                command.Parameters.Add(paramId);

                SqlParameter paramDate = new SqlParameter();
                paramDate.ParameterName = "@TimeStamp";
                paramDate.DbType = DbType.DateTime;
                paramDate.Value = date;
                command.Parameters.Add(paramDate);

                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string value = (string)reader["Value"];
                        DateTime d = (DateTime)reader["TimeStamp"];
                        t = new Tag(tagName, d, value);
                        values.Add(t);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
                return values;
            }
        }

        public  SqlConnection GetConnection()
        {
            string connectionString = @"Data Source=SRV-SKYSPARK\sqlexpress;Initial Catalog=toon;Integrated Security=True;Pooling=False";

            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }






        public void AddTagNameToDb(Tag tag)
        {
            string connectionString = @"Data Source=SRV-SKYSPARK\sqlexpress;Initial Catalog=toon;Integrated Security=True;Pooling=False";
            string query = "INSERT INTO MBR_TAGNAMES (TagName)" + "VALUES(@TagName)";

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.Add("@TagName", SqlDbType.VarChar, 128).Value = tag.name;


                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

            }
        }


        public void AddTag(Tag tag)
        {
            string connectionString = @"Data Source=SRV-SKYSPARK\sqlexpress;Initial Catalog=toon;Integrated Security=True;Pooling=False";
            string query = "INSERT INTO MBR (TimeStamp,TAG,Value,Description,TagType)" + "VALUES(@TimeStamp,@TAG,@Value,@Description,@TagType)";

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.Add("@TimeStamp", SqlDbType.DateTime, 80).Value = tag.time;
                cmd.Parameters.Add("@TAG", SqlDbType.VarChar, 128).Value = tag.name;
                cmd.Parameters.Add("@Value", SqlDbType.VarChar, 128).Value = tag.value;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar, 512).Value = tag.description;
                cmd.Parameters.Add("@TagType", SqlDbType.VarChar, 128).Value = tag.tagType;

                cn.Open();


                cmd.ExecuteNonQuery();



                cn.Close();

            }
        }
        public IReadOnlyList<Tag> getAllTags()
        {
            string connectionString = @"Data Source=SRV-SKYSPARK\sqlexpress;Initial Catalog=toon;Integrated Security=True;Pooling=False";
            string query = "SELECT * FROM MBR";
            List<Tag> tags = new List<Tag>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                try
                {
                    cn.Open();
                    cmd.CommandText = query;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DateTime time = (DateTime)reader["TimeStamp"];
                        string TAG = (string)reader["TAG"];
                        string Value = (string)reader["Value"];
                        string Description = (string)reader["Description"];
                        string TagType = (string)reader["TagType"];
                        Tag t = new Tag(TAG, time, Value, Description,TagType);
                        tags.Add(t);

                    }
                    return tags.AsReadOnly();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        }
        public IReadOnlyList<string> getAllTagNames()
        {
            string connectionString = @"Data Source=SRV-SKYSPARK\sqlexpress;Initial Catalog=toon;Integrated Security=True;Pooling=False";
            string query = "SELECT TagName FROM MBR_TAGNAMES";
            List<string> tags = new List<string>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                try
                {
                    cn.Open();
                    cmd.CommandText = query;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                       
                        string TAG = (string)reader["TagName"];
                        
                        
                        tags.Add(TAG);

                    }
                    return tags;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        }

        public DateTime getLastRowTime()
        {

            string connectionString = @"Data Source=SRV-SKYSPARK\sqlexpress;Initial Catalog=toon;Integrated Security=True;Pooling=False";
            string query = "SELECT TOP 1 TimeStamp FROM MBR ORDER BY id DESC";
            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))

            {
                try
                {
                    Tag t;
                    cn.Open();
                    cmd.CommandText = query;
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();

                    DateTime time = (DateTime)reader["TimeStamp"];




                    return time;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        }

    }
}
