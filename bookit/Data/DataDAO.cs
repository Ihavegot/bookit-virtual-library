using bookit.Models;
using System.Data.SqlClient;

namespace bookit.Data
{
    internal class DataDAO
    {

        // CRUD operations
        private string connectionString = @"Data Source=DESKTOP-GS37F69;Initial Catalog=bookitdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public List<DataModel> FetchAll()
        {
            List<DataModel> output = new List<DataModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM dbo.book_list";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DataModel data = new DataModel();
                        data.id = reader.GetInt32(0);
                        data.title = reader.GetString(1);   
                        data.author = reader.GetString(2);  
                        data.quantity = reader.GetInt32(3);

                        output.Add(data);
                    }
                } 

            }

            return output;
        }

        public DataModel FetchOne(int output_id)
        {
            DataModel output = new DataModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM dbo.book_list WHERE id = @id";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = output_id;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        output.id = reader.GetInt32(0);
                        output.title = reader.GetString(1);
                        output.author = reader.GetString(2);
                        output.quantity = reader.GetInt32(3);
                    }                   
                }

            }

            return output;
        }

        public int CreateOrUpdate(DataModel newData)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "";
                if (newData.id <= 0)
                {
                    sqlQuery = "INSERT INTO dbo.book_list VALUES(@title, @author, @quantity)";
                }
                else
                {
                    sqlQuery = "UPDATE dbo.book_list SET title=@title, author=@author, quantity=@quantity WHERE id=@id";
                }

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = newData.id;
                command.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 255).Value = newData.title;
                command.Parameters.Add("@author", System.Data.SqlDbType.VarChar, 255).Value = newData.author;
                command.Parameters.Add("@quantity", System.Data.SqlDbType.Int).Value = newData.quantity;

                connection.Open();
                int newID = command.ExecuteNonQuery();
                return newID;
            }

        }

        public int Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string sqlQuery = "DELETE FROM dbo.book_list WHERE id=@id";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                
                connection.Open();
                int deleteID = command.ExecuteNonQuery();
                return deleteID;
            }
        }

    }
}