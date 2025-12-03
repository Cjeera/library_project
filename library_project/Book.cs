namespace library_project
{
    class Book
    {
        public int bookID { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public int publicationYear { get; set; }
        public string genre { get; set; }

        public Book(int bookID, string title, string author, int publicationYear, string genre)
        {
            this.bookID = bookID;

            this.title = title;

            this.author = author;

            this.publicationYear = publicationYear;

            this.genre = genre;
        }

        public Book() {}
    }
}