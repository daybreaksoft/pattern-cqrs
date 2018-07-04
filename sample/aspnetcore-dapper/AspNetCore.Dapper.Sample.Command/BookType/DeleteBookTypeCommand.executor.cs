using System.Threading.Tasks;
using AspNetCore.Dapper.Sample.Domain.Aggregates;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Command.BookType
{
    public class DeleteBookTypeCommandExecutor : ICommandExecutor<DeleteBookTypeCommand>
    {
        protected readonly IAggregateBus AggregateBus;

        public DeleteBookTypeCommandExecutor(IAggregateBus aggregateBus)
        {
            AggregateBus = aggregateBus;
        }

        public async Task ExecuteAsync(DeleteBookTypeCommand command)
        {
            var aggregate = AggregateBus.BuildAggregate<BookTypeAggregate>(command.Id);

            await aggregate.RemoveAsync();
        }
    }
}
