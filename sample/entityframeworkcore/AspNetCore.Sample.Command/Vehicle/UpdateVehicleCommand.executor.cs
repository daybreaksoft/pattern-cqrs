using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command
{
    public class UpdateVehicleCommandExecutor : ICommandExecutor<UpdateVehicleCommand>
    {
        protected readonly IAggregateBus AggregateBus;

        public UpdateVehicleCommandExecutor(IAggregateBus aggregateBus)
        {
            AggregateBus = aggregateBus;
        }

        public async Task ExecuteAsync(UpdateVehicleCommand command)
        {
            var model = await AggregateBus.GetExsitsAggregate<Vehicle>(command.VehicleId);

            await model.ModifyAsync();

            command.CopyValueTo(model);
        }
    }
}
