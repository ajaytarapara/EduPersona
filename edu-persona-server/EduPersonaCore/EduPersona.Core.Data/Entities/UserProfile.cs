using EduPersona.Core.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPersona.Core.Data.Entities
{
    public class UserProfile : AuditEntity
    {
        public int? userId { get; set; }

        public DateOnly? Birthdate { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        [MaxLength(10)]
        public int? PhoneNo { get; set; }

        public bool IsProfileCompleted { get; set; } = false;

    }
}
