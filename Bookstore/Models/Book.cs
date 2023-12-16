using Bookstore.Validations;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [CapitalLetter]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 50 characters")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [YearRange]
        public int YearOfPublication { get; set; }

        public Author Author { get; set; }
    }
}
