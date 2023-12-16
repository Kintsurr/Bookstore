using Bookstore.Validations;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Dtos
{
    public class BookDto
    {
        public string Title { get; set; }
        public int YearOfPublication { get; set; }
        public int AuthorId { get; set; }
    }
}
