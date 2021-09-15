using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Training_Courses.Models.Entities
{
    public class Absence
    {
        public int AbsenceId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public Boolean StuAbsence { get; set; }
        [ForeignKey("StudentId")]
        public Students Student { get; set; }
        [ForeignKey("ClassId")]
        public Classes Class { get; set; }
  }    
}
