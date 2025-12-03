using System.Linq.Expressions;

namespace library_project
{
    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            while (true)
            {
                Console.WriteLine("Library Menu");
                Console.WriteLine("1. Add a Book\n2. Update Book Details\n3. Delete a Book\n4. View all books\n5. Search for a book by title\n6. Exit");

                switch(Console.ReadLine())
                {
                    case "1":
                        try
                        {
                            library.addBook();
                            continue;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex}");
                            continue;
                        }
                    
                    case "2":
                        try
                        {
                            library.updateBook();
                            continue;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex}");
                            continue;
                        }
                        
                    case "3":
                        try
                        {
                            library.deleteBook();
                            continue;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex}");
                            continue;
                        }
    
                    case "4":
                        try
                        {
                            library.getBooks();
                            continue;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex}");
                            continue;
                        }

                    case "5":
                        try
                        {
                            library.searchBooks();
                            continue;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex}");
                            continue;
                        }

                    case "6":
                        Console.WriteLine("Exiting program!");
                        break;

                    default:
                        Console.WriteLine("Input error!");
                        continue;
                }
                break;   
            }        
        }
    }
}