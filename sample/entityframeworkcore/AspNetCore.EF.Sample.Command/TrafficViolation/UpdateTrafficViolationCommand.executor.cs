using System.Threading.Tasks;
using AspNetCore.EF.Sample.Core.TrafficViolation;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.EF.Sample.Command.TrafficViolation
{
    public class UpdateTrafficViolationCommandExecutor : ICommandExecutor<UpdateTrafficViolationCommand>
    {
        protected readonly IAggregateBus AggregateBus;

        public UpdateTrafficViolationCommandExecutor(IAggregateBus aggregateBus)
        {
            AggregateBus = aggregateBus;
        }

        public async Task ExecuteAsync(UpdateTrafficViolationCommand command)
        {
            var model = await AggregateBus.GetExsitsAggregate<TrafficViolationModel>(command.TrafficViolationId);

            command.CopyValueTo(model);

            await model.ModifyAsync();
        }
    }
}
