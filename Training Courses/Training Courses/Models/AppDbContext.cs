using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training_Courses.Models.Entities;

namespace Training_Courses.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Students> Students { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<Installments> Installments { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<Images> Images { get; set; }
       
      

    }
}
