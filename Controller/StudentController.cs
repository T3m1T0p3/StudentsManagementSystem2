using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem2.DTO;
using StudentManagementSystem2.Entity;
using StudentManagementSystem2.Helper;
//using StudentManagementSystem2.Model;
using StudentManagementSystem2.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
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
        IConvertFileToByteArray _fileConverter;
        public StudentController(IMapper mapper, IStudentRepository context,IConvertFileToByteArray converter)
        {
            _student = context;
            _mapper = mapper;
            _fileConverter = converter;
        }
        [HttpGet]
        public IActionResult Gett()
        {
            return Ok("Hello");
        }

        [HttpGet("{guid:guid}",Name ="GetStudent")]
        public async Task<ActionResult<Student>> Get([FromRoute]Guid guid)
        {
            Console.WriteLine("Get Request Triggered");
            Student student;
            try
            {
                student = await _student.GetStudentAsync(guid);
                IFormFile passport=null;
                Console.WriteLine("condition1");
                
                if (student.ByteArrayofPassport!=null)
                {
                    passport = new FormFile(new MemoryStream(student.ByteArrayofPassport), 0, student.ByteArrayofPassport.Length, "ByteArrayOfPassport","Passport");
                    
                }
                
                ReturnStudent returnStudent = _mapper.Map<ReturnStudent>(student);
                returnStudent.Passport = passport;
                return Ok(returnStudent);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

            

        }
        [HttpGet("{matricNo}")]
        public async Task<ActionResult<Student>> Get([FromRoute]string matricNo)
        {
            Console.WriteLine("Get Request Triggered");
            Student student;
            IFormFile passport=null;
            try
            {
                student = await _student.GetStudentAsync(matricNo);
                if (student.ByteArrayofPassport!=null)
                {
                    passport = new FormFile(new MemoryStream(student.ByteArrayofPassport), 0, student.ByteArrayofPassport.Length, "ByteArrayOfPassport", "passport");
                }
                ReturnStudent returnStudent = _mapper.Map<ReturnStudent>(student);
                returnStudent.Passport = passport;
                return Ok(returnStudent);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("create")]
        public IActionResult CreateStudent([FromBody]CreateStudent student)
        {
            Console.WriteLine("Post Request Triggered");
            Student newStudent;
            try
            {
                
                var passport = _fileConverter.Convert(student.Passport);
                newStudent = _mapper.Map<Student>(student);
                newStudent.ByteArrayofPassport = passport;
                _student.CreateStudent(newStudent);
                return CreatedAtRoute("GetStudent", new { guid = newStudent.StudentId }, newStudent);
            }
            catch(Exception e)
            {
                return BadRequest($"{e.Message}");
            }
            
            
        }
        //[HttpPost("/upload/passport")]
        //public IActionResult UploadPassport( [FromBody] IFormFile file )
        //{
          //  return Ok();
        //}
    }
}
/*curl -X POST -H "Content-Type:application/json" http://localhost:5000/api/student/create -d
 * '{"Surname":"Thorpe","MatricNo":"FUA/15/0001","FirstName":"Seye","MiddleName":"Afeezylai","HomeAddress":"18+ Akure","ModeOfEntry":"UTME","PhoneNumber":"07055677755"}'*/