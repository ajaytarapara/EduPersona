using EduPersona.Core.Data.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace IdentityProvider.Data.Entities
{
    public class Role : IdentityEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
