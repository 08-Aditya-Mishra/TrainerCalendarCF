using Calendar.Models;
using Calendar.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ApplicationDbContext _Context;

        public SkillController(ApplicationDbContext context)
        {
            _Context = context;
        }

        // Add a Skill
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SkillDto value)
        {
            Skill skill = new Skill();
            skill.SkillId = value.SkillId;
            skill.SkillName = value.SkillName;
            var skil = await _Context.skills.AnyAsync(u => u.SkillName == skill.SkillName);
            if(!skil)
            {
                _Context.skills.Add(skill);
                _Context.SaveChanges();
                return Ok();
            }
            else
            {
                return StatusCode(409, $"Skill '{skill.SkillName}' already exists.");
            }

        }

        //Get all skills
        [HttpGet]
        public List<SkillDto> GetSkill()
        {
            List<SkillDto> skillList = new List<SkillDto>();
            foreach (Skill skil in _Context.skills.ToList())
            {
                SkillDto objSkil = new SkillDto();
                objSkil.SkillId = skil.SkillId;
                objSkil.SkillName = skil.SkillName;
                skillList.Add(objSkil);
            }
            return skillList;
        }

        //Add Skills to Course
        [HttpPost]
        [Route("AddSkillToCourse")]
        public async Task<ActionResult> AddSkillCourse([FromBody] AddSkillCourseDto value)
        {
            var course = value.CourseId;
            var skills = value.Skills;
            var courseRes = await _Context.course.FirstOrDefaultAsync(c => c.CourseId == course);
            if (courseRes == null)
            {
                return NoContent();
            }
            
            foreach (var s in skills)
            {
                var skillExist = await _Context.skills.FirstOrDefaultAsync(sk => sk.SkillId == s);
                if (skillExist != null)
                {
                    courseRes.skills.Add(skillExist);
                }
              
            }
            _Context.SaveChanges();
            return Ok(courseRes);
        }

 
        //Add Skills to Trainer
        [HttpPost]
        [Route("AddSkillToTrainer")]
        public async Task<ActionResult> AddSkillTrainer([FromBody] AddSkillTrainerDto value)
        {
            var trainer = value.Id;
            var skills = value.Skills;
            var trainerRes = await _Context.trainers.FirstOrDefaultAsync(c => c.Id == trainer);
            if (trainerRes == null)
            {
                return NoContent();
            }

            foreach (var s in skills)
            {
                var skillExist = await _Context.skills.FirstOrDefaultAsync(sk => sk.SkillId == s);
                if (skillExist != null)
                {
                    trainerRes.Skills.Add(skillExist);
                }

            }
            _Context.SaveChanges();
            return Ok(trainerRes);
        }

        //Delete Skill
        [HttpDelete("DeleteSkillById")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            var skill = await _Context.skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            _Context.skills.Remove(skill);
            await _Context.SaveChangesAsync();

            return Ok();
        }

        //Delete Skill From Course
        [HttpDelete("DeleteSkillFromCourse")]
        public async Task<ActionResult> DeleteSkillCourse(int id, int id1)
        {
            var courseExist = await _Context.course.FirstOrDefaultAsync(c => c.CourseId == id);
            if (courseExist == null)
            {
                return NoContent();
            }

            foreach (var t in courseExist.skills)
            {
                var skillExist = await _Context.skills.FirstOrDefaultAsync(ti => ti.SkillId == id1);
                if (skillExist != null)
                {
                    courseExist.skills.Remove(skillExist);
                }

            }
            _Context.SaveChanges();
            return Ok(courseExist);
        }

        //Get all Skills of a Trainer
        [HttpGet("GetSkillsOfATrainer")]
        public async Task<ActionResult<IEnumerable<Trainer>>> GetSkillTrainer(string id)
        {
            return await _Context.trainers.Where(p => p.Id == id).Include(x => x.Skills).ToListAsync();
        }

        //Delete Skill From Trainer
        [HttpDelete("DeleteSkillFromTrainer")]
        public async Task<ActionResult> DeleteSkillTrainer(string id, int id1)
        {
            var trainerExist = await _Context.trainers.FirstOrDefaultAsync(c => c.Id == id);
            if (trainerExist == null)
            {
                return NoContent();
            }

            foreach (var s in trainerExist.Skills)
            {
                var skillExist = await _Context.skills.FirstOrDefaultAsync(ti => ti.SkillId == id1);
                if (skillExist != null)
                {
                    trainerExist.Skills.Remove(skillExist);
                }

            }
            _Context.SaveChanges();
            return Ok(trainerExist);
        }
    }
}
