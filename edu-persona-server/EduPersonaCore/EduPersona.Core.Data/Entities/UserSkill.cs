using System.ComponentModel.DataAnnotations.Schema;
using EduPersona.Core.Data.Entities.Abstract;

namespace EduPersona.Core.Data.Entities
{
    public class UserSkill : AuditEntity
    {
        public int UserId { get; set; }

        public int SkillId { get; set; }

        [ForeignKey(nameof(SkillId))]
        public Skill? Skill { get; set; }
    }
}