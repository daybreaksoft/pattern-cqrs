using System.Threading.Tasks;
using AspNetCore.EF.Sample.Core.TrafficViolation;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.EF.Sample.Command.TrafficViolation
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
            var newModel = AggregateBus.BuildAggregate<TrafficViolationModel>();

            command.CopyValueTo(newModel);

            await newModel.AddAsync();
        }
    }
}
