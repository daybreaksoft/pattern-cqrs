using System.Threading.Tasks;
using AspNetCore.Dapper.Sample.Domain.Aggregates;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Command.Book
{
    public class DeleteBookCommandExecutor : ICommandExecutor<DeleteBookCommand>
    {
        protected readonly IAggregateBus AggregateBus;

        public DeleteBookCommandExecutor(IAggregateBus aggregateBus)
        {
            AggregateBus = aggregateBus;
        }

        public async Task ExecuteAsync(DeleteBookCommand command)
        {
            var aggregate = AggregateBus.BuildAggregate<BookAggregate>(command.Id);

            await aggregate.RemoveAsync();
        }
    }
}
