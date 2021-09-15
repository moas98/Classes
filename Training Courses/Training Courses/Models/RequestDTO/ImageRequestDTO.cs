using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Training_Courses.Models.RequestDTO
{
    public class ImageRequestDTO
    {
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public int StudentId { get; set; }
    }
}
