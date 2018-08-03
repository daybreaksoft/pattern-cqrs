using System.Threading.Tasks;
using AspNetCore.EF.Sample.Core.Vehicle;
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
            var vehicle = new VehicleModel(command.VehicleId, command.UserId, command.PlateNumber);

            await UnitOfWork.ModifyWithinStorageAsync(vehicle);
        }
    }
}
