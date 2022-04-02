using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem2.Authentication;
using StudentManagementSystem2.CustomAttributes;
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
        [HttpGet("login")]
        [Password]
        public async Task<IActionResult> Login([FromQuery] LoginCredential credential)
        {
            Console.WriteLine("Login Request Triggered");
            Student student;
            try
            {
                
                student = await _student.GetStudentAsync(credential.MatricNo);
                if (!_authenticate.AuthenticateUser(student.Password, credential.Password))
                {
                    return Unauthorized("Incorrect Username or Password");
                }
                var token=_authenticate.GenerateToken(credential.MatricNo);
                return Ok(token);
            }
            catch (Exception e)
            {
                return Unauthorized("Incorrect Login Credentials");
            }
        }

        [HttpGet("{guid:guid}",Name ="GetStudent")]
        public async Task<ActionResult<ReturnStudent>> Get([FromRoute]Guid guid)
        {
            Console.WriteLine("Get Request Triggered");
            Student student;
            try
            {
                student = await _student.GetStudentAsync(guid);
                IFormFile passport=null;
                
                if (student.ByteArrayofPassport.Length>1000)
                {
                    passport = new FormFile(new MemoryStream(student.ByteArrayofPassport), 0, student.ByteArrayofPassport.Length, "ByteArrayOfPassport","Passport");
                    
                }
      
                ReturnStudent returnStudent = _mapper.Map<ReturnStudent>(student);
                Console.WriteLine("forming passport");
                returnStudent.Passport = passport;
                Console.WriteLine("Returning");
                return Ok(returnStudent);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

            

        }
        [HttpGet(Name ="GetStudentUsingMatricNo")]
        public async Task<ActionResult<ReturnStudent>> Get([FromQuery]string matricNo)
        {
            Console.WriteLine("Get Request Triggered");
            Student student;
            IFormFile passport=null;
            try
            {
                student = await _student.GetStudentAsync(matricNo);
                if (student.ByteArrayofPassport.Length>1000)
                {
                    Console.WriteLine("Initializing formfile");
                    passport = new FormFile(new MemoryStream(student.ByteArrayofPassport), 0, student.ByteArrayofPassport.Length, "ByteArrayOfPassport", "passport");
                    Console.WriteLine("FormFile created successfully");
                }
                ReturnStudent returnStudent = _mapper.Map<ReturnStudent>(student);
                Console.WriteLine("Assigning passport");
                returnStudent.Passport = passport;
                Console.WriteLine("Returning");
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