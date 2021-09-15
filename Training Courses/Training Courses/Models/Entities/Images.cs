using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Training_Courses.Models.Entities
{
    public class Images
    {
        [Key]
        public int ImagesId { get; set; }
        public String ImagePath { get; set; }
        [Required]
        public int StudentId { get; set; }
        public Students Student { get; set; }
    }
}
