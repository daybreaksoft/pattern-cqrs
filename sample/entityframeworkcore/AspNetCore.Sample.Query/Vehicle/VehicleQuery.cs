using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Sample.Data;

namespace AspNetCore.Sample.Query.Vehicle
{
    public class VehicleQuery : AbstractQuery<SampleDbContext>
    {
        public VehicleQuery(SampleDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<VehicleListItemViewModel>> GetVehicles()
        {
            var query = from v in Db.Vehicles
                join u in Db.Users on v.UserId equals u.Id
                select new VehicleListItemViewModel
                {
                    Id = v.Id,
                    PlateNumber = v.PlateNumber,
                    Username = u.Username,
                    UserPoint = u.Point
                };

            var vehicles = await query.ToListAsync();

            return vehicles;
        }

        public async Task<IEnumerable<VehicleSelectListItemViewModel>> GetVehiclesAsSelectListItem()
        {
            var query = from v in Db.Vehicles
                select new VehicleSelectListItemViewModel
                {
                    Id = v.Id,
                    PlateNumber = v.PlateNumber
                };

            var vehicles = await query.ToListAsync();

            return vehicles;
        }
    }
}
