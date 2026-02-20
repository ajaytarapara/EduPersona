using System.ComponentModel.DataAnnotations;

namespace EduPersona.Core.Shared.Models.Request
{
    public class Login
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
