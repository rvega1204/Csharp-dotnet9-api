using FirstAPI.Data;
using FirstAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Controllers
{
    /// <summary>
    /// API controller for managing book resources.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly FirstAPIContext _context;

        /// <summary>
        /// Initializes a new instance of the BooksController class.
        /// </summary>
        /// <param name="context">The database context for accessing book data.</param>
        public BooksController(FirstAPIContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all books from the database.
        /// </summary>
        /// <returns>A list of all books.</returns>
        /// <response code="200">Returns the list of books.</response>
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return Ok(await _context.Books.ToListAsync());
        }

        /// <summary>
        /// Retrieves a specific book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to retrieve.</param>
        /// <returns>The requested book.</returns>
        /// <response code="200">Returns the requested book.</response>
        /// <response code="404">If the book is not found.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        /// <summary>
        /// Creates a new book in the database.
        /// </summary>
        /// <param name="newBook">The book to create.</param>
        /// <returns>The newly created book.</returns>
        /// <response code="201">Returns the newly created book.</response>
        /// <response code="400">If the book is null.</response>
        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book newBook)
        {
            if (newBook == null)
                return BadRequest();

            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
        }

        /// <summary>
        /// Updates an existing book in the database.
        /// </summary>
        /// <param name="id">The ID of the book to update.</param>
        /// <param name="updatedBook">The updated book data.</param>
        /// <returns>No content on success.</returns>
        /// <response code="204">If the book was successfully updated.</response>
        /// <response code="404">If the book is not found.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book updatedBook)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            book.Id = updatedBook.Id;
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.YearPublished = updatedBook.YearPublished;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes a book from the database.
        /// </summary>
        /// <param name="id">The ID of the book to delete.</param>
        /// <returns>No content on success.</returns>
        /// <response code="204">If the book was successfully deleted.</response>
        /// <response code="404">If the book is not found.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }



    }
}
