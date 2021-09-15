using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Training_Courses.Models.RequestDTO
{
    public class StudentUpdateRequestDTO
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 8)]
        public String StudentFullName { get; set; }
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public IFormFile StudentImage { get; set; }
        [Required]
        [Phone]
        public String PhoneNumber { get; set; }
        [Required]
        public int? ClassId { get; set; }
    }
}
