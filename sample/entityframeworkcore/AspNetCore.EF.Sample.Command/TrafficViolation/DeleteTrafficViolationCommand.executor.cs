using System.Threading.Tasks;
using AspNetCore.EF.Sample.Core.TrafficViolation;
using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.EF.Sample.Command.TrafficViolation
{
    public class DeleteTrafficViolationCommandExecutor : ICommandExecutor<DeleteTrafficViolationCommand>
    {
        protected readonly IAggregateBus AggregateBus;

        public DeleteTrafficViolationCommandExecutor(IAggregateBus aggregateBus)
        {
            AggregateBus = aggregateBus;
        }

        public async Task ExecuteAsync(DeleteTrafficViolationCommand command)
        {
            var model = AggregateBus.BuildAggregate<TrafficViolationModel>(command.TrafficViolationId);

            await model.RemoveAsync();
        }
    }
}
