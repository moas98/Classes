using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training_Courses.Models.RelationDTO
{
    public class InstallmentToStudent
    {
        public int InstallmentsId { get; set; }
        public int StudentPay { get; set; }
        public DateTime PaymentDate { get; set; }
        public int ClassCost { get; set; }

    }
}
