using AspNetCore.EF.Sample.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.EF.Sample.Data
{
    public class SampleDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<VehicleEntity> Vehicles { get; set; }
        public DbSet<TrafficViolationEntity> TrafficViolations { get; set; }
        public DbSet<ConstEntity> Const { get; set; }
        public DbSet<ConstCategoryEntity> ConstCategories { get; set; }

        public SampleDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}