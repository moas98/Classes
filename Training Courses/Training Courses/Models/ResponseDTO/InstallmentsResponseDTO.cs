using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training_Courses.Models.Entities;

namespace Training_Courses.Models.ResponseDTO
{
    public class InstallmentsResponseDTO
    {
        public int InstallmentsId { get; set; }
        public String StudentFullName { get; set; }
        public String ClassName { get; set; }
        public int StudentPay { get; set; }
        public int ClassCost { get; set; }
        public DateTime PaymentDate { get; set; }

    }
}
