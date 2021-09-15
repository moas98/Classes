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
    public class ClassRepository : IClassRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public ClassRepository(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public Classes AddClass(ClassAddRequestDTO NewClass, out string ErrorCode)
        {
            //check Class Name duplication
            ErrorCode = "";
            if (dbContext.Classes.Any(x => x.ClassName == NewClass.ClassName && x.IsDeleted == false))
            {
                ErrorCode = "Class002";
                return null;
            }
            if (NewClass==null)
            {
                ErrorCode = "Class003";
                return null;
            }

            //Mapping

            var CurClass = mapper.Map<Classes>(NewClass);
            CurClass = dbContext.Classes.Add(CurClass).Entity;
            SaveChanges();
            return CurClass;
        }
    

        public void DeleteClass(int ClassId, out string ErrorCode)
        {
            ErrorCode = "";
            var CurClass = dbContext.Classes.Where(x => x.ClassesId == ClassId).SingleOrDefault();
            if (CurClass == null)
            {
                ErrorCode = "Class001";
                return;
            }
            // dbContext.Remove(CurClass);
            CurClass.IsDeleted = true;
            SaveChanges();
        }
        
        public PagedResponse<ClassResponseDTO> GetAll(IUrlHelper Url, string FilterClassName,
                                                        string orderby, PagingDTO paging)
        {
            var ClassQuery = dbContext.Classes.Include(x => x.Students).Where(x=>x.IsDeleted==false).AsQueryable();
            if (String.IsNullOrWhiteSpace(FilterClassName)|| String.IsNullOrWhiteSpace(orderby))
            {
                ClassQuery = dbContext.Classes.Include(x => x.Students).Where(x => x.IsDeleted == false).AsQueryable();
            }
            if (!String.IsNullOrWhiteSpace(FilterClassName))
            {
                ClassQuery = ClassQuery.Where(x => x.ClassName.Equals(FilterClassName));
            }

            if (!String.IsNullOrWhiteSpace(orderby))
            {
                ClassQuery = ClassQuery.OrderBy(orderby);
            }
            else
                ClassQuery = ClassQuery.OrderBy(x => x.ClassName);


            var ResponseDTO = ClassQuery.Select(p => mapper.Map<ClassResponseDTO>(p));

            var pagedResponse = new PagedResponse<ClassResponseDTO>(ResponseDTO, paging);

            if (pagedResponse.Paging.HasNextPage)
                pagedResponse.Paging.NextPageURL = Url.Link("GetAllClasses", new
                {
                    FilterClassName,
                    orderby,
                    paging.RowCount,
                    PageNumber = paging.PageNumber + 1
                });
            if (pagedResponse.Paging.HasPrevPage)
                pagedResponse.Paging.PrevPageURL = Url.Link("GetAllClasses", new
                {
                    FilterClassName,
                    orderby,
                    paging.RowCount,
                    PageNumber = paging.PageNumber - 1
                });

            return pagedResponse;
        }

        public ClassResponseDTO GetById(int Id, out string ErrorCode)
        {
            ErrorCode = "";
            Classes CurClass= dbContext.Classes.Include(x => x.Students).Where(x => x.ClassesId == Id && x.IsDeleted == false).FirstOrDefault();
            //CurClass.Students.RemoveAll(item => item == null);
            if (CurClass==null)
            {
                ErrorCode = "Class005";
                return null;
            }
            if (CurClass == null)
            {
                ErrorCode = "Class001";
                return null;
            }
            return mapper.Map<ClassResponseDTO>(CurClass);
        }

        public int SaveChanges()
        {
           return dbContext.SaveChanges();
        }

        public void UpdateClass(int ClassId, ClassUpdateRequestDTO newClass, out string ErrorCode)
        {
            ErrorCode = "";
            if (String.IsNullOrWhiteSpace(newClass.ClassName))
            {
                ErrorCode = "Class003";
                return;
            }

            var CurClass = dbContext.Classes.Where(x =>x.ClassesId  == ClassId && x.IsDeleted == false).SingleOrDefault();
            if (CurClass == null)
            {
                ErrorCode = "Class001";
                return;
            }
            if (newClass==null)
            {
                ErrorCode = "Class004";
                return;
            }
            mapper.Map(newClass, CurClass);
            SaveChanges();
        }

    public Classes UpdateClassPartially(int ClassId, JsonPatchDocument ClassPatch, out string ErrorCode)
    {
            ErrorCode = "";
            var CurClass = dbContext.Classes.Where(x => x.ClassesId == ClassId && x.IsDeleted == false).SingleOrDefault();
            if (CurClass == null)
            {
                ErrorCode = "Class001";
                return null;
            }
            ClassPatch.ApplyTo(CurClass);
            return CurClass;
        }
    }
 }


