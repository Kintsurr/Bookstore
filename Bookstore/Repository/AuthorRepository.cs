using Bookstore.Dtos;
using Bookstore.Interfaces;
using Bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookstoreContext _context;

        public AuthorRepository(BookstoreContext context)
        {
            _context = context;
        }

        async Task<IEnumerable<Author>> IAuthorRepository.GetAllAuthorsAsync()
        {
            return await _context.Author.Include(p => p.Books).ToListAsync();
        }

        async Task<Author> IAuthorRepository.GetAuthorByIdAsync(int id)
        {
            return await _context.Author.Include(p => p.Books).FirstOrDefaultAsync(x => x.AuthorId == id);
        }

        async Task<Author> IAuthorRepository.InsertAuthorAsync(AuthorDto authorDto)
        {
            var author = new Author
            {
                Name = authorDto.Name
            };

            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            return author;
        }

        async Task<Author> IAuthorRepository.UpdateAuthorAsync(int id, AuthorDto authorDto)
        {
            var author = await _context.Author.Include(b => b.Books).FirstOrDefaultAsync(b => b.AuthorId == id);
            if (author != null)
            {
                author.Name = authorDto.Name;

                try
                {
                    _context.Author.Update(author);
                    await _context.SaveChangesAsync();
                    return author;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AuthorExistsAsync(id))
                    {
                        return null;
                    }
                    else
                    {
                        throw; 
                    }
                }
            }
            else
            {
                return null;
            }
        }

        async Task<bool> IAuthorRepository.DeleteAuthorAsync(int id)
        {
            var author = await _context.Author.FindAsync(id);
            if (author == null)
            {
                return false; // Author not found
            }

            _context.Author.Remove(author);
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<bool> AuthorExistsAsync(int id)
        {
            return await _context.Author.AnyAsync(a => a.AuthorId == id);
        }

    }
}
