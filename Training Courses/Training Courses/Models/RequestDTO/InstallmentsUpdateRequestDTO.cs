using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Training_Courses.Models.Entities;

namespace Training_Courses.Models.RequestDTO
{
    public class InstallmentsUpdateRequestDTO
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int StudentPay { get; set; }
        [Required]
        public int ClassId { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

    }
}
