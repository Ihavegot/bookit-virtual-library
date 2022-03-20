using bookit.Models;
using System.Data.SqlClient;

namespace bookit.Data
{
    internal class DataDAO
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=bookit_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public List<DataModel> FetchAll()
        {
            List<DataModel> returnList = new List<DataModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM dbo.mytable";
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
    }
}