using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Training_Courses.Models.Entities;

namespace Training_Courses.Models.ResponseDTO
{
    public class ClassResponseDTO
    {
      
        public int ClassesId{ get; set; }
        
        public String ClassName { get; set; }
       
        public int Number_of_Days { get; set; }
       
        public int Course_price { get; set; }
        public List<StudentToClass> Students { get; set; }


    }
}
