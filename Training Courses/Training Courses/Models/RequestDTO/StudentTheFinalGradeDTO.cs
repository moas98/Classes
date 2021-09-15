using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Training_Courses.Models.RequestDTO
{
    public class StudentTheFinalGradeDTO
    {
        [Required]
        public String Mark { get; set; }
    }
}
