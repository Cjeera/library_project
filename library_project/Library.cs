using MySql.Data.MySqlClient;

namespace library_project
{
    class Library
    {
        public void addBook()
        {
            
        }

        public void updateBook()
        {
            
        }

        public void deleteBook()
        {
            
        }

        public void searchBooks()
        {
            
        }
        public void getBooks()
        {
            DatabaseHandler database = new DatabaseHandler();

            using (var connection = database.MakeConnection())
            {
                try
                {
                    string query = @"SELECT * FROM books";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    var queryResult = command.ExecuteReader();

                    
                    List<Book> bookList = new List<Book>();
                    while (queryResult.Read())
                    { 
                        var id = queryResult.GetInt32("book_id");
                        var title = queryResult.GetString("title");
                        var author = queryResult.GetString("author");
                        var publicationYear = queryResult.GetInt32("publication_year");
                        var genre = queryResult.GetString("genre");
                        Book book = new Book(id, title, author, publicationYear, genre);
                        bookList.Add(book);
                    }

                    connection.Close();

                    foreach (Book item in bookList)
                    {
                        Console.WriteLine($"ID = {item.bookID}, Title = {item.title}, Author = {item.author}, Publication Year = {item.publicationYear}, Genre = {item.genre}");
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Error connecting to database: {ex}");
                }
            }
        } 
    }       
}