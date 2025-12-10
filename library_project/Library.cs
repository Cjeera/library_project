using System.Linq.Expressions;
using System.Reflection;
using Google.Protobuf.WellKnownTypes;
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

            getBooks();
            
            int book_id = getIntegerInput("the ID of the book you want to update");

            int column = getIntegerInput("which column you wish to edit (1 - Title, 2 - Author, 3 - Publication Year, 4 - Genre)");

            string columnName = null;
            switch (column)
            {
                case 1:
                    columnName = "title";
                    break;
                case 2:
                    columnName = "author";
                    break;
                case 3:
                    columnName = "publication_year";
                    break;
                case 4:
                    columnName = "genre";
                    break;
                default:
                    Console.WriteLine("Invalid column selection!");
                    return;         
            }
      
            object value = null;
            if (column == 3)
            {
                value = getIntegerInput("new data");    
            }
            else
            {
                value = getStringInput("new data");
            }

            DatabaseHandler database = new DatabaseHandler();

            using (var connection = database.MakeConnection())
            {
                string query = $@"UPDATE books
                                    SET {columnName} = @value
                                    WHERE book_id = @book_id";
                

                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@book_id", book_id);
                command.Parameters.AddWithValue("@value", value);

                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    Console.WriteLine("Updated book details!");
                }
                else
                {
                    Console.WriteLine("Failed to update details!");
                }       
            }
        }

        public void deleteBook()
        {
            Console.Clear();

            getBooks();

            int book_id = getIntegerInput("the ID of the book you want to delete");

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
            List<Book> bookList = getBooks();
            Console.Clear();

            string titleInput = getStringInput("book title");

            Console.WriteLine("Search results:");

            foreach (Book entry in bookList)
            {
                if (entry.title.Contains(titleInput, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"ID = {entry.bookID}, Title = {entry.title}, Author = {entry.author}, Publication Year = {entry.publicationYear}, Genre = {entry.genre}");           
                }
            }
        }

        public List<Book> getBooks()
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

                    foreach (Book entry in bookList)
                    {
                        Console.WriteLine($"ID = {entry.bookID}, Title = {entry.title}, Author = {entry.author}, Publication Year = {entry.publicationYear}, Genre = {entry.genre}");
                    }

                    return bookList;
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Error connecting to database: {ex}");
                    return null;
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