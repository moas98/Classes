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

namespace Training_Courses.Models.Repositories
{
    public class AbsenceRepository : IAbsenceRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public AbsenceRepository(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public ICollection<Absence> AddStudentAbsence(List<AbsenceAddRequestDTO> NewStuAbsence,int ClassId, out string ErrorCode)
        {
            List<DateTime> stuAbsenceDate = dbContext.Absences.Select(d=>d.DateTime).ToList();
            List<DateTime> stuAbsenceDateReq = NewStuAbsence.Select(d=>d.DateTime).ToList();
            //---------------------------------------------------------------------------------------

            List<int> studentsId = dbContext.Students.Select(s => s.StudentId).ToList();
            List<int> studentsIdReq = NewStuAbsence.Select(s => s.StudentId).ToList();
            //---------------------------------------------------------------------------------------  
           List<int> StuClass = dbContext.Students.Where(x=>x.ClassId==ClassId).Select(s=>s.StudentId).ToList();
            //---------------------------------------------------------------------------------------
            List<int> StudentAbcenceState =dbContext.Absences.Where(x=>x.StuAbsence==true).Select(s=>s.StudentId).ToList();
            
            //var stuTrue = StudentAbcenceState.SequenceEqual(studentsIdReq);
            var studentIdNotinDbStudentId = studentsIdReq.Except(studentsId).ToList();
            var missStudent = StuClass.Except(studentsIdReq).ToList();
           var StudentAllCome = studentsIdReq.Except(StuClass).ToList();
           // var DateinDb = stuAbsenceDateReq.SequenceEqual(stuAbsenceDate);
            ErrorCode = "";
            if (StudentAllCome.Any())
            {
                ErrorCode = "Absence007";
                return null;
            }
            if (missStudent.Any())
            {
                ErrorCode = "Absence007";
                return null;
            }
           
            if (studentIdNotinDbStudentId.Any())
            {
                ErrorCode = "Absence006";
                return null;
            }

            //if (DateinDb && stuTrue)
            //{
            //    ErrorCode = "Absence004";
            //    return null;
            //}
            //Mapping
            var stuAbsence= mapper.Map<ICollection<Absence>>(NewStuAbsence);
            dbContext.Absences.AddRange(stuAbsence);
            
            SaveChanges();
            return stuAbsence;
        }

        public PagedResponse<AbsenceResponseDTO> GetAllStudentAbsence(IUrlHelper Url, DateTime? date, string orderby, PagingDTO paging)
        {
            var stuAbsenceQuery = dbContext.Absences.Include(s=>s.Student).AsQueryable();
            if (date==null|| String.IsNullOrWhiteSpace(orderby))
            {
                stuAbsenceQuery = dbContext.Absences.Include(s => s.Student).AsQueryable();
            }
            if (date!=null)
            {
                stuAbsenceQuery = stuAbsenceQuery.Where(x=>x.DateTime==date);
            }
            

             if (!String.IsNullOrWhiteSpace(orderby))
            {
                stuAbsenceQuery = stuAbsenceQuery.OrderBy(orderby);
            }
            else
                stuAbsenceQuery = stuAbsenceQuery.OrderBy(x => x.DateTime);


            var ResponseDTO = stuAbsenceQuery.Select(p => mapper.Map<AbsenceResponseDTO>(p));

            var pagedResponse = new PagedResponse<AbsenceResponseDTO>(ResponseDTO, paging);

            if (pagedResponse.Paging.HasNextPage)
                pagedResponse.Paging.NextPageURL = Url.Link("GetAllStudentsAbsence", new
                {
                    date,
                    orderby,
                    paging.RowCount,
                    PageNumber = paging.PageNumber + 1
                });
            if (pagedResponse.Paging.HasPrevPage)
                pagedResponse.Paging.PrevPageURL = Url.Link("GetAllStudentsAbsence", new
                {
                    date,
                    orderby,
                    paging.RowCount,
                    PageNumber = paging.PageNumber - 1
                });

            return pagedResponse;
        }

        public AbsenceResponseDTO GetStudentAbsenceById(int Id, out string ErrorCode)
        {
            ErrorCode = "";
            Absence CurStuAbsence = dbContext.Absences.Include(s => s.Student)
                .Include(c => c.Class).Where(x => x.AbsenceId == Id).FirstOrDefault();
            if (CurStuAbsence == null)
            {
                ErrorCode = "Absence003";
                return null;
            }
            return mapper.Map<AbsenceResponseDTO>(CurStuAbsence);
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        public void UpdateStudentAbsence(int StudenAbsencetId, AbsenceUpdateRequestDTO newStu, out string ErrorCode)
        {
            var CurStu = dbContext.Absences.Where(x => x.AbsenceId == StudenAbsencetId).SingleOrDefault();
            ErrorCode = "";
            
            
            if (CurStu==null)
            {
                ErrorCode = "Absence001";
                return;
            }
            if (newStu==null)
            {
                ErrorCode = "Absence002";
                return;
            }
            mapper.Map(newStu, CurStu);
            SaveChanges();
        }

        public Absence UpdateStudentAbsencePartially(int StudenAbsencetId, JsonPatchDocument StuPatch, out string ErrorCode)
        {
            ErrorCode = "";
            var CurStuAbsence = dbContext.Absences.Where(x => x.AbsenceId == StudenAbsencetId).SingleOrDefault();
            if (CurStuAbsence == null)
            {
                ErrorCode = "Absence001";
                return null;
            }
            if (StuPatch==null)
            {
                ErrorCode = "Absence002";
                return null;
            }
            StuPatch.ApplyTo(CurStuAbsence);
            return CurStuAbsence;
        }
    }
}
