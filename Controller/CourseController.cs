using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem2.DTO;
using StudentManagementSystem2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem2.Controller
{
    [ApiController]
    [Route("courses")]
    public class CourseController:ControllerBase
    {
        ICourseRepository _courseRepository;
        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        [HttpPost]
        public IActionResult Createcourse([FromForm]CreateCourse createCourse)
        {
            Console.WriteLine("CreateCourse action");
            try
            {
                _courseRepository.CreateCourse(createCourse);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}
