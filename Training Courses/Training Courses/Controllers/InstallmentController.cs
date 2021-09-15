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
    [Route("api/Class/Students/Installment")]
    [ApiController]
    public class InstallmentController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IErrorClass error;
        private readonly IinstallmentsRepository installmentRep;

        public InstallmentController(IMapper Mapper,
                                IErrorClass error,
                                IinstallmentsRepository installmentRep)
        {
            mapper = Mapper;
            this.error = error;
            this.installmentRep = installmentRep;
        }
        [HttpGet(Name = "GetAllStudentsInstallment")]

        public IActionResult GetAllStudentsInstallment([FromQuery]String FilterStudentName,
                                            int FilterStudentId,
                                            String orderby,
                                            [FromQuery] PagingDTO paging)
        {

            var Result = installmentRep.GetAll(Url,FilterStudentName, FilterStudentId, orderby, paging);

            return Ok(Result);
        }
        [HttpGet("{Id}")] 
        public IActionResult GetStudentInstallmentById(int Id)
        {
            String ErrorCode = "";
            var result = installmentRep.GetById(Id, out ErrorCode);
            if (!String.IsNullOrWhiteSpace(ErrorCode))
            {
                error.LoadError(ErrorCode);
                ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
                return ValidationProblem();
            }

            return Ok(result);

        }
        [HttpPost]
        public IActionResult AddStudentPayment([FromBody] InstallmentsAddRequestDTO NewStuStuInstallment)
        {
            String ErrorCode = "";
            var result = installmentRep.AddStudentPayment(NewStuStuInstallment, out ErrorCode);
            
            if (!String.IsNullOrWhiteSpace(ErrorCode))
            {
                error.LoadError(ErrorCode);
                ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
                return ValidationProblem();

            }
            mapper.Map<InstallmentsResponseDTO>(result);
            return Ok(new { objadded= NewStuStuInstallment , Massage = "added successfuly" })
                //nameof(GetStudentInstallmentById)
                ;

        }
        [HttpPut("{StuIdInstallment}")]
        public IActionResult UpdateStudentInstallment(int StuIdInstallment, InstallmentsUpdateRequestDTO newStuInstallment)
        {
            String ErrorCode = "";
            installmentRep.UpdateStudentInstallment(StuIdInstallment, newStuInstallment, out ErrorCode);
            if (!String.IsNullOrWhiteSpace(ErrorCode))
            {
                error.LoadError(ErrorCode);
                ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
                return ValidationProblem();

            }

            return Ok(new { objOfPut = newStuInstallment, Massage = $"Updated is successfuly where Student Installment Id = {StuIdInstallment}" });
        }
        [HttpPatch("{StuIdInstallment}")]
        public IActionResult UpdateStudentInstallmentPartially(int StuIdInstallment, JsonPatchDocument StuInstallmentPatch)
        {
            String ErrorCode = "";
            var CurStudentInstallment = installmentRep.UpdateStudentInstallmentPartially(StuIdInstallment, StuInstallmentPatch, out ErrorCode);
            if (!String.IsNullOrWhiteSpace(ErrorCode))
            {
                error.LoadError(ErrorCode);
                ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
                return ValidationProblem();
            }
            //check Cur Student Validation
            if (!TryValidateModel(CurStudentInstallment))
                return ValidationProblem();

            installmentRep.SaveChanges();
            return Ok(new { objOfPut = CurStudentInstallment, Massage = $"Updated is successfuly where Student Installment Id = {StuIdInstallment}" });

        }
        [HttpDelete("{installmentId}")]
        public IActionResult DeleteClass(int installmentId)
        {
            String ErrorCode = "";
            installmentRep.DeleteStuInstallment(installmentId, out ErrorCode);
            if (!String.IsNullOrWhiteSpace(ErrorCode))
            {
                error.LoadError(ErrorCode);
                ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
                return ValidationProblem();
            }
            return Ok(new { Status = $"Installment deleted where installmentId Id={installmentId} successfully" });
        }
    }
}
