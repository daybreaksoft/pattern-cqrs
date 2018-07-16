using System.Threading.Tasks;
using AspNetCore.EF.Sample.Core.Vehicle;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.EF.Sample.Command.Vehicle
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
            var model = await AggregateBus.GetExsitsAggregate<VehicleModel>(command.VehicleId);

            //await model.ModifyAsync();

            command.CopyValueTo(model);
        }
    }
}
