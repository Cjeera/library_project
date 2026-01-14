namespace library_project
{
    public class Book
    {
        public int bookID { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public int publicationYear { get; set; }
        public string genre { get; set; }

        // Constructor for Book object that takes the Book ID. Used in UpdateBook, DeleteBook, GetAllBooks.
        public Book(int bookID, string title, string author, int publicationYear, string genre)
        {
            this.bookID = bookID;

            this.title = title;

            this.author = author;

            this.publicationYear = publicationYear;

            this.genre = genre;
        }

        // Constructor for Book object that doesn't take the Book ID. Used when inserting new books as ID is given via autoincrement in MySQL.
        public Book(string title, string author, int publicationYear, string genre)
        {
            this.title = title;

            this.author = author;

            this.publicationYear = publicationYear;

            this.genre = genre;
        }
    }
}