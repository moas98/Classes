using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Training_Courses.Models.Entities
{
    public class Installments
    {
        public int InstallmentsId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int StudentPay { get; set; }
        [Required]
        public int ClassId { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        public int ClassCost { get; set; }
        public Students Student { get; set; }
        public Classes Class { get; set; }
      
    
    }
}
