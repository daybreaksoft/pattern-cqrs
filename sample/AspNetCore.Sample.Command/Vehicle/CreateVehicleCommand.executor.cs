using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command
{
    public class CreateVehicleCommandExecutor : ICommandExecutor<CreateVehicleCommand>
    {
        protected readonly IAggregateBus AggregateBus;

        public CreateVehicleCommandExecutor(IAggregateBus aggregateBus)
        {
            AggregateBus = aggregateBus;
        }

        public async Task ExecuteAsync(CreateVehicleCommand command)
        {
            var newModel = AggregateBus.BuildAggregate<Vehicle>();

            command.CopyValueTo(newModel);

            newModel.Add();

            await Task.CompletedTask;
        }
    }
}
