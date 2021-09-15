using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training_Courses.Models.Entities;
using Training_Courses.Models.Interfaces;
using Training_Courses.Models.RequestDTO;
using Training_Courses.Models.ResponseDTO;
using System.Linq.Dynamic.Core;

namespace Training_Courses.Models.Repositories
{
    public class InstallmentsRepository : IinstallmentsRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public InstallmentsRepository(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public Installments AddStudentPayment(InstallmentsAddRequestDTO NewInstallment, out string ErrorCode)
        {
            //check Installment is Compeleted ? 
            ErrorCode = "";
            var stuId = dbContext.Students.Where(x => x.StudentId == NewInstallment.StudentId).Any(x => x.ClassId == NewInstallment.ClassId);
            var stuClassId = dbContext.Students.Where(x => x.ClassId == NewInstallment.ClassId).Any(x => x.StudentId == NewInstallment.StudentId);

            var sumStudentPay = dbContext.Installments.Where(x=>x.StudentId==NewInstallment.StudentId).Sum(x=>x.StudentPay);
            var classPrice = dbContext.Classes.Where(x => x.ClassesId==NewInstallment.ClassId).Sum(x=>x.Course_price);
            var installmentState = dbContext.Students.Where(s=>s.StudentId==NewInstallment.StudentId).Any(i => i.InstallmentStatus);
            var result = Convert.ToBoolean(installmentState);
            var CurStu = dbContext.Students.Where(x => x.StudentId == NewInstallment.StudentId).SingleOrDefault();
            var Curpay = NewInstallment.StudentPay;
            var AmountPay = Curpay + sumStudentPay;

            if (!stuId && !stuClassId)
            {
                ErrorCode = "Installment004";
                return null;
            }
            if (sumStudentPay.Equals(classPrice))
            {
                result = true;
                installmentState = result;
                CurStu.InstallmentStatus = installmentState;
                SaveChanges();
            }

            if (result)
            {
                ErrorCode = "Installment001";
                return null;
            }
            
            //Mapping
            var CurStuInstallment = mapper.Map<Installments>(NewInstallment);
            CurStuInstallment = dbContext.Installments.Add(CurStuInstallment).Entity;
            SaveChanges();
            if (AmountPay>=(classPrice))
            {
                result = true;
                installmentState = result;
                CurStu.InstallmentStatus = installmentState;
                SaveChanges();
            }

            return CurStuInstallment;
        }

        public PagedResponse<InstallmentsResponseDTO> GetAll(IUrlHelper Url,String FilterStudentName, int FilterStudentId, string orderby, PagingDTO paging)
        {
            var StuInstallmentQuery = dbContext.Installments.Include(s=>s.Student).Include(x => x.Class).AsQueryable();

            if (String.IsNullOrWhiteSpace(FilterStudentName)|| String.IsNullOrWhiteSpace(orderby))
            {
                StuInstallmentQuery = dbContext.Installments.Include(s => s.Student).Include(x => x.Class).AsQueryable();
            }
            if (!String.IsNullOrWhiteSpace(FilterStudentName))
            {
                StuInstallmentQuery = StuInstallmentQuery.OrderBy(FilterStudentName);
            }
            if (!String.IsNullOrWhiteSpace(orderby))
            {
                StuInstallmentQuery = StuInstallmentQuery.OrderBy(orderby);
            }
            else
                StuInstallmentQuery = StuInstallmentQuery.OrderBy(x => x.InstallmentsId);


            var ResponseDTO = StuInstallmentQuery.Select(p => mapper.Map<InstallmentsResponseDTO>(p));

            var pagedResponse = new PagedResponse<InstallmentsResponseDTO>(ResponseDTO, paging);

            if (pagedResponse.Paging.HasNextPage)
                pagedResponse.Paging.NextPageURL = Url.Link("GetAllStudentsInstallment", new
                {
                    FilterStudentName,
                    FilterStudentId,
                    orderby,
                    paging.RowCount,
                    PageNumber = paging.PageNumber + 1
                });
            if (pagedResponse.Paging.HasPrevPage)
                pagedResponse.Paging.PrevPageURL = Url.Link("GetAllStudentsInstallment", new
                {
                    FilterStudentName,
                    FilterStudentId,
                    orderby,
                    paging.RowCount,
                    PageNumber = paging.PageNumber - 1
                });

            return pagedResponse;
        }

        public InstallmentsResponseDTO GetById(int Id, out string ErrorCode)
        {
            ErrorCode = "";
            Installments CurStudentInstallment = dbContext.Installments.Include(x => x.Class).Include(z=>z.Student).Where(x => x.InstallmentsId == Id).FirstOrDefault();
            if (CurStudentInstallment == null)
            {
                ErrorCode = "Installment002";
                return null;
            }
            return mapper.Map<InstallmentsResponseDTO>(CurStudentInstallment);
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        public void UpdateStudentInstallment(int InstallmentsId, InstallmentsUpdateRequestDTO newStuInstallment, out string ErrorCode)
        {
            var stuId = dbContext.Students.Where(x => x.StudentId == newStuInstallment.StudentId).Any(x => x.ClassId == newStuInstallment.ClassId);
            var stuClassId = dbContext.Students.Where(x => x.ClassId == newStuInstallment.ClassId).Any(x => x.StudentId == newStuInstallment.StudentId);
            ErrorCode = "";
            if (!stuId && !stuClassId)
            {
                ErrorCode = "Installment004";
                return;
            }
            var CurStu = dbContext.Installments.Where(x => x.InstallmentsId== InstallmentsId).SingleOrDefault();
            if (CurStu == null)
            {
                ErrorCode = "Installment002";
                return;
            }
            mapper.Map(newStuInstallment, CurStu);
            SaveChanges();
        }

        public Installments UpdateStudentInstallmentPartially(int StudentInstallmentId, JsonPatchDocument StuPatch, out string ErrorCode)
        {
            ErrorCode = "";
            var CurStuInstallment = dbContext.Installments.Where(x => x.InstallmentsId == StudentInstallmentId).SingleOrDefault();
            if (CurStuInstallment == null)
            {
                ErrorCode = "Installment002";
                return null;
            }

            if (StuPatch==null)
            {
                ErrorCode = "Installment003";
                return null;
            }
            StuPatch.ApplyTo(CurStuInstallment);
            return CurStuInstallment;
        }
        public void DeleteStuInstallment(int InstallmentId, out string ErrorCode)
        {
            ErrorCode = "";
            var CurInstallment = dbContext.Installments.Where(x => x.InstallmentsId == InstallmentId).SingleOrDefault();
            if (CurInstallment == null)
            {
                ErrorCode = "Installment002";
                return;
            }
            dbContext.Remove(CurInstallment);
            SaveChanges();
        }
    }
}
