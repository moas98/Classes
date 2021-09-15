using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Training_Courses.Models.Entities
{
    public class Classes
    {
        [Key]
        public int ClassesId { get; set; }
        [StringLength(maximumLength:80,MinimumLength=4)]
        public String ClassName { get; set; }
        [Required]
        public int Number_of_Days { get; set; }
        [Required]
        public int Course_price { get; set; }
        public Boolean IsDeleted { get; set; }
        public List<Students> Students { get; set; }
        
    }
}
