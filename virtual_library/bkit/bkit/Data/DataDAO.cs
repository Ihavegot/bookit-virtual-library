using bkit.Models;
using Microsoft.Data.SqlClient;

namespace bkit.Data
{
    internal class DataDAO
    {
        private string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=bookit;Trusted_Connection=True;MultipleActiveResultSets=true";

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
                try
                {
                    int newID = command.ExecuteNonQuery();
                    return newID;
                }
                catch (Exception)
                {
                    return -1;
                }
                
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

        public int Borrow(int book, string user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "INSERT INTO dbo.borrowed_book VALUES(@userId, @bookId)";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@userId", System.Data.SqlDbType.VarChar).Value = user;
                command.Parameters.Add("@bookId", System.Data.SqlDbType.Int).Value = book;

                connection.Open();
                int borrowId = command.ExecuteNonQuery();

                string sqlQueryUpdate = "UPDATE dbo.book_list SET quantity=(quantity-1) WHERE id=@id";
                SqlCommand commandUpdate = new SqlCommand(sqlQueryUpdate, connection);

                commandUpdate.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = book;

                int borrowIdUpdate = commandUpdate.ExecuteNonQuery();

                return borrowId;
            }
        }

        public List<DataModel> FetchBorrowed(string user)
        {
            List<DataModel> output = new List<DataModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM dbo.borrowed_book WHERE userId=@userId";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@userId", System.Data.SqlDbType.VarChar).Value = user;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    string sqlQueryBorrowed = "SELECT * FROM dbo.book_list WHERE id=@id";

                    while (reader.Read())
                    {
                        int borrowId = reader.GetInt32(0);
                        string userId = reader.GetString(1);
                        int bookId = reader.GetInt32(2);

                        SqlCommand commandBorrowed = new SqlCommand(sqlQueryBorrowed, connection);
                        commandBorrowed.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = bookId;
                        SqlDataReader readerBorrowed = commandBorrowed.ExecuteReader();

                        Console.WriteLine(bookId);
                        Console.WriteLine(readerBorrowed.HasRows);

                        if (readerBorrowed.HasRows)
                        {
                            if (readerBorrowed.Read())
                            {
                                DataModel data = new DataModel();
                                data.id = readerBorrowed.GetInt32(0);
                                data.title = readerBorrowed.GetString(1);
                                data.author = readerBorrowed.GetString(2);
                                data.quantity = readerBorrowed.GetInt32(3);

                                output.Add(data);
                                readerBorrowed.Close();
                            }
                            
                        }

                    }
                }

            }

            return output;
        }

        public int Return(int book, string user)
        {
            Console.WriteLine("HERE1");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string sqlQuery = "DELETE FROM dbo.borrowed_book WHERE bookId=@bookId and userId=@userId";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@bookId", System.Data.SqlDbType.Int).Value = book;
                command.Parameters.Add("@userId", System.Data.SqlDbType.VarChar).Value = user;

                connection.Open();
                int deleteID = command.ExecuteNonQuery();

                Console.WriteLine(book);
                Console.WriteLine(user);

                string sqlQueryUpdate = "UPDATE dbo.book_list SET quantity=(quantity+1) WHERE id=@id";
                SqlCommand commandUpdate = new SqlCommand(sqlQueryUpdate, connection);

                commandUpdate.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = book;

                int borrowIdUpdate = commandUpdate.ExecuteNonQuery();

                return deleteID;
            }
        }

    }
}
