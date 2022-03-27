using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem2.DTO
{
    public class ReturnStudent
    {
        public string Name { get; set; }
        public string MatricNo { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile Passport { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string HomeAddress { get; set; }

    }
}
