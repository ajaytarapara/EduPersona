using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduPersona.Core.Shared.Models.Response
{
    public class UserProfileResponse
    {
        public int UserProfileId { get; set; }

        public DateTimeOffset? Birthdate { get; set; }
        public string? Address { get; set; }
        public string? PhoneNo { get; set; }

        public string ProfessionName { get; set; } = null!;
        public string CurrentDesignationName { get; set; } = null!;
        public string TargetDesignationName { get; set; } = null!;

        public List<string> Skills { get; set; } = new();
    }
}