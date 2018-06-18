using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command
{
    public class CreateUserCommandExecutor : ICommandExecutor<CreateUserCommand>
    {
        protected readonly IAggregateBus AggregateBus;

        public CreateUserCommandExecutor(IAggregateBus aggregateBus)
        {
            AggregateBus = aggregateBus;
        }

        public async Task ExecuteAsync(CreateUserCommand command)
        {
            var newModel = AggregateBus.BuildAggregate<User>();

            command.CopyValueTo(newModel);

            await newModel.AddAsync();
        }
    }
}
