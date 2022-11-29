using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calendar.Models
{
    public class Session
    {
        [Key]
        public int SessionId { get; set; }
        public string SessionTitle { get; set; }
        public string SessionDescription { get; set; }
        
        [ForeignKey("Standard")]
        public int fk_UserId { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("Standard")]
        public int fk_SkillId { get; set; }
        public Skill skill { get; set; }

        [ForeignKey("Standard")]
        public int fk_CourseId { get; set; }
        public Courses course { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }

    }
}
