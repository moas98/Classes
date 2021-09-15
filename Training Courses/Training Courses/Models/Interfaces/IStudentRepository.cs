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
    public interface IStudentRepository
    {
        PagedResponse<StudentsResponseDTO> GetAll(int ClassId, IUrlHelper Url, String FilterStudentName, String FilterPhone,  String orderby, PagingDTO paging);
        StudentsResponseByIdDTO GetById(int Id, out String ErrorCode);
        Students AddStudent(StudentAddRequestDTO NewStu, out String ErrorCode);
        Students AddStudentMark(StudentTheFinalGradeDTO NewStu,int StuId, out String ErrorCode);
        void UpdateStudent(int StudentId, StudentUpdateRequestDTO newStu, out String ErrorCode);
        Students UpdateStudentPartially(int StudentId, JsonPatchDocument StuPatch, out String ErrorCode);
        void DeleteStudent(int StudentId, out string ErrorCode);
        int SaveChanges();
    }
}
