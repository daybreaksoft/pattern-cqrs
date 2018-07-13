using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.Sample.Command
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
            var model = await AggregateBus.GetExsitsAggregate<TrafficViolation>(command.TrafficViolationId);

            command.CopyValueTo(model);

            await model.ModifyAsync();
        }
    }
}
