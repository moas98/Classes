using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training_Courses.Models.Entities;
using Training_Courses.Models.Interfaces;
using Training_Courses.Models.RequestDTO;
using Training_Courses.Models.ResponseDTO;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Training_Courses.Models.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;
    

        public StudentRepository(AppDbContext dbContext, IMapper mapper )
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
         
        }
        public Students AddStudentMark(StudentTheFinalGradeDTO NewStu,int StuId, out String ErrorCode)
        {
            ErrorCode = "";
            var curentStudent = dbContext.Students.Where(x => x.StudentId == StuId).SingleOrDefault();
            if (curentStudent==null)
            {
                ErrorCode = "Stu004";
                return null;
            }
            var CurStu = dbContext.Students.Where(x => x.StudentId == StuId).SingleOrDefault();
            CurStu.Mark = NewStu.Mark;
            SaveChanges();
            return CurStu;
        }
        public Students AddStudent(StudentAddRequestDTO NewStu, out string ErrorCode)
        {
            //check Student Name duplication
            ErrorCode = "";
           
            var CurStuGender = NewStu.Gender;
           
           
            if (CurStuGender!=Gender.Female && CurStuGender!=Gender.Male)
            {
                ErrorCode = "Stu006";
                return null;
            }
            var ClassId = dbContext.Classes.Any(x => x.ClassesId == NewStu.ClassId && x.IsDeleted == false);
            if (NewStu==null)
            {
                ErrorCode = "Stu004";
                return null;
            }
            if (!ClassId)
            {
                ErrorCode = "Stu005";
                return null;
            }
            if (dbContext.Students.Where(x => x.Class.ClassesId == NewStu.ClassId && x.Class.IsDeleted == false).Any(x=>x.StudentFullName==NewStu.StudentFullName))
            {
                ErrorCode = "Stu002";
                return null;

            }
            //Mapping
            var CurStu = mapper.Map<Students>(NewStu);
            CurStu = dbContext.Students.Add(CurStu).Entity;
            SaveChanges();
            return CurStu;
        }
        public void DeleteStudent(int StudentId, out string ErrorCode)
        {
            ErrorCode = "";
            var CurStu = dbContext.Students.Where(x => x.StudentId == StudentId).SingleOrDefault();
            var CurStuId = dbContext.Students.Any(x => x.StudentId == StudentId);
            
            if (!CurStuId)
            {
                ErrorCode = "Stu004";
                return;
            }
            if (CurStu == null)
            {
                ErrorCode = "Stu001";
                return;
            }
            dbContext.Remove(CurStu);
            SaveChanges();
        }

        public PagedResponse<StudentsResponseDTO> GetAll(int ClassId, IUrlHelper Url, string FilterStuName,
                                                            string FilterPhone,
                                                            string orderby, PagingDTO paging)
        {
           
            var StuQuery = dbContext.Students.Include(x => x.Images).Include(x=>x.Absences).Include(x => x.Installment).Include(s=>s.Class).AsQueryable();
            if (String.IsNullOrWhiteSpace(FilterStuName)|| String.IsNullOrWhiteSpace(FilterPhone)|| String.IsNullOrWhiteSpace(orderby))
            {
                StuQuery = dbContext.Students.Include(x => x.Images).Include(x => x.Absences).Include(x => x.Installment).Include(s => s.Class).AsQueryable();
            }

            if (!String.IsNullOrWhiteSpace(FilterStuName))
            {
                StuQuery = StuQuery.Where(x => x.StudentFullName==FilterStuName);
            }

            if (!String.IsNullOrWhiteSpace(FilterPhone))
            {
                StuQuery = StuQuery.Where(x => x.PhoneNumber.Contains(FilterPhone, StringComparison.OrdinalIgnoreCase));
            }


            if (!String.IsNullOrWhiteSpace(orderby))
            {
                StuQuery = StuQuery.OrderBy(orderby);
            }
            else
                StuQuery = StuQuery.OrderBy(x => x.StudentFullName);


            var ResponseDTO = StuQuery.Select(p => mapper.Map<StudentsResponseDTO>(p));

            var pagedResponse = new PagedResponse<StudentsResponseDTO>(ResponseDTO, paging);

            if (pagedResponse.Paging.HasNextPage)
                pagedResponse.Paging.NextPageURL = Url.Link("GetAllStudents", new
                {
                    FilterStuName,
                    FilterPhone,
                   
                    orderby,
                    paging.RowCount,
                    PageNumber = paging.PageNumber + 1
                });
            if (pagedResponse.Paging.HasPrevPage)
                pagedResponse.Paging.PrevPageURL = Url.Link("GetAllStudents", new
                {
                    FilterStuName,
                    FilterPhone,
                   
                    orderby,
                    paging.RowCount,
                    PageNumber = paging.PageNumber - 1
                });

            return pagedResponse;
        }

        public StudentsResponseByIdDTO GetById(int Id, out string ErrorCode)
        {
            ErrorCode = "";
            Students CurStudent = dbContext.Students.Include(x => x.Images).Include(x => x.Absences).Include(x => x.Installment).Include(z=>z.Class).Where(x => x.StudentId == Id ).FirstOrDefault();
            if (CurStudent == null)
            {
                ErrorCode = "Stu001";
                return null;
            }
            return mapper.Map<StudentsResponseByIdDTO>(CurStudent);
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        public void UpdateStudent(int StuId, StudentUpdateRequestDTO newStudent, out string ErrorCode)
        {
            ErrorCode = "";
            if (String.IsNullOrWhiteSpace(newStudent.StudentFullName))
            {
                ErrorCode = "Stu003";
                return;
            }

            var CurStu = dbContext.Students.Where(x => x.StudentId == StuId).SingleOrDefault();
            var CurStuClassId = dbContext.Students.Where(x => x.ClassId == newStudent.ClassId).SingleOrDefault();
            if (CurStuClassId==null)
            {
                ErrorCode = "Class001";
                return;
            }
            if (CurStu == null)
            {
                ErrorCode = "Stu001";
                return;
            }
            if (newStudent == null)
            {
                ErrorCode = "Stu004";
                return;
            }
            mapper.Map(newStudent, CurStu);
            SaveChanges();
        }

        public Students UpdateStudentPartially(int StuId, JsonPatchDocument StuPatch, out string ErrorCode)
        {
            ErrorCode = "";
            var CurStu = dbContext.Students.Where(x => x.StudentId == StuId).SingleOrDefault();
            if (CurStu == null)
            {
                ErrorCode = "Stu001";
                return null;
            }
            
            if (StuPatch == null)
            {
                ErrorCode = "Stu004";
                return null;
            }

            StuPatch.ApplyTo(CurStu);
            return CurStu;
        }
        
    }
}
