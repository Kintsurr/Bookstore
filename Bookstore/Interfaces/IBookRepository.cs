using Bookstore.Dtos;
using Bookstore.Models;

namespace Bookstore.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<Book> InsertBookAsync(BookDto bookDto);
        Task<Book> UpdateBookAsync(int id, BookDto bookDto);
        Task<bool> DeleteBookAsync(int id);
    }
}
