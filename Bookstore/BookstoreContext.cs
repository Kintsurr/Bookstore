using Microsoft.EntityFrameworkCore;
using Bookstore.Models;

using Bookstore.Models;
using Microsoft.EntityFrameworkCore;


namespace Bookstore
{
    public class BookstoreContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Author { get; set; }

        public BookstoreContext(DbContextOptions<BookstoreContext> options) : base(options)
        {
        }

       

        
    }
}
