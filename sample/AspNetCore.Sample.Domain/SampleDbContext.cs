
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using AspNetCore.Sample.Domain.Models;

namespace AspNetCore.Sample.Repository
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
