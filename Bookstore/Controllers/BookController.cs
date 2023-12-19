using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Dtos;
using Bookstore.Models;
using Bookstore.Interfaces;
using Bookstore.Repository;
using static System.Reflection.Metadata.BlobBuilder;
using System.Xml.Linq;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        //GET: api/Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(string? title = null, string? sortBy = null, int page = 1, int pageSize = 10)
        {
            var books = await _bookRepository.GetAllBooksAsync();

            if (string.IsNullOrEmpty(title) == false)
            {
                books = books.Where(a => a.Title.Contains(title));
            }

            if (string.IsNullOrEmpty(sortBy) == false)
            {
                switch (sortBy.ToLower())
                {
                    case "title":
                        books = books.OrderBy(a => a.Title);
                        break;
                    case "id":
                        books = books.OrderBy(a => a.Id);
                        break;
                    default:
                        books = books.OrderBy(a => a.Title); // Default sorting if unrecognized field
                        break;
                }
            }
            else
            {
                books = books.OrderBy(a => a.Title);
            }

            var pagedBooks = books.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            if (books == null)
            {
                return NotFound();
            }


            return Ok(books);
        }

        //GET: api/Book/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookDto bookDto)
        {
            var book = await _bookRepository.InsertBookAsync(bookDto);
            if (book == null)
            {
                return BadRequest("Author not found");
            }

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookDto bookDto)
        {
            var updatedBook = await _bookRepository.UpdateBookAsync(id, bookDto);
            if (updatedBook == null)
            {
                return NotFound();
            }

            return NoContent();
        }


        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {

            var deleteSuccess = await _bookRepository.DeleteBookAsync(id);
            if (!deleteSuccess)
            {
                return NotFound(); // Book not found
            }

            return NoContent(); // Successfully deleted, return HTTP 204
        }

    }
}
