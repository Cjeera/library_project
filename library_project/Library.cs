namespace library_project
{
    class Library
    {
        // DatabaseHandler object is created to allow for MySQL operations.
        private DatabaseHandler db = new DatabaseHandler();

        public void AddBook()
        {
            Console.Clear();

            // Input is taken for title, author, publication year and genre.
            string title = GetStringInput("title");
            string author = GetStringInput("author");
            int year = GetIntInput("publication year");
            string genre = GetStringInput("genre");

            // New book object is created from user inputted values.
            Book book = new Book(title, author, year, genre);

            // Book details are inserted into the database using the Book object.
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

            // All books in the database including their IDs are shown to the user so they can select a book by it's ID.
            GetAllBooks();
            int id = GetIntInput("ID of the book to update");

            // User input is taken for title, author, publication year and genre.
            string title = GetStringInput("new title");
            string author = GetStringInput("new author");
            int year = GetIntInput("new publication year");
            string genre = GetStringInput("new genre");

            // New book object is created from user inputted values.
            Book updated = new Book(title, author, year, genre);

            // Book details are updated with the user inputted details.
            if (db.UpdateBook(id, updated))
            {
                Console.WriteLine("Book updated!");  
            }           
            else
            {
                Console.WriteLine("Update failed. ");
            }            
        }

        public void DeleteBook()
        {
            // All books in the database including their IDs are shown to the user so they can select a book by it's ID.
            GetAllBooks();
            int id = GetIntInput("ID of the book to delete");

            // The specified book is delected from the database.
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
            // Gets every book and it's details from the database and stores it all in a list of book objects.
            var books = db.GetAllBooks();

            // Loops through every book object found in the list and prints it to console.
            foreach (var book in books)
            {
                Console.WriteLine($"ID={book.bookID}, Title={book.title}, Author={book.author}, Year={book.publicationYear}, Genre={book.genre}");
            }

            return books;
        }

        public void SearchBooks()
        {
            // User input is taken for book title.
            string title = GetStringInput("title to search");

            // All results found are stored in a list of book objects.
            var results = db.SearchByTitle(title);

            // Displays every book found by the search.
            Console.WriteLine("Search results:");
            foreach (var book in results)
            {
                Console.WriteLine($"ID={book.bookID}, Title={book.title}, Author={book.author}, Year={book.publicationYear}, Genre={book.genre}");
            }
        }

        // A helper function for taking string inputs. Saves repeating code. 
        private string GetStringInput(string field)
        {
            while (true)
            {
                // User is prompted to input something for a specific field. This field is provided via the function call argument (Examples: Name, Title).
                Console.Write($"Enter {field}: ");
                string input = Console.ReadLine();

                // Returns the inputted value if it's null, empty, or only has white-space characters. If not, the loop continues and the user is prompted to enter a value again.
                if (!string.IsNullOrWhiteSpace(input))
                    return input;
                Console.WriteLine("Invalid input!");
            }
        }

        // A helper function for taking integer inputs. Saves repeating code.
        private int GetIntInput(string field)
        {
            while (true)
            {
                // User is prompted to input something for a specific field. This field is provided via the function call argument (Examples: Publication Year, Book ID).
                Console.Write($"Enter {field}: ");

                // Converts the input into an integer. If successful and the value is more or equal to 0, then value is returned. If not, the loop continues and the user is prompted to enter a value again
                if (int.TryParse(Console.ReadLine(), out int value) && value >= 0)
                    return value;
                Console.WriteLine("Invalid number!");
            }
        }
    }
}
