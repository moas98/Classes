using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training_Courses.Models.Entities;
using Training_Courses.Models.RequestDTO;
using Training_Courses.Models.ResponseDTO;

namespace Training_Courses.Models.Interfaces
{
    public interface IinstallmentsRepository
    {
        PagedResponse<InstallmentsResponseDTO> GetAll(IUrlHelper Url, String FilterStudentName, int FilterStudentId, String orderby, PagingDTO paging);
        InstallmentsResponseDTO GetById(int Id, out String ErrorCode);
        Installments AddStudentPayment(InstallmentsAddRequestDTO NewStuInstallment, out String ErrorCode);
        void UpdateStudentInstallment(int StudentId, InstallmentsUpdateRequestDTO newStuInstallment, out String ErrorCode);
        Installments UpdateStudentInstallmentPartially(int StudentId, JsonPatchDocument StuInstallmentPatch, out String ErrorCode);
        void DeleteStuInstallment(int InstallmentId, out string ErrorCode);
        int SaveChanges();
    }
}
