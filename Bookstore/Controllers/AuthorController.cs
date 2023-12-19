using Bookstore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using Bookstore.Dtos;
using Bookstore.Interfaces;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        // GET: api/Author
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthor()
        {
            var authors = await _authorRepository.GetAllAuthorsAsync();

            if (!authors.Any())
            {
                return NotFound();
            }
            return Ok(authors);
        }

        //GET: api/Author/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }
            return author;
        }

        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AuthorDto authorDto)
        {
            var author = await _authorRepository.InsertAuthorAsync(authorDto);

            return CreatedAtAction(nameof(GetAuthor), new { id = author.AuthorId }, author);
        }

        // PUT: api/Author/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorDto authorDto)
        {
            var updateAuthor = await _authorRepository.UpdateAuthorAsync(id, authorDto);
            if (updateAuthor == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var deleteSuccess = await _authorRepository.DeleteAuthorAsync(id);
            if (!deleteSuccess)
            {
                return NotFound(); // Author not found
            }

            return NoContent();
        }

    }
}
