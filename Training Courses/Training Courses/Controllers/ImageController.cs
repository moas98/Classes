using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Training_Courses.Models;
using Training_Courses.Models.Entities;
using Training_Courses.Models.Helper;
using Training_Courses.Models.RequestDTO;
using Training_Courses.Models.ResponseDTO;
using static System.Net.Mime.MediaTypeNames;

namespace Training_Courses.Controllers
{
    [Route("api/Class/Students/Imge")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment environment;

        public ImageController(AppDbContext dbContext, IMapper mapper, IWebHostEnvironment environment)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.environment = environment;
        }
        [HttpPost]
        [Route("Upload")]
        public string AddStudent([FromForm] ImageRequestDTO NewImage)
        {
            if (dbContext.Images.Where(x => x.StudentId == NewImage.StudentId).Any())
            {
                return "The picture already exists for the student";
            }
            
            if (NewImage.Image.Length >0)
            {
                if (!Directory.Exists(environment.WebRootPath + "\\uploads\\"))
                    {
                        Directory.CreateDirectory(environment.WebRootPath + "\\uploads\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(environment.WebRootPath + "\\uploads\\" + NewImage.Image.FileName))
                    {
                        NewImage.Image.CopyTo(fileStream);
                        
                        //mapper.Map<Image>(NewImage);
                        fileStream.Flush();
                    var Img = new Images() { ImagePath = "\\uploads\\" + NewImage.Image.FileName, StudentId = NewImage.StudentId };
                    var stu = dbContext.Students.Where(x => x.StudentId == NewImage.StudentId).SingleOrDefault();
                    stu.ImagePath = Img.ImagePath.ToString();
                    
                  
                    Add(Img);
                    return "\\uploads\\" + NewImage.Image.FileName;
                }
            }
            else
            {
                return "Upload Failed";
            }
        }
        //[Route("Upload")]
        [HttpPut("{StudentId}")]
        
        public string UpdateImg(int StudentId, [FromForm] ImageUpdateRequestDTO newImg)
        {
            var student = dbContext.Students.Where(x => x.StudentId == StudentId).SingleOrDefault();
            var CurImage = dbContext.Images.Where(id => id.StudentId ==StudentId).SingleOrDefault();
            //var fileStreamToCurImage = System.IO.File.GetAttributes(environment.WebRootPath + "\\uploads\\" + CurImage.ImagePath);
            //bool ImageExists = System.IO.File.Exists(environment.WebRootPath + CurImage.ImagePath);
            if (dbContext.Images.Where(x => x.StudentId == StudentId).Any())
            {
                System.IO.File.Delete(environment.WebRootPath + CurImage.ImagePath);
                System.IO.File.Create(environment.WebRootPath + "\\uploads\\" + newImg.Image.FileName);
                var Img = new Images() { ImagePath = "\\uploads\\" + newImg.Image.FileName, StudentId = newImg.StudentId };
                student.ImagePath = Img.ImagePath.ToString();
                CurImage.ImagePath = Img.ImagePath.ToString();
                CurImage.StudentId = Img.StudentId;
                dbContext.SaveChanges();
                // Update(StudentId, Img);
                return $"image with path={CurImage.ImagePath} updated to image with path={Img.ImagePath}" +
                    $"" +$"and student Id={StudentId} with New Student Id = {Img.StudentId}"+
                    $" secsessfuly";
            }
            else
            {
                return $"Updated Failed or There is No Image with Id {StudentId}";
            }
            
        }
        [HttpDelete("{StudentId}")]
       public string DeleteImage(int StudentId)
        {
            var CurImage = dbContext.Images.Where(id => id.StudentId == StudentId).SingleOrDefault();
            var student = dbContext.Students.Where(x => x.StudentId == StudentId).SingleOrDefault();
            var Image = dbContext.Images.Where(id => id.StudentId == StudentId).Any();
            if (Image)
            {
                //dbContext.Images.Where(x => x.StudentId == StudentId).SingleOrDefault();
                dbContext.Images.Remove(CurImage);
                System.IO.File.Delete(environment.WebRootPath + CurImage.ImagePath);
                student.ImagePath ="".ToString();
                dbContext.SaveChanges();
                return  $"Image with Student Id{StudentId} has been deleted Seccsessfly";
            }
            else
            {
                return $"there is no image With Student Id = {StudentId} ";
            }
        }
      
        public void Add(Images entity)
        {
            dbContext.Images.Add(entity);
            dbContext.SaveChanges();
        }
         


    }
}
