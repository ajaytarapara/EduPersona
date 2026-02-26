using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EduPersona.Core.Shared.Constants;
using static EduPersona.Core.Shared.Constants.Messages;

namespace EduPersona.Core.Shared.Models.Request
{
    public class UserProfileRequest
    {
        public DateOnly? Birthdate { get; set; }

        [MaxLength(50, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "AddressMaxLength")]
        public string? Address { get; set; }

        [MaxLength(50, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PhoneNoMaxLength")]
        public int? PhoneNo { get; set; }

        [Required(ErrorMessage = ModelStateMessage.CurrentDesignationRequired)]
        public int CurrentDesignationId { get; set; }

        [Required(ErrorMessage = ModelStateMessage.TargetDesignationRequired)]
        public int TargetDesignationId { get; set; }

        [Required(ErrorMessage = ModelStateMessage.ProfessionRequired)]
        public int ProfessionId { get; set; }

        [Required(ErrorMessage = ModelStateMessage.SkillRequired)]
        [MinLength(1, ErrorMessage = ModelStateMessage.SkillRequired)]
        public List<int> SkillIds { get; set; }

    }
}