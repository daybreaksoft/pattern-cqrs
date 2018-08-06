using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.EF.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS.DomainModel;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.EF.Sample.Core.Vehicle
{
    public class VehicleService : SimpleApplicationService<VehicleModel, VehicleEntity>
    {
        public VehicleService(IRepository<VehicleEntity> repository) : base(repository)
        {
        }

        protected override async Task BeforeInsertAsync(VehicleModel aggregate)
        {
            await CheckPlateNumberUnique(aggregate);
        }

        protected override async Task BeforeUpdateAsync(VehicleModel aggregate)
        {
            await CheckPlateNumberUnique(aggregate);
        }

        #region Constraint

        private async Task CheckPlateNumberUnique(VehicleModel aggregate)
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