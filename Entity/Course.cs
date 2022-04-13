using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem2.Entity
{
    public class Course
    {
        public int CourseId { get; set; }

        //[ForeignKey("SemesterId")]
        public int SemesterId { get; set; }
        public Guid StudentId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCode { get; set; }
        public char Grade { get; set; }
        public int Score { get; set; }
        public string Status { get; set; }

    }
}
