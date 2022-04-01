using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem2.Entity
{
    public class Student
    {
        //BioData
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid StudentId { get; set; }
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
        //[FileExtensions(Extensions = ("jpg,jpeg"))]
        public byte[] ByteArrayofPassport { get; set; }// = new byte[2097152];

        public string Email { get; set; }
        public string Password { get; set; }
        //public string Username { get; set; }

        public List<Session> Sessions { get; set; } = new List<Session>();

    }
} 
