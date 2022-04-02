using StudentManagementSystem2.Entity;
using StudentManagementSystem2.StudentContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem2.Repositories
{
    public class StudentRepository: IStudentRepository
    {
        StudentContext _context;
        public StudentRepository(StudentContext context)
        {
            _context = context;
        }
        public void CreateStudent(Student student)
        {
            var oldStudent = _context.Students.SingleOrDefault(s=>s.MatricNo==student.MatricNo);
            if (oldStudent != null)
            {
                throw new Exception("Account alredy exists, log in to your account");
            }

            try
            {
                _context.Students.Add(student);
                _context.SaveChanges();
            }

            catch(Exception e)
            {
                throw new Exception($"{e.Message}");
            }
        }

        public async Task<Student> GetStudentAsync(Guid guid)
        {
            Student student = await _context.Students.FindAsync(guid);
            if (student == null)
            {
                throw new Exception("Account Not found");
            }
            Console.WriteLine(student.FirstName);
            return student;
        }

        public async Task<Student> GetStudentAsync(string matricNo)
        {
            var student = _context.Students.SingleOrDefault(s => s.MatricNo == matricNo);
            if (student==null)
            {
                throw new Exception("Invalid User");
            }
            return student;
        }

        /*public Student GetStudentUsingUsername(string username)
        {
            Student student = _context.Students.SingleOrDefault(s => s.Username == username);
            if (student == null)
            {
                throw new Exception("Inalid Username or Password");

            }
            return student;
        }*/
    }
}
