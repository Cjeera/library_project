using System.Linq.Expressions;
using MySql.Data.MySqlClient;

namespace library_project
{
    class Library
    {
        public void addBook()
        {
            Console.Clear();
            string title = getStringInput("title");

            string author = getStringInput("author");

            int publication_year = getIntegerInput("publication year");

            string genre = getStringInput("genre");

            DatabaseHandler database = new DatabaseHandler();

            using (var connection = database.MakeConnection())
            {
                string query = @"INSERT INTO books (title, author, publication_year, genre) VALUES
                                    (@title, @author, @publication_year, @genre)";
                
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@author", author);
                command.Parameters.AddWithValue("@publication_year", publication_year);
                command.Parameters.AddWithValue("@genre", genre);

                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    Console.WriteLine("Book added!");
                }
                else
                {
                    Console.WriteLine("Failed to add book");
                }
                connection.Close();      
            }
            
        }

        public void updateBook()
        {
            Console.Clear();
            
        }

        public void deleteBook()
        {
            Console.Clear();

            getBooks();

            int book_id = getIntegerInput("book ID");

            DatabaseHandler database = new DatabaseHandler();

            using (var connection = database.MakeConnection())
            {
                string query = @"DELETE FROM books
                                    WHERE book_id = @book_id";

                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@book_id", book_id);

                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    Console.WriteLine("Book deleted!");
                }
                else
                {
                    Console.WriteLine("Failed to delete book!");
                }

                connection.Close();  
            }


            
        }

        public void searchBooks()
        {
            Console.Clear();
            
        }
        public void getBooks()
        {
            Console.Clear();

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

        public string getStringInput(string data)
        {
            
            while (true)
            {
                try
                {
                    Console.WriteLine($"Enter {data}: ");
                    string input = Console.ReadLine();
            
                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine($"Invalid input!");
                        continue;      
                    }

                    return input;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error! {e}");
                    continue;
                }        
            }     
        }

        public int getIntegerInput(string data)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine($"Enter {data}: ");
                    int input = Convert.ToInt32(Console.ReadLine());

                    if (int.IsNegative(input))
                    {
                        Console.WriteLine("Negative numbers are invalid!");
                        continue;
                    }

                    return input;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error! {e}");
                    continue;
                }
            }      
        } 
    }       
}