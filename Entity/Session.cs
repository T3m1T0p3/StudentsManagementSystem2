using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem2.Entity
{
    public class Session
    {
        public int SessionId { get; set; }
        public Guid StudentId { get; set; }
        public List<Semester> Semesters = new List<Semester>();
    }
}
