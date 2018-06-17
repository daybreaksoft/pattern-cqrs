using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command
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
            var model = AggregateBus.BuildAggregate<TrafficViolation>(command.TrafficViolationId);

            model.Remove();

            await Task.CompletedTask;
        }
    }
}
