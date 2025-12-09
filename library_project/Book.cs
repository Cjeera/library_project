namespace library_project
{
    public class Book
    {
        public int bookID { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public int publicationYear { get; set; }
        public string genre { get; set; }

        //Constructor used when listing and searching books. Includes book ID.
        public Book(int bookID, string title, string author, int publicationYear, string genre)
        {
            this.bookID = bookID;

            this.title = title;

            this.author = author;

            this.publicationYear = publicationYear;

            this.genre = genre;
        }

        //Constructor used for adding books. Excludes book ID since it auto increments in the database.
        public Book(string title, string author, int publicationYear, string genre)
        {
            this.title = title;

            this.author = author;

            this.publicationYear = publicationYear;

            this.genre = genre;
        }
    }
}