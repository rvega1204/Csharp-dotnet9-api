using FirstAPI.Models;
using FluentAssertions;

namespace FirstAPI.Tests.Models
{
    public class BookTests
    {
        [Fact]
        public void Book_CanBeCreated_WithAllProperties()
        {
            // Arrange & Act
            var book = new Book
            {
                Id = 1,
                Title = "Test Book",
                Author = "Test Author",
                YearPublished = 2023
            };

            // Assert
            book.Id.Should().Be(1);
            book.Title.Should().Be("Test Book");
            book.Author.Should().Be("Test Author");
            book.YearPublished.Should().Be(2023);
        }

        [Fact]
        public void Book_Properties_CanBeSet()
        {
            // Arrange
            var book = new Book();

            // Act
            book.Id = 10;
            book.Title = "Another Book";
            book.Author = "Another Author";
            book.YearPublished = 2024;

            // Assert
            book.Id.Should().Be(10);
            book.Title.Should().Be("Another Book");
            book.Author.Should().Be("Another Author");
            book.YearPublished.Should().Be(2024);
        }

        [Fact]
        public void Book_DefaultId_IsZero()
        {
            // Arrange & Act
            var book = new Book
            {
                Title = "Test",
                Author = "Author",
                YearPublished = 2020
            };

            // Assert
            book.Id.Should().Be(0);
        }

        [Theory]
        [InlineData("The Great Gatsby", "F. Scott Fitzgerald", 1925)]
        [InlineData("1984", "George Orwell", 1949)]
        [InlineData("To Kill a Mockingbird", "Harper Lee", 1960)]
        public void Book_CanBeCreated_WithDifferentValues(string title, string author, int year)
        {
            // Arrange & Act
            var book = new Book
            {
                Title = title,
                Author = author,
                YearPublished = year
            };

            // Assert
            book.Title.Should().Be(title);
            book.Author.Should().Be(author);
            book.YearPublished.Should().Be(year);
        }

        [Fact]
        public void Book_YearPublished_CanBeNegative()
        {
            // Arrange & Act
            var book = new Book
            {
                Title = "Ancient Book",
                Author = "Ancient Author",
                YearPublished = -100
            };

            // Assert
            book.YearPublished.Should().Be(-100);
        }
    }
}
