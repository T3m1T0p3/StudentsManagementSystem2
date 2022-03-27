using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StudentManagementSystem2.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem2.StudentContexts
{
    public class StudentContext :DbContext
    {
        private readonly IConfiguration _config;

        public DbSet<Student> Students { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Semester> Semesters { get; set; }

        public StudentContext() { }

        public StudentContext(DbContextOptions option, IConfiguration config):base(option)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(_config.GetConnectionString("sms2"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Student>().HasMany<Session>();
            modelBuilder.Entity<Session>().HasMany<Semester>();

        }

    }
}
