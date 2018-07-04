using System.Threading.Tasks;
using AspNetCore.Dapper.Sample.Domain.Aggregates;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Command.BookType
{
    public class CreateBookTypeCommandExecutor : ICommandExecutor<CreateBookTypeCommand>
    {
        protected readonly IAggregateBus AggregateBus;

        public CreateBookTypeCommandExecutor(IAggregateBus aggregateBus)
        {
            AggregateBus = aggregateBus;
        }

        public async Task ExecuteAsync(CreateBookTypeCommand command)
        {
            var newAggregate = AggregateBus.BuildAggregate<BookTypeAggregate>();

            command.CopyValueTo(newAggregate);

            await newAggregate.AddAsync();
        }
    }
}
