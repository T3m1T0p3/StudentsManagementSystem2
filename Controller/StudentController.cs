using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem2.DTO;
using StudentManagementSystem2.Entity;
//using StudentManagementSystem2.Model;
using StudentManagementSystem2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem2.Controller
{
    [ApiController]
    [Route("api/student")]
    public class StudentController:ControllerBase
    {
        IStudentRepository _student;
        IMapper _mapper;
        public StudentController(IMapper mapper, IStudentRepository context)
        {
            _student = context;
            _mapper = mapper;
        }
        [HttpGet("guid:guid",Name ="GetStudent")]
        public async Task<ActionResult<Student>> Get(Guid guid)
        {
            Console.WriteLine("Get Request Triggered");
            Student student = await _student.GetStudent(guid);
            return Ok(student);

        }
        [HttpGet("matricNo")]
        public async Task<ActionResult<Student>> Get(string matricNo)
        {
            Console.WriteLine("Get Request Triggered");
            Student student = await _student.GetStudent(matricNo);
            return Ok(student);

        }
        [HttpPost("create")]
        public IActionResult CreateStudent([FromBody]CreateStudent student)
        {
            Console.WriteLine("Post Request Triggered");
            Student newStudent;
            try
            {
                Console.WriteLine($"{student.FirstName}");
                newStudent = _mapper.Map<Student>(student);
                _student.CreateStudent(newStudent);

            }
            catch(Exception e)
            {
                return BadRequest($"{e.Message}");
            }
            return CreatedAtRoute("GetStudent",new { guid = newStudent.StudentId },newStudent);

        }
    }
}
/*curl -X POST -H "Content-Type:application/json" http://localhost:5000/api/student/create -d
 * '{"Surname":"Thorpe","MatricNo":"FUA/15/0001","FirstName":"Seye","MiddleName":"Afeezylai","HomeAddress":"18+ Akure","ModeOfEntry":"UTME","PhoneNumber":"07055677755"}'*/