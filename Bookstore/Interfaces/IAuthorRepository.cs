using Bookstore.Models;

namespace Bookstore.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int authorId);
        Task<Author> AddAuthorAsync( Author author);
        Task<Author> UpdateAuthorAsync(int id, Author author);
        Task DeleteAuthorAsync(int authorId);
    }
}
