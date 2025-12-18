namespace FirstAPI.Models
{
    /// <summary>
    /// Represents a book entity in the database.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Gets or sets the unique identifier for the book.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        public string Title { get; set; } = null!;

        /// <summary>
        /// Gets or sets the author of the book.
        /// </summary>
        public string Author { get; set; } = null!;

        /// <summary>
        /// Gets or sets the year the book was published.
        /// </summary>
        public int YearPublished { get; set; }
    }
}
