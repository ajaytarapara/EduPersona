using IdentityProvider.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace IdentityProvider.Shared.Models.Request
{
    public class Register
    {
        [Required(ErrorMessage = ValidationMessages.FirstNameRequired)]
        [MaxLength(50, ErrorMessage = ValidationMessages.NameMaxLength)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = ValidationMessages.LastNameRequired)]
        [MaxLength(50, ErrorMessage = ValidationMessages.NameMaxLength)]
        public string LastName { get; set; }

        [Required(ErrorMessage = ValidationMessages.EmailRequired)]
        [EmailAddress(ErrorMessage = ValidationMessages.InvalidEmail)]
        [MaxLength(100, ErrorMessage = ValidationMessages.EmailMaxLength)]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
        [MinLength(6, ErrorMessage = ValidationMessages.PasswordMinLength)]
        [RegularExpression(RegularExpression.PasswordPattern, ErrorMessage = ValidationMessages.PasswordRegex)]
        public string? Password { get; set; }

        [Required(ErrorMessage = ValidationMessages.ConfirmPasswordRequired)]
        [Compare(nameof(Password), ErrorMessage = ValidationMessages.PasswordMismatch)]
        public string? ConfirmPassword { get; set; }
    }
}
