using AutoMapper;
using StudentManagementSystem2.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem2.Entity
{
    public class Profiles:Profile
    {
        public Profiles()
        {
            CreateMap<CreateStudent,Student>();
            CreateMap<Student, ReturnStudent>();
        }
    }
}
