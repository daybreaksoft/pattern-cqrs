using System;
using System.Linq;
using System.Threading.Tasks;
using Daybreaksoft.Pattern.CQRS.DomainModel;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.EF.Sample.Core.Vehicle
{
    public class VehicleAppService : AbstractDomainAppService<VehicleModel, VehicleEntity>
    {
        public VehicleAppService(IRepository<VehicleEntity> repository) : base(repository)
        {
        }

        public override async Task InsertAsync(VehicleModel aggregate)
        {
            await CheckPlateNumberUnique(aggregate);

            await base.InsertAsync(aggregate);
        }

        public override async Task UpdateAsync(VehicleModel aggregate)
        {
            await CheckPlateNumberUnique(aggregate);

            await base.UpdateAsync(aggregate);
        }

        #region Constraint

        private async Task CheckPlateNumberUnique(VehicleModel aggregate)
        {
            var queryable = Repository.GetQueryable();

            var id = Convert.ToInt32(aggregate.Id);

            if (await queryable.Where(p => p.PlateNumber == aggregate.PlateNumber && p.Id != id).AnyAsync())
            {
                throw new Exception($"Vehicle {aggregate.PlateNumber} already exists.");
            }
        }

        #endregion

        #region Data Transfer

        protected override void CopyValueToEntity(VehicleEntity entity, VehicleModel aggregate)
        {
            entity.UserId = aggregate.UserId;
            entity.PlateNumber = aggregate.PlateNumber;
        }

        protected override VehicleModel ConvertToAggregate(VehicleEntity entity)
        {
            return new VehicleModel(entity.Id, entity.UserId, entity.PlateNumber);
        }

        #endregion
    }
}