using EduPersona.Core.Data.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityProvider.Data.Entities
{
    public class Session : IdentityEntity
    {
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime ExpiredAt { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
