using Calendar.Models;
using Calendar.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Calendar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDbContext _Context;

        public CourseController(ApplicationDbContext context)
        {
            _Context = context;
        }

        // Add a Course
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CourseDto value)
        {
            Courses course = new Courses();
            course.CourseId = value.CourseId;
            course.CourseName = value.CourseName;
            var cour = await _Context.course.AnyAsync(u => u.CourseName == course.CourseName);
            if (!cour)
            {
                _Context.course.Add(course);
                _Context.SaveChanges();
                return Ok();
            }
            else
            {
                return StatusCode(409, $"Skill '{course.CourseName}' already exists.");
            }

        }

        //Get all courses
        [HttpGet]
        public List<CourseDto> GetCourse()
        {
            List<CourseDto> courseList = new List<CourseDto>();
            foreach (Courses cour in _Context.course.ToList())
            {
                CourseDto objSkil = new CourseDto();
                objSkil.CourseId = cour.CourseId;
                objSkil.CourseName = cour.CourseName;
                courseList.Add(objSkil);
            }
            return courseList;
        }

        //Delete Course
        [HttpDelete("DeleteCourseById")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var courses = await _Context.course.FindAsync(id);
            if (courses == null)
            {
                return NotFound();
            }

            _Context.course.Remove(courses);
            await _Context.SaveChangesAsync();

            return Ok();
        }
        

        //Get all skills of a course
        [HttpGet("GetSkillsOfCourse")]
        public async Task<ActionResult<IEnumerable<Courses>>> GetSkillCourse(int id)
        {
            return await _Context.course.Where(p => p.CourseId == id).Include(x => x.skills).ToListAsync();
        }

        //Get all trainers in a course
        [HttpGet("GetTrainersInACourse")]
        public async Task<ActionResult<IEnumerable<Courses>>> GetTrainerCourse(int id)
        {
            return await _Context.course.Where(p => p.CourseId == id).Include(x => x.users).ToListAsync();
        }
    }
}
