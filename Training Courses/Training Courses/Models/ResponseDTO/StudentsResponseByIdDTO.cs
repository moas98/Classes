using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training_Courses.Models.Entities;
using Training_Courses.Models.RelationDTO;

namespace Training_Courses.Models.ResponseDTO
{
    public class StudentsResponseByIdDTO
    {
        public int StudentId { get; set; }
        public String StudentFullName { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public String Mark { get; set; }
        public int StudentPayment { get; set; }
        public int Remaining_amount { get; set; }
        public Boolean InstallmentStatus { get; set; }
        public List<InstallmentToStudent> Installment { get; set; }
        public List<AbsensesToStudent> Absenses { get; set; }
        public String ImagesPath { get; set; }
    }
}
