using System.Threading.Tasks;
using AspNetCore.EF.Sample.Core.Vehicle;
using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.EF.Sample.Command.Vehicle
{
    public class DeleteVehicleCommandExecutor : ICommandExecutor<DeleteVehicleCommand>
    {
        protected readonly IAggregateBus AggregateBus;

        public DeleteVehicleCommandExecutor(IAggregateBus aggregateBus)
        {
            AggregateBus = aggregateBus;
        }

        public async Task ExecuteAsync(DeleteVehicleCommand command)
        {
            var model = AggregateBus.BuildAggregate<VehicleModel>(command.VehicleId);

            //await model.RemoveAsync();

            await Task.CompletedTask;
        }
    }
}
