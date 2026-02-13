using EduPersona.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduPersona.Core.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Profession> Professions { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<UserDesignation> UserDesignations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Data Seeding
            base.OnModelCreating(modelBuilder);
        }
    }
}