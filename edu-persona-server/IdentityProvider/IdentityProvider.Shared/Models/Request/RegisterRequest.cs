using IdentityProvider.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace IdentityProvider.Shared.Models.Request
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = ValidationMessages.FirstNameRequired)]
        [MaxLength(50, ErrorMessage = ValidationMessages.NameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = ValidationMessages.LastNameRequired)]
        [MaxLength(50, ErrorMessage = ValidationMessages.NameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = ValidationMessages.EmailRequired)]
        [EmailAddress(ErrorMessage = ValidationMessages.InvalidEmail)]
        [MaxLength(100, ErrorMessage = ValidationMessages.EmailMaxLength)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
        [MinLength(6, ErrorMessage = ValidationMessages.PasswordMinLength)]
        [RegularExpression(RegularExpression.PasswordPattern, ErrorMessage = ValidationMessages.PasswordRegex)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = ValidationMessages.ConfirmPasswordRequired)]
        [Compare(nameof(Password), ErrorMessage = ValidationMessages.PasswordMismatch)]
        public string? ConfirmPassword { get; set; } = null!;
    }
}
