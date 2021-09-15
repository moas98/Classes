using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Training_Courses.Models.Entities;

namespace Training_Courses.Models.RequestDTO
{
    public class AbsenceUpdateRequestDTO
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int ClassId { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public Boolean StuAbsence { get; set; }
    }
}
