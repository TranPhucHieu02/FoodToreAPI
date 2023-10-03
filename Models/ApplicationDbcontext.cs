using Microsoft.EntityFrameworkCore;

namespace apiforapp.Models
{
    public class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options)
        : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Role> Roles { get; set; }

    }
}
