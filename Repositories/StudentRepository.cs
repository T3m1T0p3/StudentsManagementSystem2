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
            var oldStudent = from s in _context.Students where s.MatricNo==student.MatricNo select s;
            if (oldStudent.Count() != 0)
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

        public async Task<Student> GetStudent(Guid guid)
        {
            Student student = await _context.Students.FindAsync(guid);
            if (student == null)
            {
                throw new Exception("Account Not found");
            }
            return student;
        }

        public async Task<Student> GetStudent(string matricNo)
        {
            Student student =await  _context.Students.FindAsync(matricNo);
            if (student == null)
            {
                throw new Exception("Account Not found");
            }
            return student;
        }
    }
}
