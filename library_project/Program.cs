using library_project;

class Program
{
    static void Main(string[] args)
    {
        // Library object is created which allows it's methods to be called by the user.
        Library library = new Library();

        // Library menu. Prints the name of the program and presents the user all available options.
        while (true)
        {
            Console.WriteLine("\nLibrary Program");
            Console.WriteLine("1. Add a Book\n2. Update Book\n3. Delete Book\n4. View All Books\n5. Search Books\n6. Clear\n7. Exit");

            switch (Console.ReadLine()?.Trim())
            {
                case "1": library.AddBook(); break;
                case "2": library.UpdateBook(); break;
                case "3": library.DeleteBook(); break;
                case "4": library.GetAllBooks(); break;
                case "5": library.SearchBooks(); break;
                case "6": Console.Clear(); break;
                case "7": return;
                default: Console.WriteLine("Invalid option."); break;
            }
        }
    }
}
