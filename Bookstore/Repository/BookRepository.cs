using Bookstore.Dtos;
using Bookstore.Interfaces;
using Bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookstoreContext _context;

        public BookRepository(BookstoreContext context)
        {
            _context = context;
        }

        async Task<IEnumerable<Book>> IBookRepository.GetAllBooksAsync()
        {

            return await _context.Books.Include(p => p.Author).ToListAsync();
        }

        async Task<Book> IBookRepository.GetBookByIdAsync(int id)
        {
            return await _context.Books.Include(p => p.Author).FirstOrDefaultAsync(x => x.Id == id);
        }

        async Task<Book> IBookRepository.InsertBookAsync(BookDto bookDto)
        {
            var author = await _context.Author.FindAsync(bookDto.AuthorId);
            if (author == null)
            {
                return null; // Author not found
            }

            var book = new Book
            {
                Title = bookDto.Title,
                YearOfPublication = bookDto.YearOfPublication,
                Author = author
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book;
        }

        async Task<Book> IBookRepository.UpdateBookAsync(int id, BookDto bookDto)
        {
            var book = await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
            if (book != null)
            {
                book.Title = bookDto.Title;
                book.YearOfPublication = bookDto.YearOfPublication;

                
                var author = await _context.Author.FindAsync(bookDto.AuthorId);
                if (author != null)
                {
                    book.Author = author;
                }
                else
                {
             
                    return null;
                }
                
                _context.Books.Update(book);
                await _context.SaveChangesAsync();
            }
            return book;
        }

        async Task<bool> IBookRepository.DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return false; // Book not found
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true; // Successfully deleted
        }
    }
}
