using System.ComponentModel.DataAnnotations;

namespace Calendar.Models
{
    public class Courses
    {
        public Courses()
        {
            this.skills = new HashSet<Skill>();
            this.users = new HashSet<Trainer>();
        }
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public virtual ICollection<Skill> skills { get; set; }
        public virtual ICollection<Trainer> users { get; set; }
    }
}
