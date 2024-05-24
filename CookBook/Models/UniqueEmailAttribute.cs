using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CookBook.Models;

namespace CookBook.Models
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            var dbContext = (CookBookDbContext)validationContext.GetService(typeof(CookBookDbContext));
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var existingUser = dbContext.Users.FirstOrDefault(u => u.Email == (string)value);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            if (existingUser != null)
            {
                return new ValidationResult("Email must be unique.");
            }

#pragma warning disable CS8603 // Possible null reference return.
            return ValidationResult.Success;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
