using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command.Vehicle
{
    public class CreateVehicleCommandExecutor : ICommandExecutor<CreateVehicleCommand>
    {
        protected readonly IDomainModelBuilder DomainModelBuilder;

        public CreateVehicleCommandExecutor(IDomainModelBuilder domainModelBuilder)
        {
            DomainModelBuilder = domainModelBuilder;
        }

        public async Task ExecuteAsync(CreateVehicleCommand command)
        {
            //// Create vehicle model via command values
            //var vehicleModel = DomainModelBuilder.BuildModel<Domain.Models.Vehicle>();
            //command.CopyValueTo(vehicleModel);

            //// Insert vehicle
            //await vehicleModel.AddAsync();
        }
    }
}
