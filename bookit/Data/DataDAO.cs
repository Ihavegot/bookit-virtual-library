using bookit.Models;
using System.Data.SqlClient;

namespace bookit.Data
{
    internal class DataDAO
    {
        private string connectionString = @"Data Source=DESKTOP-GS37F69;Initial Catalog=bookitdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public List<DataModel> FetchAll()
        {
            List<DataModel> returnList = new List<DataModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM dbo.books";
                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DataModel data = new DataModel();
                    data.Id = reader.GetInt32(0);
                    data.Title = reader.GetString(1);   
                    data.Author = reader.GetString(2);  
                    data.Quantity = reader.GetInt32(3); 

                    returnList.Add(data);
                }

            }

            return returnList;
        }

        public int Create(DataModel newData)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "INSERT INTO dbo.books VALUES(@Id, @Title, @Author, @Quantity)";
                SqlCommand cmd = new SqlCommand(sqlQuery, connection);

                cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int, 100).Value = newData.Id;
                cmd.Parameters.Add("@Title", System.Data.SqlDbType.VarChar, 100).Value = newData.Title;
                cmd.Parameters.Add("@Author", System.Data.SqlDbType.VarChar, 100).Value = newData.Author;
                cmd.Parameters.Add("@Quantity", System.Data.SqlDbType.Int, 100).Value = newData.Quantity;

                connection.Open();
                int newID = cmd.ExecuteNonQuery();
                return newID;
            }

        }

    }
}