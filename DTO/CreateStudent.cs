using Microsoft.AspNetCore.Http;
using StudentManagementSystem2.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem2.DTO
{
    public class CreateStudent
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string HomeAddress { get; set; }
        [Required]
        public DateTime YearofAdmission { get; set; }
        [Required]
        public string ModeOfEntry { get; set; }
        [Required]
        public string MatricNo { get; set; }
        public IFormFile Passport { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
