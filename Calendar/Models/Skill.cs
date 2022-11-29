using System.ComponentModel.DataAnnotations;

namespace Calendar.Models
{
    public class Skill
    {
        public Skill()
        {
            this.Users = new HashSet<Trainer>();
            this.Courses = new HashSet<Courses>();
        }
        [Key]
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public virtual  ICollection<Trainer> Users { get; set; }
        public virtual  ICollection<Courses> Courses { get; set; }
    }
}
