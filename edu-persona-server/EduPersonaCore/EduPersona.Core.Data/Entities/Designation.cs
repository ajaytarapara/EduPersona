using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EduPersona.Core.Data.Entities.Abstract;

namespace EduPersona.Core.Data.Entities
{
    public class Designation : AuditEntity
    {
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public int ProfessionId { get; set; }

        [ForeignKey(nameof(ProfessionId))]
        public Profession? Profession { get; set; }

    }
}