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
        public DateTimeOffset? Birthdate { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum length of address is 50 char.")]
        public string? Address { get; set; }

        [MaxLength(10, ErrorMessage = "Phone no. length must be 10.")]
        [MinLength(10, ErrorMessage = "Phone no. length must be 10.")]
        public string? PhoneNo { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = ModelStateMessage.CurrentDesignationRequired)]
        public int CurrentDesignationId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = ModelStateMessage.TargetDesignationRequired)]
        public int TargetDesignationId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = ModelStateMessage.ProfessionRequired)]
        public int ProfessionId { get; set; }

        [MinLength(1, ErrorMessage = ModelStateMessage.SkillRequired)]
        public List<int> SkillIds { get; set; }

    }
}