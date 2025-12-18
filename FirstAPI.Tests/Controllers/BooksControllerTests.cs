using FirstAPI.Controllers;
using FirstAPI.Data;
using FirstAPI.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Tests.Controllers
{
    public class BooksControllerTests
    {
        private FirstAPIContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<FirstAPIContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new FirstAPIContext(options);
        }

        [Fact]
        public async Task GetBooks_ReturnsOkResult_WithListOfBooks()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var testBooks = new List<Book>
            {
                new Book { Id = 1, Title = "Test Book 1", Author = "Author 1", YearPublished = 2020 },
                new Book { Id = 2, Title = "Test Book 2", Author = "Author 2", YearPublished = 2021 }
            };
            context.Books.AddRange(testBooks);
            await context.SaveChangesAsync();

            var controller = new BooksController(context);

            // Act
            var result = await controller.GetBooks();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var books = okResult.Value.Should().BeAssignableTo<List<Book>>().Subject;
            books.Should().HaveCount(2);
            books.Should().Contain(b => b.Title == "Test Book 1");
            books.Should().Contain(b => b.Title == "Test Book 2");
        }

        [Fact]
        public async Task GetBooks_ReturnsEmptyList_WhenNoBooksExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new BooksController(context);

            // Act
            var result = await controller.GetBooks();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var books = okResult.Value.Should().BeAssignableTo<List<Book>>().Subject;
            books.Should().BeEmpty();
        }

        [Fact]
        public async Task GetBookById_ReturnsOkResult_WithBook()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var testBook = new Book { Id = 1, Title = "Test Book", Author = "Test Author", YearPublished = 2022 };
            context.Books.Add(testBook);
            await context.SaveChangesAsync();

            var controller = new BooksController(context);

            // Act
            var result = await controller.GetBookById(1);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var book = okResult.Value.Should().BeOfType<Book>().Subject;
            book.Id.Should().Be(1);
            book.Title.Should().Be("Test Book");
            book.Author.Should().Be("Test Author");
            book.YearPublished.Should().Be(2022);
        }

        [Fact]
        public async Task GetBookById_ReturnsNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new BooksController(context);

            // Act
            var result = await controller.GetBookById(999);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task AddBook_ReturnsCreatedAtAction_WithNewBook()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new BooksController(context);
            var newBook = new Book { Title = "New Book", Author = "New Author", YearPublished = 2023 };

            // Act
            var result = await controller.AddBook(newBook);

            // Assert
            var createdAtActionResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
            var book = createdAtActionResult.Value.Should().BeOfType<Book>().Subject;
            book.Title.Should().Be("New Book");
            book.Author.Should().Be("New Author");
            book.YearPublished.Should().Be(2023);

            // Verify book was added to database
            var bookInDb = await context.Books.FindAsync(book.Id);
            bookInDb.Should().NotBeNull();
            bookInDb!.Title.Should().Be("New Book");
        }

        [Fact]
        public async Task AddBook_ReturnsBadRequest_WhenBookIsNull()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new BooksController(context);

            // Act
            var result = await controller.AddBook(null!);

            // Assert
            result.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateBook_ReturnsNoContent_WhenBookIsUpdated()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var existingBook = new Book { Id = 1, Title = "Original Title", Author = "Original Author", YearPublished = 2020 };
            context.Books.Add(existingBook);
            await context.SaveChangesAsync();

            var controller = new BooksController(context);
            var updatedBook = new Book { Id = 1, Title = "Updated Title", Author = "Updated Author", YearPublished = 2024 };

            // Act
            var result = await controller.UpdateBook(1, updatedBook);

            // Assert
            result.Should().BeOfType<NoContentResult>();

            // Verify book was updated in database
            var bookInDb = await context.Books.FindAsync(1);
            bookInDb.Should().NotBeNull();
            bookInDb!.Title.Should().Be("Updated Title");
            bookInDb.Author.Should().Be("Updated Author");
            bookInDb.YearPublished.Should().Be(2024);
        }

        [Fact]
        public async Task UpdateBook_ReturnsNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new BooksController(context);
            var updatedBook = new Book { Id = 999, Title = "Updated Title", Author = "Updated Author", YearPublished = 2024 };

            // Act
            var result = await controller.UpdateBook(999, updatedBook);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeleteBook_ReturnsNoContent_WhenBookIsDeleted()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var bookToDelete = new Book { Id = 1, Title = "Book to Delete", Author = "Author", YearPublished = 2020 };
            context.Books.Add(bookToDelete);
            await context.SaveChangesAsync();

            var controller = new BooksController(context);

            // Act
            var result = await controller.DeleteBook(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();

            // Verify book was deleted from database
            var bookInDb = await context.Books.FindAsync(1);
            bookInDb.Should().BeNull();
        }

        [Fact]
        public async Task DeleteBook_ReturnsNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new BooksController(context);

            // Act
            var result = await controller.DeleteBook(999);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
