using AspNetCore.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Sample.Domain.Models
{
    public class VehicleRepository : AbstractRepository<Vehicle, VehicleEntity>
    {
        public VehicleRepository(DbContext db, IAggregateBus aggregateBus) : base(db, aggregateBus)
        {
        }
    }
}
