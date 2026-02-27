using EduPersona.Core.Data.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EduPersona.Core.Data.Entities
{
    public class UserProfile : AuditEntity
    {
        public int? UserId { get; set; }

        public DateTimeOffset? Birthdate { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        [MaxLength(10)]
        public string? PhoneNo { get; set; }

        public bool IsProfileCompleted { get; set; } = false;

    }
}
