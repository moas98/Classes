using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Training_Courses.Models.Entities;

namespace Training_Courses.Models.RequestDTO
{
    public class ClassAddRequestDTO
    {
        [Required]
        [StringLength(maximumLength: 80, MinimumLength = 4)]
        public String ClassName { get; set; }
        [Required]
        public int Number_of_Days { get; set; }
        [Required]
        public int Course_price { get; set; }
    }
}
