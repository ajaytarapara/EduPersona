using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduPersona.Core.Shared.Models.Request
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
    }
}