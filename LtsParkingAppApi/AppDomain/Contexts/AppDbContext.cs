using AppDomain.Models;
using Microsoft.EntityFrameworkCore;


namespace AppDomain.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<EmployeeShift> EmployeeShift { get; set; }
    }
}
