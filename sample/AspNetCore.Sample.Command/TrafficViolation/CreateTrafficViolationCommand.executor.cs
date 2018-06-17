using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command
{
    public class CreateTrafficViolationCommandExecutor : ICommandExecutor<CreateTrafficViolationCommand>
    {
        protected readonly IAggregateBus AggregateBus;

        public CreateTrafficViolationCommandExecutor(IAggregateBus aggregateBus)
        {
            AggregateBus = aggregateBus;
        }

        public async Task ExecuteAsync(CreateTrafficViolationCommand command)
        {
            var newModel = AggregateBus.BuildAggregate<TrafficViolation>();

            command.CopyValueTo(newModel);

            newModel.Add();

            await Task.CompletedTask;
        }
    }
}
