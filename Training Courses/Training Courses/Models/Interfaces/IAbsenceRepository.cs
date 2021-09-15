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
    public interface IAbsenceRepository
    {
        PagedResponse<AbsenceResponseDTO> GetAllStudentAbsence(IUrlHelper Url, DateTime? date, String orderby, PagingDTO paging);
        AbsenceResponseDTO GetStudentAbsenceById(int Id, out String ErrorCode);
        ICollection<Absence> AddStudentAbsence(List<AbsenceAddRequestDTO> NewStuAbsence,int ClassId, out String ErrorCode);
        void UpdateStudentAbsence(int StudentId, AbsenceUpdateRequestDTO newStu, out String ErrorCode);
        Absence UpdateStudentAbsencePartially(int StudentId, JsonPatchDocument StuPatch, out String ErrorCode);
        int SaveChanges();
    }
}
