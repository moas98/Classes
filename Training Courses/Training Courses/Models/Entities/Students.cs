using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Training_Courses.Models.Entities
{
    public class Students
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        [StringLength(maximumLength:20,MinimumLength =8)]
        public String StudentFullName { get; set; }
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        [Phone]
        public String PhoneNumber { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public String Mark { get; set; }
        public int ClassId { get; set; }
        public Classes Class { get; set; }
        public Boolean InstallmentStatus { get; set; } 
        public int? InstallmentId { get; set; }
        public List<Installments> Installment { get; set; }
        public List<Absence> Absences { get; set; }
        public Images Images { get; set; }
        public String ImagePath { get; set; }

    }
}
