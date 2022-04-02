using StudentManagementSystem2.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem2.Entity
{
    public class LoginCredential
    {
        public string MatricNo { get; set; }
        [Password]
        public string Password { get; set; }
    }
}
