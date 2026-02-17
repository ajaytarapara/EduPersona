using EduPersona.Core.Data.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityProvider.Data.Entities
{
    public class RefreshToken : IdentityEntity
    {

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [Required]
        public string Token { get; set; } = null!;

        public int SessionId { get; set; }

        [ForeignKey(nameof(SessionId))]
        public Session Session { get; set; } = null!;

        public DateTime ExpiredAt { get; set; }

        public bool Revoked { get; set; } = false;
    }
}
