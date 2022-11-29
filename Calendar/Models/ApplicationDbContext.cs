using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace Calendar.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Courses> course { get; set; }
        public DbSet<Skill> skills { get; set; }
        public DbSet<Session> sessions { get; set; }
        public DbSet<Trainer> trainers { get; set; }
        public DbSet<RefreshToken> refreshTokens { get; set; }
    }
}
