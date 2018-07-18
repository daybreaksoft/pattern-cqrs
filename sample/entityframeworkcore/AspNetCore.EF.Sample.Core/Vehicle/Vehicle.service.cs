using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.EF.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS.DomainModel;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.EF.Sample.Core.Vehicle
{

    public class VehicleService : SimpleDomainService<VehicleEntity>
    {
        public VehicleService(IRepository<VehicleEntity> repository) : base(repository)
        {
        }

        public override async Task InsertAsync(VehicleEntity aggregate)
        {
            await CheckPlateNumberUnique(aggregate);

            await base.InsertAsync(aggregate);
        }

        public override async Task UpdateAsync(VehicleEntity aggregate)
        {
            await CheckPlateNumberUnique(aggregate);

            await base.UpdateAsync(aggregate);
        }

        #region Constraint

        private async Task CheckPlateNumberUnique(VehicleEntity aggregate)
        {
            var queryable = Repository.GetQueryable();

            if (await queryable.Where(p => p.PlateNumber == aggregate.PlateNumber && p.Id != aggregate.Id).AnyAsync())
            {
                throw new Exception($"Vehicle {aggregate.PlateNumber} already exists.");
            }
        }

        #endregion
    }
}