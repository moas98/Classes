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
    public interface IClassRepository
    {
        PagedResponse<ClassResponseDTO> GetAll(IUrlHelper Url, String FilterClassName, String orderby, PagingDTO paging);
        ClassResponseDTO GetById(int Id, out String ErrorCode);
        Classes AddClass(ClassAddRequestDTO NewClass, out String ErrorCode);
        void UpdateClass(int ClassId, ClassUpdateRequestDTO newClass, out String ErrorCode);
        Classes UpdateClassPartially(int ClassId, JsonPatchDocument ClassPatch, out String ErrorCode);
        void DeleteClass(int ClassId, out string ErrorCode);
        int SaveChanges();
    }
}
