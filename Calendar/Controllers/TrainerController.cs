using Calendar.Models;
using Calendar.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private readonly ApplicationDbContext _Context;

        public TrainerController(ApplicationDbContext context)
        {
            _Context = context;
        }

        //Add Trainer to Course
        [HttpPost]
        [Route("AddTrainerToCourse")]
        public async Task<ActionResult> AddTrainerCourse([FromBody] AddTrainerCourseDto value)
        {
            var course = value.CourseId;
            var trainer = value.Trainers;
            var courseRes = await _Context.course.FirstOrDefaultAsync(c => c.CourseId == course);
            if (courseRes == null)
            {
                return NoContent();
            }

            foreach (var t in trainer)
            {
                var trainerExist = await _Context.trainers.FirstOrDefaultAsync(ti => ti.Id == t);
                if (trainerExist != null)
                {
                    courseRes.users.Add(trainerExist);
                }

            }
            _Context.SaveChanges();
            return Ok(courseRes);
        }

        //Delete Trainer From Course
        [HttpDelete("DeleteTrainerFromCourse")]
        public async Task<ActionResult> DeleteTrainerCourse(int id, string id1)
        {
            var courseExist = await _Context.course.FirstOrDefaultAsync(c => c.CourseId == id);
            if (courseExist == null)
            {
                return NoContent();
            }

            foreach (var t in courseExist.users)
            {
                var trainerExist = await _Context.trainers.FirstOrDefaultAsync(ti => ti.Id == id1);
                if (trainerExist != null)
                {
                    courseExist.users.Remove(trainerExist);
                }

            }
            _Context.SaveChanges();
            return Ok(courseExist);
        }

        //Delete User
        [HttpDelete("DeleteTrainerById")]
        public async Task<IActionResult> DeleteTrainer(string id)
        {
            var trainer = await _Context.Users.FindAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }

            _Context.Users.Remove(trainer);
            await _Context.SaveChangesAsync();

            return Ok();
        }
    }
}
