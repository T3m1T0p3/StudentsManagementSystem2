using AutoMapper;
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
    public class CourseRepository: ICourseRepository
    {
        StudentContext _context;
        IStudentRepository _studentRepository;
        IMapper _mapper;
        public CourseRepository(StudentContext context,IStudentRepository studentRepository,IMapper mapper)
        {
            _context = context;
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public bool SemesterExists(Guid sessionId,int semesterNumber)
        {
            Semester semester;
            try
            {
                semester = _context.Semesters.SingleOrDefault(s => s.SessionId == sessionId&&s.SemesterNumber==semesterNumber);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            if (semester is null) return false;
            return true;
        }

        public bool SessionExists(Guid studentId,int sessionNumber)
        {
            Session session;
            try
            {
                session = _context.Sessions.SingleOrDefault(s => s.StudentId == studentId&&s.SessionNumber==sessionNumber);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            if (session is null) return false;
            return true;
        }

        public Session GetSession(Guid studentId, int session)
        {
            Student student;
            Session returnSession=null;
            try
            {
                student = _context.Students.SingleOrDefault(s=>s.StudentId==studentId);
                if (student!=null)
                {
                    returnSession = _context.Sessions.SingleOrDefault(s => s.StudentId == student.StudentId&&s.SessionNumber==session);
                    if (returnSession == null)//Create new Session?
                    {
                        throw new Exception("The required Session does not exist.");
                    }
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            return returnSession;
        }
        public Semester GetSemester(Guid sessionId,int sessionNumber,Guid studentId,int semesterNumber)
        {
            Semester semester;
            Session session;
            if (SessionExists(sessionId, sessionNumber))
            {
                try
                {
                    session = GetSession(studentId, sessionNumber);
                    semester = session.Semesters.SingleOrDefault(s => s.SemesterNumber == semesterNumber);
                    return semester;
                }
                catch(Exception e)
                {
                    throw new Exception(e.Message);
                }

            }
            else
            {
                throw new Exception("Requested Semester does not Exist");
            }
        }

        public async void CreateCourse(CreateCourse createCourse)
        {
            Student student;
            IEnumerable<Course> courses;
            Semester semester;
            Session session;
            try
            {
                student = await _studentRepository.GetStudentAsync(createCourse.MatricNo);
                session = GetSession(student.StudentId,createCourse.SessionNumber);
                semester = GetSemester(session.SessionId, session.SessionNumber, student.StudentId, createCourse.SemesterNumber);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            courses = GetCourse(createCourse.CourseCode, student.StudentId);
            foreach (Course course in courses)
            {
                if (course.Status == "Passed" || course.Status == "Registered")
                {
                    throw new Exception("Cannot Add course. Course is either in progress or has been cleared by specified student");
                }
            }
            Course cours = _mapper.Map<Course>(createCourse);
            cours.Status = "Registered";
            semester.Courses.Add(cours);
            _context.SaveChanges();
            return;
            //student.C
        }

        public IEnumerable<Course> GetCourse(string courseCode, Guid studentId)
        {
            IEnumerable<Course> courses;
            courses = from course in _context.Courses where course.CourseCode == courseCode && course.StudentId == studentId select course;
            return courses;

        }
    }
}
