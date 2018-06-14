using System.Threading.Tasks;
using AspNetCore.Sample.Command.User;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command.Vehicle
{
    public class DeleteVehicleCommandExecutor : ICommandExecutor<DeleteVehicleCommand>
    {
        protected readonly IDomainModelBuilder DomainModelBuilder;

        public DeleteVehicleCommandExecutor(IDomainModelBuilder domainModelBuilder)
        {
            DomainModelBuilder = domainModelBuilder;
        }

        public async Task ExecuteAsync(DeleteVehicleCommand command)
        {
            // Build vehicle
            var vehicleModel = DomainModelBuilder.BuildModel<VehicleModel>(command.VehicleId);

            // Remove vehicle
            await vehicleModel.RemoveAsync();
        }
    }
}
