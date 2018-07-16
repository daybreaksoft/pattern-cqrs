using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.EF.Sample.Data;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.EF.Sample.Query.TrafficViolation
{
    public class TrafficViolationQuery : AbstractQuery<SampleDbContext>
    {
        public TrafficViolationQuery(SampleDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<TrafficViolationListItemViewModel>> GetTrafficViolations()
        {
            var query = from tv in Db.TrafficViolations
                join v in Db.Vehicles on tv.VehicleId equals v.Id
                select new TrafficViolationListItemViewModel
                {
                    Id = tv.Id,
                    VehiclePlateNumber = v.PlateNumber,
                    DeductPoint = tv.DeductPoint,
                };

            var vehicles = await query.ToListAsync();

            return vehicles;
        }
    }
}
