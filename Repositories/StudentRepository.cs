using StudentManagementSystem2.DTO;
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

        public bool StudentExists(Guid studentId)
        {
            Student student;
            try
            {
                student = _context.Students.SingleOrDefault(s =>  s.StudentId == studentId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            if (student is null) return false;
            return true;
        }
        public bool StudentExists(string matricNo)
        {
            Student student;
            try
            {
                student = _context.Students.SingleOrDefault(s => s.MatricNo == matricNo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            if (student is null) return false;
            return true;
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
            var student =  _context.Students.SingleOrDefault(s => s.MatricNo == matricNo);
            if (student==null)
            {
                throw new Exception("Invalid User");
            }
            return student;
        }
    }
}
