using EduPersona.Core.Data.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace IdentityProvider.Data.Entities
{
    public class Client : IdentityEntity
    {
        [Required]
        public string ClientSecret { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string AppName { get; set; } = null!;

        public string? RedirectUris { get; set; }

        public string? PostLogoutUris { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
