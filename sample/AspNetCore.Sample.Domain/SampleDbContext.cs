using AspNetCore.Sample.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Sample.Domain
{
    public class SampleDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<TrafficViolation> TrafficViolations { get; set; }

        public SampleDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
