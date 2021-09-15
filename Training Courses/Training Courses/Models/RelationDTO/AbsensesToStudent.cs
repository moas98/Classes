using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training_Courses.Models.RelationDTO
{
    public class AbsensesToStudent
    {
        public int AbsenceId { get; set; }
        public DateTime DateTime { get; set; }
        public Boolean StuAbsence { get; set; }
    }
}
