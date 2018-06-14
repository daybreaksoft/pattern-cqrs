using System.Threading.Tasks;
using AspNetCore.Sample.Command.User;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command.Vehicle
{
    public class UpdateVehicleCommandExecutor : ICommandExecutor<UpdateVehicleCommand>
    {
        protected readonly IDomainModelBuilder DomainModelBuilder;

        public UpdateVehicleCommandExecutor(IDomainModelBuilder domainModelBuilder)
        {
            DomainModelBuilder = domainModelBuilder;
        }

        public async Task ExecuteAsync(UpdateVehicleCommand command)
        {
            //// Load vehicle
            //var vehicleModel = DomainModelBuilder.BuildModel<Domain.Models.Vehicle>(command.VehicleId);
            //await vehicleModel.LoadAsync();

            //// Copy value to model
            //command.CopyValueTo(vehicleModel);

            //// Update vehicle
            //await vehicleModel.UpdateAsync();
        }
    }
}
