using Microsoft.AspNetCore.Identity;

namespace Calendar.Models
{
    public class ApplicationUser : IdentityUser
    {
        public  string Name { get; set; }
        public string Role { get; set; }
    }
}
