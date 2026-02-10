namespace EduPersona.Core.Data.Entities.Abstract
{
    public class AuditEntity : IdentityEntity
    {
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public int CreatedBy { get; set; }

        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

        public int? UpdatedBy { get; set; }

        public DateTimeOffset DeletedAt { get; set; } = DateTimeOffset.UtcNow;

        public int? DeletedBy { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = false;

    }
}