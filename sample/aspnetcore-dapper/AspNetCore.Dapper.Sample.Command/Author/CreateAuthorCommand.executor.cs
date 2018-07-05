using System.Threading.Tasks;
using AspNetCore.Dapper.Sample.Domain.Aggregates;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Command.Author
{
    public class CreateAuthorCommandExecutor : ICommandExecutor<CreateAuthorCommand>
    {
        protected readonly IAggregateBus AggregateBus;

        public CreateAuthorCommandExecutor(IAggregateBus aggregateBus)
        {
            AggregateBus = aggregateBus;
        }

        public async Task ExecuteAsync(CreateAuthorCommand command)
        {
            var newAggregate = AggregateBus.BuildAggregate<AuthorAggregate>();

            command.CopyValueTo(newAggregate);

            await newAggregate.AddAsync();
        }
    }
}
