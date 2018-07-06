using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Dapper.Sample.Domain.Aggregates;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Command.Book
{
    public class UpdateBookCommandExecutor : ICommandExecutor<UpdateBookCommand>
    {
        protected readonly IAggregateBus AggregateBus;

        public UpdateBookCommandExecutor(IAggregateBus aggregateBus)
        {
            AggregateBus = aggregateBus;
        }

        public async Task ExecuteAsync(UpdateBookCommand command)
        {
            var aggregate = await AggregateBus.GetExsitsAggregate<BookAggregate>(command.Id);

            aggregate.Name = command.Name;
            aggregate.BookTypeId = command.BookTypeId;
            aggregate.AuthorIds = command.AuthorIds.ToList();

            await aggregate.ModifyAsync();
        }
    }
}
