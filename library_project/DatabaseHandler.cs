using MySql.Data.MySqlClient;

namespace library_project
{
    class DatabaseHandler
    {
        private readonly string connectionString = "server=localhost;port=3306;database=librarydb;user=root;password=root";

        // Creates a new connection to a local MySQL DB.
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
        
        public bool InsertBook(Book book)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                string query = @"INSERT INTO books (title, author, publication_year, genre)
                                 VALUES (@title, @author,
                                         @year, @genre)";

                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@title", book.title);
                cmd.Parameters.AddWithValue("@author", book.author);
                cmd.Parameters.AddWithValue("@year", book.publicationYear);
                cmd.Parameters.AddWithValue("@genre", book.genre);

                try
                {
                    return cmd.ExecuteNonQuery() > 0;      
                }
                catch (MySqlException error)
                {
                    Console.WriteLine(error.Message);
                    return false;
                }        
            }
        }
        
        public bool UpdateBook(int id, Book book)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                string query = @"UPDATE books 
                                 SET title=@title, author=@author,
                                     publication_year=@year, genre=@genre
                                 WHERE book_id=@id";

                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@title", book.title);
                cmd.Parameters.AddWithValue("@author", book.author);
                cmd.Parameters.AddWithValue("@year", book.publicationYear);
                cmd.Parameters.AddWithValue("@genre", book.genre);

                try
                {
                    return cmd.ExecuteNonQuery() > 0;      
                }
                catch (MySqlException error)
                {
                    Console.WriteLine(error.Message);
                    return false;
                }   
            }
        }
        
        public bool DeleteBook(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                string query = "DELETE FROM books WHERE book_id=@id";

                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    return cmd.ExecuteNonQuery() > 0;      
                }
                catch (MySqlException error)
                {
                    Console.WriteLine(error.Message);
                    return false;
                }
            }
        }

        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            using (var connection = GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM books";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                try
                {
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        books.Add(new Book
                        (
                            reader.GetInt32("book_id"),
                            reader.GetString("title"),
                            reader.GetString("author"),
                            reader.GetInt32("publication_year"),
                            reader.GetString("genre")
                        ));
                    }           
                }
                catch (MySqlException error)
                {
                    Console.WriteLine(error.Message);
                    return books;
                }
            }

            return books;
        }

        public List<Book> SearchByTitle(string titleSearch)
        {
            List<Book> results = new List<Book>();

            using (var connection = GetConnection())
            {
                connection.Open();

                string query = @"SELECT * FROM books 
                                 WHERE title LIKE CONCAT('%', @search, '%')";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@search", titleSearch);

                try
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        results.Add(new Book
                        (
                            reader.GetInt32("book_id"),
                            reader.GetString("title"),
                            reader.GetString("author"),
                            reader.GetInt32("publication_year"),
                            reader.GetString("genre")
                        ));
                    }        
                }
                catch (MySqlException error)
                {
                    Console.WriteLine(error.Message);
                    return results;
                }
            }

            return results;
        }
    }
}
