using System.ComponentModel.DataAnnotations;

namespace Bookstore.Validations
{
    public class CapitalLetterAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !(value is string))
            {
                return new ValidationResult("Value is not a string");
            }
            
            string inputString = (string)value;
            

            if (!string.IsNullOrEmpty(inputString) && !char.IsUpper(inputString[0]))
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return "The first letter must be a capital letter.";
        }
    }
}

