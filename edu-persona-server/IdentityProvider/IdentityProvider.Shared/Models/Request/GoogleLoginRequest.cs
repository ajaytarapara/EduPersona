using System.ComponentModel.DataAnnotations;

namespace IdentityProvider.Shared.Models.Request
{
    public class GoogleLoginRequest
    {
        [Required]
        public string Code { get; set; } = default!;
    }
}
