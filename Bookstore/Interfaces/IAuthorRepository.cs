using Bookstore.Dtos;
using Bookstore.Models;

namespace Bookstore.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
        Task<Author> InsertAuthorAsync(AuthorDto authorDto);
        Task<Author> UpdateAuthorAsync(int id, AuthorDto authorDto);
        Task<bool> DeleteAuthorAsync(int id);
    }
}
