using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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
    [Route("api/Class/Students/Absence")]
    [ApiController]
    public class AbsenceController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IErrorClass error;
        private readonly IAbsenceRepository absenceRep;
        private readonly AppDbContext context;

        public AbsenceController(IMapper Mapper,
                                IErrorClass error,
                                IAbsenceRepository absenceRep,
                                AppDbContext context)
        {
            mapper = Mapper;
            this.error = error;
            this.absenceRep = absenceRep;
            this.context = context;
        }
        [HttpGet(Name = "GetAllStudentsAbsence")]

        public IActionResult GetAllStudentsAbsence([FromQuery] 
                                           String orderby,DateTime? date,
                                           [FromQuery] PagingDTO paging)
        {

            var Result = absenceRep.GetAllStudentAbsence(Url,date, orderby, paging);

            return Ok(Result);
        }
        [HttpGet("{Id}")] //Api/Absence/
        public IActionResult GetStudentAbsenceById(int Id)
        {
            String ErrorCode = "";
            var result = absenceRep.GetStudentAbsenceById(Id, out ErrorCode);
            if (!String.IsNullOrWhiteSpace(ErrorCode))
            {
                error.LoadError(ErrorCode);
                ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
                return ValidationProblem();
            }

            return Ok(result);

        }
        [HttpPost("{ClassId}")]
        public IActionResult AddStudentAbsence([FromBody] List<AbsenceAddRequestDTO> NewStu, int ClassId)
        {
            String ErrorCode = "";
            var result = absenceRep.AddStudentAbsence(NewStu,ClassId, out ErrorCode);
            //check Student Name duplication
            if (!String.IsNullOrWhiteSpace(ErrorCode))
            {
                error.LoadError(ErrorCode);
                ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
                return ValidationProblem();

            }

            return Ok(new { objofStudentAbsence = NewStu, Massage = "Students Absence Added Secssesfuly" });

        }
        [HttpPut("{StuId}")]
        public IActionResult UpdateStudentAbsence(int StuId, AbsenceUpdateRequestDTO newStu)
        {
            String ErrorCode = "";
            absenceRep.UpdateStudentAbsence(StuId, newStu, out ErrorCode);
            if (!String.IsNullOrWhiteSpace(ErrorCode))
            {
                error.LoadError(ErrorCode);
                ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
                return ValidationProblem();

            }

            return  Ok(new { objOfPut = newStu, Massage = $"Updated is successfuly where Student Absence Id = {StuId}" });
        }
        [HttpPatch("{StuId}")]
        public IActionResult UpdateStudentAbsencePartially(int StuId, JsonPatchDocument StuPatch)
        {
            String ErrorCode = "";
            var CurStudent = absenceRep.UpdateStudentAbsencePartially(StuId, StuPatch, out ErrorCode);
            if (!String.IsNullOrWhiteSpace(ErrorCode))
            {
                error.LoadError(ErrorCode);
                ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
                return ValidationProblem();
            }
            //check Cur Student Validation
            if (!TryValidateModel(CurStudent))
                return ValidationProblem();

            absenceRep.SaveChanges();
            return Ok(new { objOfPut = StuPatch, Massage = $"Updated is successfuly where Student Absence Id = {StuId}" });

        }      
    }
}
