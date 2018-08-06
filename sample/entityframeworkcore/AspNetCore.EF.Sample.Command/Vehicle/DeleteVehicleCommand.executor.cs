using System.Threading.Tasks;
using AspNetCore.EF.Sample.Core.Vehicle;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.Vehicle
{
    public class DeleteVehicleCommandExecutor : ICommandExecutor<DeleteVehicleCommand>
    {
        private readonly IApplicationService<VehicleModel> _vehicleService;

        public DeleteVehicleCommandExecutor(IApplicationService<VehicleModel> vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task ExecuteAsync(DeleteVehicleCommand command)
        {
            //await _vehicleService.DeleteAsync(command.VehicleId);
        }
    }
}
