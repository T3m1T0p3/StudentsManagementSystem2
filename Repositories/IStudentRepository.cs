using StudentManagementSystem2.DTO;
using StudentManagementSystem2.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem2.Repositories
{
    public interface IStudentRepository
    {
        void CreateStudent(Student student);
        Task<Student> GetStudentAsync(Guid guid);
        Task<Student> GetStudentAsync(string matricNo);
        bool StudentExists(Guid guid);
        bool StudentExists(string matricNo);
        //Student GetStudentUsingUsername(string username);
    }
}
