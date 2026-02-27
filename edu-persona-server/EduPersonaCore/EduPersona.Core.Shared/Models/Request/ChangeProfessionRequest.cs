using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static EduPersona.Core.Shared.Constants.Messages;

namespace EduPersona.Core.Shared.Models.Request
{
    public class ChangeProfessionRequest
    {
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