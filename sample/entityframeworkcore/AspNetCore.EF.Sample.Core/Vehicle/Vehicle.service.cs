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
        public VehicleService(IUnitOfWork unitOfWork, IRepository<VehicleEntity> repository) : base(unitOfWork, repository)
        {
        }

        protected override Task BeforeInsertAsync(VehicleModel aggregate)
        {
            return CheckPlateNumberUnique(aggregate);
        }

        protected override Task BeforeUpdateAsync(VehicleModel aggregate)
        {
            return CheckPlateNumberUnique(aggregate);
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