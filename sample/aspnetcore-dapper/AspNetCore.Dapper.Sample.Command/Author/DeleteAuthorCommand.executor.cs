using System.Threading.Tasks;
using AspNetCore.Dapper.Sample.Domain.Aggregates;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Command.Author
{
    public class DeleteAuthorCommandExecutor : ICommandExecutor<DeleteAuthorCommand>
    {
        protected readonly IAggregateBus AggregateBus;

        public DeleteAuthorCommandExecutor(IAggregateBus aggregateBus)
        {
            AggregateBus = aggregateBus;
        }

        public async Task ExecuteAsync(DeleteAuthorCommand command)
        {
            var aggregate = AggregateBus.BuildAggregate<AuthorAggregate>(command.Id);

            await aggregate.RemoveAsync();
        }
    }
}
