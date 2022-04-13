﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentManagementSystem2.StudentContexts;

namespace StudentManagementSystem2.Migrations
{
    [DbContext(typeof(StudentContext))]
    partial class StudentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StudentManagementSystem2.Entity.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CourseCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grade")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("SemesterId")
                        .HasColumnType("int");

                    b.Property<Guid?>("SemesterId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CourseId");

                    b.HasIndex("SemesterId1");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("StudentManagementSystem2.Entity.Semester", b =>
                {
                    b.Property<Guid>("SemesterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CGPA")
                        .HasColumnType("int");

                    b.Property<int>("GPA")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("SemesterNumber")
                        .HasColumnType("int");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SemesterId");

                    b.HasIndex("SessionId");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("StudentManagementSystem2.Entity.Session", b =>
                {
                    b.Property<Guid>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SessionNumber")
                        .HasColumnType("int");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SessionId");

                    b.HasIndex("StudentId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("StudentManagementSystem2.Entity.Student", b =>
                {
                    b.Property<Guid>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("ByteArrayofPassport")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomeAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MatricNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModeOfEntry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("YearofAdmission")
                        .HasColumnType("datetime2");

                    b.HasKey("StudentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("StudentManagementSystem2.Entity.Course", b =>
                {
                    b.HasOne("StudentManagementSystem2.Entity.Semester", null)
                        .WithMany("Courses")
                        .HasForeignKey("SemesterId1");
                });

            modelBuilder.Entity("StudentManagementSystem2.Entity.Semester", b =>
                {
                    b.HasOne("StudentManagementSystem2.Entity.Session", null)
                        .WithMany()
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StudentManagementSystem2.Entity.Session", b =>
                {
                    b.HasOne("StudentManagementSystem2.Entity.Student", null)
                        .WithMany("Sessions")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StudentManagementSystem2.Entity.Semester", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("StudentManagementSystem2.Entity.Student", b =>
                {
                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
