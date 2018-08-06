using System.Threading.Tasks;
using AspNetCore.EF.Sample.Core.Vehicle;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.Vehicle
{
    public class UpdateVehicleCommandExecutor : ICommandExecutor<UpdateVehicleCommand>
    {
        private readonly IApplicationService<VehicleModel> _vehicleService;

        public UpdateVehicleCommandExecutor(IApplicationService<VehicleModel> vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task ExecuteAsync(UpdateVehicleCommand command)
        {
            var vehicle = new VehicleModel(command.VehicleId, command.UserId, command.PlateNumber);

            _vehicleService.Update(vehicle);

            await Task.CompletedTask;
        }
    }
}
