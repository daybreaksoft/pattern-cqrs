﻿using AspNetCore.Sample.Repository;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Sample.Query.Vehicle
{
    public class VehicleQuery : QueryBase<SampleDbContext>
    {
        public VehicleQuery(SampleDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<VehicleListItemViewModel>> GetVehicles()
        {
            var query = from v in Db.Vehicles
                join u in Db.Users on v.UserId equals u.UserId
                select new VehicleListItemViewModel
                {
                    VehicleId = v.VehicleId,
                    PlateNumber = v.PlateNumber,
                    Username = u.Username,
                    UserPoint = u.Point
                };

            var vehicles = await query.ToListAsync();

            return vehicles;
        }
    }
}
