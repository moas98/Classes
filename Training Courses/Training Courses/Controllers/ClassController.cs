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
    [Route("api/Class")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IErrorClass error;
        private readonly AppDbContext dbContext;
        private readonly IClassRepository classRep;

        public ClassController(IMapper Mapper,
                                IErrorClass error, AppDbContext dbContext,
                                IClassRepository classRep)
        {
            mapper = Mapper;
            this.error = error;
            this.dbContext = dbContext;
            this.classRep = classRep;
        }
 
      
        [HttpGet(Name = "GetAllClasses")]
        public IActionResult GetAllClasses([FromQuery] String FilterClassName,
                                            String orderby,
                                            [FromQuery] PagingDTO paging)
        {
            var Result = classRep.GetAll(Url, FilterClassName, orderby, paging);

            return Ok(Result);
        }
        [HttpGet("{Id}")] 
        public IActionResult GetClassById(int Id)
        {
            String ErrorCode = "";
            var result = classRep.GetById(Id, out ErrorCode);
            if (!String.IsNullOrWhiteSpace(ErrorCode))
            {
                error.LoadError(ErrorCode);
                ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
                return ValidationProblem();
            }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddClass([FromBody] ClassAddRequestDTO NewClass)
        {
            String ErrorCode = "";
            var result = classRep.AddClass(NewClass, out ErrorCode);
            //check Class Name duplication
            if (!String.IsNullOrWhiteSpace(ErrorCode))
            {
                error.LoadError(ErrorCode);
                ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
                return ValidationProblem();
            }
            return Ok(new { objofClass = NewClass, Massage = "Class Added Secssesfuly" }); ;
        }
        [HttpPut("{ClassId}")]
        public IActionResult UpdateClass(int ClassId, ClassUpdateRequestDTO newClass)
        {
            String ErrorCode = "";
            classRep.UpdateClass(ClassId, newClass, out ErrorCode);
            if (!String.IsNullOrWhiteSpace(ErrorCode))
            {
                error.LoadError(ErrorCode);
                ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
                return ValidationProblem();

            }

            return Ok(new { objOfPut = newClass , Massage = $"Updated is successfuly  where ClassId ={ClassId}" });
        }
        [HttpPatch("{ClassId}")]
        public IActionResult UpdateClassPartially(int ClassId,[FromBody] JsonPatchDocument ClassPatch)
        {
            String ErrorCode = "";
            var CurClass = classRep.UpdateClassPartially(ClassId, ClassPatch, out ErrorCode);
            if (!String.IsNullOrWhiteSpace(ErrorCode))
            {
                error.LoadError(ErrorCode);
                ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
                return ValidationProblem();
            }
            //check Cur Student Validation
            if (!TryValidateModel(CurClass))
                return ValidationProblem();

            classRep.SaveChanges();
            return Ok(new { objOfPatch = CurClass, Massage = "Updated is successfuly" });
        }
        [HttpDelete("{ClassId}")]
        public IActionResult DeleteClass(int ClassId)
        {
            String ErrorCode = "";
            classRep.DeleteClass(ClassId, out ErrorCode);
            if (!String.IsNullOrWhiteSpace(ErrorCode))
            {
                error.LoadError(ErrorCode);
                ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
                return ValidationProblem();
            }
            return Ok(new { Status = $"Class deleted where Id={ClassId} successfully" });
        }
    }
}
