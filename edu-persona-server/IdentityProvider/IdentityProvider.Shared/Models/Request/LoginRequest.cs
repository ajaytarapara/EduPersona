using System.ComponentModel.DataAnnotations;
using IdentityProvider.Shared.Constants;

namespace IdentityProvider.Shared.Models.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = ValidationMessages.EmailRequired)]
        [EmailAddress(ErrorMessage = ValidationMessages.InvalidEmail)]
        [MaxLength(100, ErrorMessage = ValidationMessages.EmailMaxLength)]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
        [MinLength(6, ErrorMessage = ValidationMessages.PasswordMinLength)]
        [RegularExpression(RegularExpression.PasswordPattern, ErrorMessage = ValidationMessages.PasswordRegex)]
        public string? Password { get; set; }

    }
}