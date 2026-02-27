using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduPersona.Core.Shared.Models.Response
{
    public class ProfileVersionResponse
    {
        public int DesignationVersionId { get; set; }

        public string ProfessionId { get; set; } = null!;
        public string CurrentDesignation { get; set; } = null!;
        public string TargetDesignation { get; set; } = null!;

        public List<string> Skills { get; set; } = new();

        public bool IsActive { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}