using StudentManagementSystem2.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem2.Entity
{
    public class Semester
    {
        public int SemesterId { get; set; }
        public int SessionId { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
        public int CGPA { get; set; }
        public int GPA { get; set; }
        public int Level { set; get; }

        [SemesterAttribute]
        public string semester { get; set; }
      
    }
}
