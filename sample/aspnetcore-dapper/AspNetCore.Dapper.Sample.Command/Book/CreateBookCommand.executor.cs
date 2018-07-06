using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Dapper.Sample.Domain.Aggregates;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Command.Book
{
    public class CreateBookCommandExecutor : ICommandExecutor<CreateBookCommand>
    {
        protected readonly IAggregateBus AggregateBus;

        public CreateBookCommandExecutor(IAggregateBus aggregateBus)
        {
            AggregateBus = aggregateBus;
        }

        public async Task ExecuteAsync(CreateBookCommand command)
        {
            var newAggregate = AggregateBus.BuildAggregate<BookAggregate>();

            newAggregate.Name = command.Name;
            newAggregate.BookTypeId = command.BookTypeId;
            newAggregate.AuthorIds = command.AuthorIds.ToList();

            await newAggregate.AddAsync();
        }
    }
}
