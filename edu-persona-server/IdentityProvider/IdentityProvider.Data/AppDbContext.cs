

using IdentityProvider.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityProvider.Data
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
               new Role { Id = 2, Name = "User" },
               new Role { Id = 1, Name = "Admin" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Admin",
                    LastName = "EduPersona",
                    Email = "admin@edupersona.com",
                    PasswordHash = "6G94qKPK8LYNjnTllCqm2G3BUM08AzOK7yW30tfjrMc=",
                    RoleId = 1
                }
            );

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = 1,
                    AppName = "User Profile App",
                    ClientSecret = "PIY135_USER_PROFILE_APP_531yip",
                    RedirectUris = "https://profile.app/callback",
                    PostLogoutUris = "https://profile.app/logout",
                    IsActive = true
                },
                new Client
                {
                    Id = 2,
                    AppName = "Exam App",
                    ClientSecret = "ADG086_EXAM_APP_680adg",
                    RedirectUris = "https://exam.app/callback",
                    PostLogoutUris = "https://exam.app/logout",
                    IsActive = true
                }
            );
            //Data Seeding
            base.OnModelCreating(modelBuilder);
        }
    }
}