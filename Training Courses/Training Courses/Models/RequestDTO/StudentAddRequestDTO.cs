using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Training_Courses.Models.Entities;

namespace Training_Courses.Models.RequestDTO
{
    public class StudentAddRequestDTO
    {
        [Required]
        public String StudentFullName { get; set; }
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [Phone]
        public String PhoneNumber { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public int ClassId { get; set; }



    }
}
