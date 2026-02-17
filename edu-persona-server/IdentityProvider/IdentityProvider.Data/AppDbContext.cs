

using IdentityProvider.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduPersona.Core.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
               new Role { Id = 1, Name = "User"  },
               new Role { Id = 2, Name = "Admin" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Admin",
                    LastName = "EduPersona",
                    Email = "admin@edupersona.com",
                    PasswordHash = "Admin@123",
                    RoleId = 1
                }
            );
            //Data Seeding
            base.OnModelCreating(modelBuilder);
        }
    }
}