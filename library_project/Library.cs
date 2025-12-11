using MySql.Data.MySqlClient;

namespace library_project
{
    class Library
    {
        private DatabaseHandler db = new DatabaseHandler();

        public void AddBook()
        {
            Console.Clear();
            string title = GetStringInput("title");
            string author = GetStringInput("author");
            int year = GetIntInput("publication year");
            string genre = GetStringInput("genre");

            Book book = new Book(title, author, year, genre);

            if (db.InsertBook(book))
            {
                Console.WriteLine("Book added successfully!");   
            }

            else
            {
                Console.WriteLine("Failed to add book.");           
            }
        }
        
        public void UpdateBook()
        {
            Console.Clear();
            GetAllBooks();

            int id = GetIntInput("ID of the book to update");

            string title = GetStringInput("new title");
            string author = GetStringInput("new author");
            int year = GetIntInput("new publication year");
            string genre = GetStringInput("new genre");

            Book updated = new Book(title, author, year, genre);

            if (db.UpdateBook(id, updated))
            {
                Console.WriteLine("Book updated!");
                
            }           
            else
            {
                Console.WriteLine("Update failed.");
            }            
        }

        public void DeleteBook()
        {
            Console.Clear();
            GetAllBooks();

            int id = GetIntInput("ID of the book to delete");

            if (db.DeleteBook(id))
            {
                Console.WriteLine("Book deleted!");  
            }       
            else
            {
                Console.WriteLine("Delete failed.");
            }            
        }

        public List<Book> GetAllBooks()
        {
            Console.Clear();
            var books = db.GetAllBooks();

            foreach (var book in books)
            {
                Console.WriteLine($"ID={book.bookID}, Title={book.title}, Author={book.author}, Year={book.publicationYear}, Genre={book.genre}");
            }

            return books;
        }

        public void SearchBooks()
        {
            Console.Clear();
            string title = GetStringInput("title to search");

            var results = db.SearchByTitle(title);

            Console.WriteLine("Search results:");
            foreach (var b in results)
            {
                Console.WriteLine($"ID={b.bookID}, Title={b.title}, Author={b.author}, Year={b.publicationYear}, Genre={b.genre}");
            }
        }

        private string GetStringInput(string field)
        {
            while (true)
            {
                Console.Write($"Enter {field}: ");
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                Console.WriteLine("Invalid input!");
            }
        }

        private int GetIntInput(string field)
        {
            while (true)
            {
                Console.Write($"Enter {field}: ");
                if (int.TryParse(Console.ReadLine(), out int value) && value >= 0)
                    return value;

                Console.WriteLine("Invalid number!");
            }
        }
    }
}
