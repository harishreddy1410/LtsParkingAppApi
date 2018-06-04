using AppDomain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace AppDomain.Contexts
{
    public class AppDbContext : DbContext,IContext
    {
        public string ConnectionString { get; private set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

            SqlServerOptionsExtension extension = options.FindExtension<SqlServerOptionsExtension>();

            if (extension != null)
                this.ConnectionString = extension.ConnectionString;
            else
                this.ConnectionString = this.Database.GetDbConnection().ConnectionString;
        }

        public DbSet<UserProfile> UserProfile { get; set; }

        public DbSet<EmployeeShift> EmployeeShift { get; set; }

        public DbSet<Company> Company { get; set; }

        public DbSet<Location> Location { get; set; }

        public DbSet<ParkingDivision> ParkingDivision { get; set; }

        public DbSet<ParkingSlot> ParkingSlot { get; set; }

        public DbSet<ParkingTraffic> ParkingTraffic { get; set; }

        public DbSet<Vehicle> Vehicle { get; set; }
    }
}
