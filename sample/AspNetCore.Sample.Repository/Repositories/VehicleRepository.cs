using AspNetCore.Sample.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Sample.Repository.Repositories
{
    public class VehicleRepository : DefaultRepository<Vehicle>
    {
        public VehicleRepository(DbContext db) : base(db)
        {
        }
    }
}
