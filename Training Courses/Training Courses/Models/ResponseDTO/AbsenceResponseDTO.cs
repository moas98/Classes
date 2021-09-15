using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Training_Courses.Models.Entities;

namespace Training_Courses.Models.ResponseDTO
{
    public class AbsenceResponseDTO
    {
        public int AbsenceId { get; set; }

        public String StudentName { get; set; }

        public int ClassID { get; set; }

        public DateTime DateTime { get; set; }

        public Boolean StuAbsence { get; set; }
    }
}
