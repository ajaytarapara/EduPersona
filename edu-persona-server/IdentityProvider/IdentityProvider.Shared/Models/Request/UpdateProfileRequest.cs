using System.ComponentModel.DataAnnotations;

namespace IdentityProvider.Shared.Models.Request
{
    public class UpdateProfileRequest
    {
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }
    }
}
