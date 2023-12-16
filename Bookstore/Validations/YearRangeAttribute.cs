using System.ComponentModel.DataAnnotations;

namespace Bookstore.Validations
{
    public class YearRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int currentYear = DateTime.Now.Year;
            int inputYear = (int)value;

            if (inputYear < 1700 || inputYear > currentYear)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Year must be between 1700 and {DateTime.Now.Year}.";
        }
    }
}
