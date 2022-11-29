using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calendar.Models
{
    public class Trainer
    {
        public Trainer()
        {
            this.Skills = new HashSet<Skill>();
            this.Courses = new HashSet<Courses>();
        }
        [Key]
        public string Id { get; set; }
        [ForeignKey("Id")]
        public ApplicationUser User { get; set; }
        
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<Courses> Courses { get; set; }


    }
}
