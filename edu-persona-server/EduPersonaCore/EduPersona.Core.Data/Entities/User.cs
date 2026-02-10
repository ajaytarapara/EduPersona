using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EduPersona.Core.Data.Entities.Abstract;

namespace EduPersona.Core.Data.Entities
{
    public class User : AuditEntity
    {
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        public string? PasswordHash { get; set; }

        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role? Role { get; set; }

        public DateOnly? Birthdate { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        [MaxLength(10)]
        public int? PhoneNo { get; set; }

        public bool IsProfileCompleted { get; set; } = false;

        // Audit navigation (self reference)
        public User? CreatedByUser { get; set; }
        public User? UpdatedByUser { get; set; }
        public User? DeletedByUser { get; set; }


        // Reverse navigation
        public ICollection<User> CreatedUsers { get; set; } = new List<User>();
        public ICollection<User> UpdatedUsers { get; set; } = new List<User>();
        public ICollection<User> DeletedUsers { get; set; } = new List<User>();
    }
}