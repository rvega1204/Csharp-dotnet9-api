using FirstAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Data
{
    /// <summary>
    /// Database context for the FirstAPI application.
    /// </summary>
    public class FirstAPIContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the FirstAPIContext class.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public FirstAPIContext(DbContextOptions<FirstAPIContext> options):base(options) { }

        /// <summary>
        /// Configures the model and seeds initial data.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for the context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    YearPublished = 1925
                },
                new Book
                {
                    Id = 2,
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    YearPublished = 1960
                },
                new Book
                {
                    Id = 3,
                    Title = "1984",
                    Author = "George Orwell",
                    YearPublished = 1949
                },
                new Book
                {
                    Id = 4,
                    Title = "Pride and Prejudice",
                    Author = "Jane Austen",
                    YearPublished = 1813
                },
                new Book
                {
                    Id = 5,
                    Title = "Moby-Dick",
                    Author = "Herman Melville",
                    YearPublished = 1851
                }
                );
        }

        /// <summary>
        /// Gets or sets the DbSet of books in the database.
        /// </summary>
        public DbSet<Book> Books { get; set; }
    }
}
