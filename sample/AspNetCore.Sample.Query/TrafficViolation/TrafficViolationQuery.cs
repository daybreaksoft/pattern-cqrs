using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Sample.Repository;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Sample.Query.TrafficViolation
{
    public class TrafficViolationQuery : AbstractQuery<SampleDbContext>
    {
        public TrafficViolationQuery(SampleDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<TrafficViolationListItemViewModel>> GetTrafficViolations()
        {
            var query = from tv in Db.TrafficViolations
                join v in Db.Vehicles on tv.VehicleId equals v.VehicleId
                select new TrafficViolationListItemViewModel
                {
                    TrafficViolationId = tv.TrafficViolationId,
                    VehiclePlateNumber = v.PlateNumber,
                    DeductPoint = tv.DeductPoint,
                };

            var vehicles = await query.ToListAsync();

            return vehicles;
        }
    }
}
