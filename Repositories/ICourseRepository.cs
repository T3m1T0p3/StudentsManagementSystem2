using StudentManagementSystem2.DTO;
using StudentManagementSystem2.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem2.Repositories
{
    public interface ICourseRepository
    {
        Session GetSession(Guid studentId, int sessionNumber);
        Semester GetSemester(Guid sessionId, int sessionNumber, Guid studentId, int semesterNumber);
        bool SessionExists(Guid studentId, int sessionNumber);
        bool SemesterExists(Guid sessionId, int semesterNumber);
        void CreateCourse(CreateCourse createCourse);
        IEnumerable<Course> GetCourse(string courseCode, Guid studentId);
    }
}
