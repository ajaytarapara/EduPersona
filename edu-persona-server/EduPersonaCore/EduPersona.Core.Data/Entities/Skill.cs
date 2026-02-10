using System.ComponentModel.DataAnnotations;
using EduPersona.Core.Data.Entities.Abstract;

namespace EduPersona.Core.Data.Entities
{
    public class Skill : AuditEntity
    {
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}