using System.Threading.Tasks;
using AspNetCore.EF.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.Vehicle
{
    public class UpdateVehicleCommandExecutor : ICommandExecutor<UpdateVehicleCommand>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public UpdateVehicleCommandExecutor(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(UpdateVehicleCommand command)
        {
            var vehicle = await UnitOfWork.DomainService<VehicleEntity>().FindAsync(command.VehicleId);
            vehicle.UserId = command.UserId;
            vehicle.PlateNumber = command.PlateNumber;

            await UnitOfWork.ModifyWithinStorageAsync(vehicle);
        }
    }
}
