using System.ComponentModel.DataAnnotations.Schema;
using EduPersona.Core.Data.Entities.Abstract;

namespace EduPersona.Core.Data.Entities
{
    public class UserDesignation : AuditEntity
    {
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        public int CurrentDesignationId { get; set; }

        [ForeignKey(nameof(CurrentDesignationId))]
        public Designation? CurrentDesignation { get; set; }

        public int TargetDesignationId { get; set; }

        [ForeignKey(nameof(TargetDesignationId))]
        public Designation? TargetDesignation { get; set; }

        public bool IsCurrent { get; set; }
    }
}