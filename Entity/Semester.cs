using StudentManagementSystem2.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem2.Entity
{
    public class Semester
    {
        public Guid SemesterId { get; set; }
        public Guid SessionId { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
        public int CGPA { get; set; }
        public int GPA { get; set; }
        [Range(100,500)]
        public int Level { set; get; }

        [Semester]
        public int SemesterNumber { get; set; }
      
    }
}
