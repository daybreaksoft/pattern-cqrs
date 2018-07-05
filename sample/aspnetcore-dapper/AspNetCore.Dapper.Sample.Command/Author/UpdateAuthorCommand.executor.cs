using System.Threading.Tasks;
using AspNetCore.Dapper.Sample.Domain.Aggregates;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Command.Author
{
    public class UpdateAuthorCommandExecutor : ICommandExecutor<UpdateAuthorCommand>
    {
        protected readonly IAggregateBus AggregateBus;

        public UpdateAuthorCommandExecutor(IAggregateBus aggregateBus)
        {
            AggregateBus = aggregateBus;
        }

        public async Task ExecuteAsync(UpdateAuthorCommand command)
        {
            var aggregate = await AggregateBus.GetExsitsAggregate<AuthorAggregate>(command.Id);

            command.CopyValueTo(aggregate);

            await aggregate.ModifyAsync();
        }
    }
}
