using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Training_Courses.Models.Entities;
using Training_Courses.Models.RelationDTO;

namespace Training_Courses.Models.ResponseDTO
{
    public class StudentsResponseDTO
    {

     
        public int StudentId { get; set; }
        public String StudentFullName { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public String ImagePath { get; set; }
        public String ClassName { get; set; }
        public Boolean InstallmentStatus { get; set; }
        public int InstallmentsCount { get; set; }
        public int AbsensesCount { get; set; }



    }
}
