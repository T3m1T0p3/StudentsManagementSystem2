using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem2.Authentication;
using StudentManagementSystem2.DTO;
using StudentManagementSystem2.Entity;
using StudentManagementSystem2.Helper;
//using StudentManagementSystem2.Model;
using StudentManagementSystem2.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        IAuthenticate _authenticate;
        public StudentController(IMapper mapper, IStudentRepository context,IConvertFileToByteArray converter,IAuthenticate authenticate)
        {
            _student = context;
            _mapper = mapper;
            _fileConverter = converter;
            _authenticate = authenticate;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery]string matricNo, [FromQuery] string password)
        {
            Student student;
            try
            {
                
                student = await _student.GetStudentAsync(matricNo);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            if (!_authenticate.AuthenticateUser(student.Password, password))
            {
                return Ok("Incorrect Username or Password");
            }
            var http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_authenticate.GenerateToken(matricNo));
            return Ok(http);


            //return Ok(CreatedAtRoute("GetStudent", new { guid = student.StudentId }, student));

            //CreatedAtRoute("GetStudent", new { guid = student.StudentId }, student);

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
        [HttpGet("matricNo",Name ="GetStudentUsingMatricNo")]
        public async Task<ActionResult<Student>> Get([FromQuery]string matricNo)
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

        [HttpPost("register")]
        public IActionResult CreateStudent([FromForm]CreateStudent student)
        {
            Console.WriteLine("Post Request Triggered");
            Student newStudent;
            if (student == null) return Ok();
            try
            { 
                Console.WriteLine("processing matricNo");
                student.MatricNo=student.MatricNo.Replace('/','-');
                Console.WriteLine("Generating Password Hash");
                student.Password = _authenticate.GenerateHash(student.Password);
                Console.WriteLine("Processing Passport");
                var passport = _fileConverter.Convert(student.Passport);
                newStudent = _mapper.Map<Student>(student);
                newStudent.ByteArrayofPassport = passport;
                _student.CreateStudent(newStudent);
                return CreatedAtRoute("GetStudentUsingMatricNo", new { matricNo = newStudent.MatricNo }, newStudent);
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