using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training_Courses.Models;
using Training_Courses.Models.Helper;
using Training_Courses.Models.Interfaces;
using Training_Courses.Models.RequestDTO;
using Training_Courses.Models.ResponseDTO;

namespace Training_Courses.Controllers
{
    [Route("api/Class/Students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IErrorClass error;
        private readonly AppDbContext dbContext;
        private readonly IStudentRepository stuRep;

        public StudentController(IMapper Mapper,
                                IErrorClass error, AppDbContext dbContext,
                                IStudentRepository stuRep)
        {
            mapper = Mapper;
            this.error = error;
            this.dbContext = dbContext;
            this.stuRep = stuRep;
        }
        [HttpGet(Name = "GetAllStudents")]
        
        public IActionResult GetAllStudents([FromQuery] String FilterStuName,
                                            String FilterPhone,String orderby,
                                            [FromQuery] PagingDTO paging,int ClassId)
        {
            var Result = stuRep.GetAll(ClassId,Url, FilterStuName, FilterPhone,  orderby, paging);

            return Ok(Result);
        }
        [HttpGet("{Id}")] //Api/Student/
        public IActionResult GetStudentById(int Id)
        {
            String ErrorCode = "";
            var result = stuRep.GetById(Id, out ErrorCode);
            if (!String.IsNullOrWhiteSpace(ErrorCode))
            {
                error.LoadError(ErrorCode);
                ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
                return ValidationProblem();
            }

            return Ok(result);

        }
        
        [HttpPost("{StuId}")]
        public IActionResult AddStudentMark([FromBody] StudentTheFinalGradeDTO NewStu,int StuId)
        {
            string ErrorCode = "";
            var result = stuRep.AddStudentMark(NewStu,StuId, out ErrorCode);
            //check Student Name duplication
            if (!String.IsNullOrWhiteSpace(ErrorCode))
            {
                error.LoadError(ErrorCode);
                ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
                return ValidationProblem();

            }
            mapper.Map<StudentTheFinalGradeResponseDTO>(result);
            return Ok(new { objofStudent = NewStu, Massage = "Students Mark Added Secssesfuly" });

        }
        [HttpPost]
        public IActionResult AddStudent([FromBody] StudentAddRequestDTO NewStu)
        {
            
            string ErrorCode = "";
            var result = stuRep.AddStudent(NewStu, out ErrorCode);
            //check Student Name duplication
            if (!String.IsNullOrWhiteSpace(ErrorCode))
            {
                error.LoadError(ErrorCode);
                ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
                return ValidationProblem();

            }
            mapper.Map<StudentsResponseDTO>(result);
            return Ok(new {objofStudent= NewStu , Massage="Students Added Secssesfuly" });

        }
        [HttpPut("{StuId}")]
        public IActionResult UpdateStudent(int StuId, StudentUpdateRequestDTO newStu)
        {
            String ErrorCode = "";
            stuRep.UpdateStudent(StuId, newStu, out ErrorCode);
            if (!String.IsNullOrWhiteSpace(ErrorCode))
            {
                error.LoadError(ErrorCode);
                ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
                return ValidationProblem();

            }

            return Ok(new { ObjectFromStudents = newStu, Message = "Update has been successfuly" });
        }
        [HttpPatch("{StuId}")]
        public IActionResult UpdateStudentPartially(int StuId, JsonPatchDocument StuPatch)
        {
            String ErrorCode = "";
            var CurStudent = stuRep.UpdateStudentPartially(StuId, StuPatch, out ErrorCode);
            if (!String.IsNullOrWhiteSpace(ErrorCode))
            {
                error.LoadError(ErrorCode);
                ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
                return ValidationProblem();
            }
            //check Cur Student Validation
            if (!TryValidateModel(CurStudent))
                return ValidationProblem();

            stuRep.SaveChanges();
            return Ok(new { ObjectFromStudents = CurStudent, Message = "Update has been successfuly" });

        }
        [HttpDelete("{StuId}")]
        public IActionResult DeleteStudent(int StuId)
        {

            String ErrorCode = "";
            stuRep.DeleteStudent(StuId, out ErrorCode);
            if (!String.IsNullOrWhiteSpace(ErrorCode))
            {
                error.LoadError(ErrorCode);
                ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
                return ValidationProblem();
            }
            return Ok(new { Status = $"Student deleted where Id={StuId} successfully" });
        }
    }
}
