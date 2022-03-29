using AutoMapper;
using StudentManagementSystem2.DTO;
using StudentManagementSystem2.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem2.Entity
{
    public class Profiles:Profile
    {
        public Profiles()
        {
            CreateMap<CreateStudent,Student>() ;
            CreateMap<Student, ReturnStudent>().ForMember(dest=>dest.Name,src=>src.MapFrom(x=>$"{x.FirstName} {x.MiddleName} {x.Surname}"));
        }
    }
}
