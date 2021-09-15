using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training_Courses.Models.Entities;
using Training_Courses.Models.RelationDTO;
using Training_Courses.Models.RequestDTO;
using Training_Courses.Models.ResponseDTO;

namespace Training_Courses.Models
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            //Students Mapper
            CreateMap<StudentTheFinalGradeDTO, Students>();
            CreateMap<Students, StudentTheFinalGradeResponseDTO>()
                .ForMember(des=>des.StudentName,opt=>opt.MapFrom(src=>src.StudentFullName))
                .ForMember(des => des.Mark, opt => opt.MapFrom(src => src.Mark))
                ;
            //CreateMap<Images, Students>().ForMember(x=>x.ImagePath,opt=>opt.MapFrom(src=>src.ImagePath))
                
            CreateMap<Students, StudentForm>();
            CreateMap<Students, StudentsResponseDTO>().ForMember(des => des.InstallmentsCount, opt => opt.MapFrom(src => src.Installment.Count))
              .ForMember(des => des.AbsensesCount, opt => opt.MapFrom(src => src.Absences.Count))
              .ForMember(des => des.InstallmentStatus, opt => opt.MapFrom(act => act.Class.Course_price - act.Installment.Sum(x => x.StudentPay) <= 0))
              .ForMember(des => des.ClassName, opt => opt.MapFrom(src => src.Class.ClassName))
              .ForMember(des=>des.ImagePath,opt=>opt.MapFrom(src=>src.ImagePath))
              ;
            CreateMap<Students, StudentsResponseByIdDTO>()
               
                .ForMember(des => des.Installment, opt => opt.MapFrom(src => src.Installment
                 .Select(x => new Installments() 
                 { InstallmentsId = x.InstallmentsId,
                     ClassCost = x.Class.Course_price, StudentPay = x.StudentPay
                 , PaymentDate = x.PaymentDate, StudentId = x.StudentId, ClassId = x.ClassId })))
                .ForMember(des=>des.ImagesPath,opt=>opt.MapFrom(src=>src.Images.ImagePath) )
                .ForMember(des=>des.Absenses,opt=>opt.MapFrom(sec=>sec.Absences.Select(s=>new Absence() { 
                AbsenceId=s.AbsenceId,DateTime=s.DateTime,StuAbsence=s.StuAbsence})))
               //.ForMember(des=>des.Images,opt=>opt.MapFrom(src=>src.Images.Select(x=>new Images() { 
               //ImagePath=x.ImagePath,ImagesId=x.ImagesId
               //})))
                .ForMember(des => des.StudentPayment, opt => opt.MapFrom(src => src.Installment.Sum(x => x.StudentPay)))
                .ForMember(des => des.Remaining_amount, opt => opt.MapFrom(action => action.Class.Course_price - action.Installment.Sum(x => x.StudentPay)));
            CreateMap<StudentUpdateRequestDTO, Students>();
            CreateMap<StudentAddRequestDTO, Students>();
            CreateMap<Students, StudentToClass>();

            //----------------------------------------------------------------------------------------------------------------------------------------------
            //CreateMap<Stu, Images>();
          // CreateMap<>
            //----------------------------------------------------------------------------------------------------------------------------------------------

            //Classes Mapper
            CreateMap<Classes, ClassResponseDTO>()
                .ForMember(des => des.Students, opt =>
                                         opt.MapFrom(src =>
                                         src.Students.Select(x => new Students()
                                         {
                                             StudentId = x.StudentId,
                                             StudentFullName = x.StudentFullName,
                                             Email = x.Email,
                                             Gender = x.Gender,
                                             PhoneNumber = x.PhoneNumber
                                             
                                         }).ToList()))
              
                ;
            CreateMap<ClassAddRequestDTO, Classes>();
            CreateMap<ClassUpdateRequestDTO, Classes>();
            //----------------------------------------------------------------------------------------------------------------------------------------------
            // Absence Mapper
            CreateMap<Absence, AbsensesToStudent>();
            CreateMap<Absence, AbsenceResponseDTO>()
                .ForMember(des => des.StudentName, opt => opt.MapFrom(src => src.Student.StudentFullName))
                .ForMember(des => des.ClassID, opt => opt.MapFrom(src => src.Student.ClassId))
                .ForMember(des => des.DateTime, opt => opt.MapFrom(x => x.DateTime))
                ;
            CreateMap<AbsenceAddRequestDTO, Absence>();
            CreateMap<AbsenceUpdateRequestDTO, Absence>();

            //----------------------------------------------------------------------------------------------------------------------------------------------
            //Installments Mapper
            CreateMap<Installments, InstallmentToStudent>();
            CreateMap<Installments, InstallmentsResponseDTO>()
            .ForMember(des => des.ClassCost, src => src.MapFrom(act => act.Class.Course_price))
            .ForMember(des => des.StudentFullName, src => src.MapFrom(act => act.Student.StudentFullName))
            .ForMember(des=>des.StudentFullName,opt=>opt.MapFrom(src=>src.Student.StudentFullName))
            .ForMember(des => des.ClassName, opt => opt.MapFrom(src => src.Class.ClassName))
            ;
            CreateMap<InstallmentsAddRequestDTO, Installments>();
            CreateMap<InstallmentsUpdateRequestDTO, Installments>();
            //----------------------------------------------------------------------------------------------------------------------------------------------
            
        }
    }
}
