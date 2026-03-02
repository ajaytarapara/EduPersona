using Microsoft.EntityFrameworkCore;

namespace EduPersona.Assesment.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}