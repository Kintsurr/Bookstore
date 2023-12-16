using Bookstore.Validations;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [CapitalLetter]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Author Name must be between 2 and 50 characters")]
        [Required(ErrorMessage = "Author Name is required")]
        public string Name { get; set; }

        public List<Book> Books { get; set;}
    }
}
