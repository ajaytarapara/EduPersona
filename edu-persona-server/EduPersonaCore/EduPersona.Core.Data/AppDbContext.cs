using EduPersona.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduPersona.Core.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<UserDesignation> UserDesignations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                   .HasOne(u => u.CreatedByUser)
                   .WithMany(u => u.CreatedUsers)
                   .HasForeignKey(u => u.CreatedBy)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.UpdatedByUser)
                .WithMany(u => u.UpdatedUsers)
                .HasForeignKey(u => u.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.DeletedByUser)
                .WithMany(u => u.DeletedUsers)
                .HasForeignKey(u => u.DeletedBy)
                .OnDelete(DeleteBehavior.Restrict);

            //Data Seeding

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "User", CreatedBy = 1 },
                new Role { Id = 2, Name = "Admin", CreatedBy = 1 }
             );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Admin",
                    LastName = "EduPersona",
                    Email = "admin@edupersona.com",
                    PasswordHash = "Admin@123",
                    RoleId = 1,
                    CreatedBy = 1
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}