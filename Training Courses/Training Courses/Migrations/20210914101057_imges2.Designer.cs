// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Training_Courses.Models;

namespace Training_Courses.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210914101057_imges2")]
    partial class imges2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Training_Courses.Models.Entities.Absence", b =>
                {
                    b.Property<int>("AbsenceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClassId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("StuAbsence")
                        .HasColumnType("bit");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("AbsenceId");

                    b.HasIndex("ClassId");

                    b.HasIndex("StudentId");

                    b.ToTable("Absences");
                });

            modelBuilder.Entity("Training_Courses.Models.Entities.Classes", b =>
                {
                    b.Property<int>("ClassesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClassName")
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int>("Course_price")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("Number_of_Days")
                        .HasColumnType("int");

                    b.HasKey("ClassesId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("Training_Courses.Models.Entities.Images", b =>
                {
                    b.Property<int>("ImagesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("ImagesId");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Training_Courses.Models.Entities.Installments", b =>
                {
                    b.Property<int>("InstallmentsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClassCost")
                        .HasColumnType("int");

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("StudentPay")
                        .HasColumnType("int");

                    b.HasKey("InstallmentsId");

                    b.HasIndex("ClassId");

                    b.HasIndex("StudentId");

                    b.ToTable("Installments");
                });

            modelBuilder.Entity("Training_Courses.Models.Entities.Students", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("ImgeNumber")
                        .HasColumnType("int");

                    b.Property<int?>("InstallmentId")
                        .HasColumnType("int");

                    b.Property<bool>("InstallmentStatus")
                        .HasColumnType("bit");

                    b.Property<string>("Mark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentFullName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("StudentId");

                    b.HasIndex("ClassId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Training_Courses.Models.Entities.Absence", b =>
                {
                    b.HasOne("Training_Courses.Models.Entities.Classes", "Class")
                        .WithMany()
                        .HasForeignKey("ClassId");

                    b.HasOne("Training_Courses.Models.Entities.Students", "Student")
                        .WithMany("Absences")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Training_Courses.Models.Entities.Images", b =>
                {
                    b.HasOne("Training_Courses.Models.Entities.Students", "Student")
                        .WithOne("Images")
                        .HasForeignKey("Training_Courses.Models.Entities.Images", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Training_Courses.Models.Entities.Installments", b =>
                {
                    b.HasOne("Training_Courses.Models.Entities.Classes", "Class")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Training_Courses.Models.Entities.Students", "Student")
                        .WithMany("Installment")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Training_Courses.Models.Entities.Students", b =>
                {
                    b.HasOne("Training_Courses.Models.Entities.Classes", "Class")
                        .WithMany("Students")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("Training_Courses.Models.Entities.Classes", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Training_Courses.Models.Entities.Students", b =>
                {
                    b.Navigation("Absences");

                    b.Navigation("Images");

                    b.Navigation("Installment");
                });
#pragma warning restore 612, 618
        }
    }
}
