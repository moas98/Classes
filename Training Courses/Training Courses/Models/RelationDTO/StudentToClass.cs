using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training_Courses.Models.ResponseDTO
{
    public class StudentToClass
    {
        public int StudentId { get; set; }
        public String StudentFullName { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public Gender Gender { get; set; }

    }
}
