using AspNetCore.EF.Sample.Core.Vehicle;
using AspNetCore.EF.Sample.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.EF.Sample.Core
{
    public class SampleDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<VehicleModel> Vehicles { get; set; }
        public DbSet<TrafficViolationEntity> TrafficViolations { get; set; }

        public SampleDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
