using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduPersona.Core.Shared.Models.Request
{
    public class ChangeDesignationRequest
    {

        [Range(1, int.MaxValue)]
        public int CurrentDesignationId { get; set; }

        [Range(1, int.MaxValue)]
        public int TargetDesignationId { get; set; }
    }
}