using System.Threading.Tasks;
using AspNetCore.Dapper.Sample.Domain.Aggregates;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Command.BookType
{
    public class UpdateBookTypeCommandExecutor : ICommandExecutor<UpdateBookTypeCommand>
    {
        protected readonly IAggregateBus AggregateBus;

        public UpdateBookTypeCommandExecutor(IAggregateBus aggregateBus)
        {
            AggregateBus = aggregateBus;
        }

        public async Task ExecuteAsync(UpdateBookTypeCommand command)
        {
            var aggregate = await AggregateBus.GetExsitsAggregate<BookTypeAggregate>(command.Id);

            command.CopyValueTo(aggregate);

            await aggregate.ModifyAsync();
        }
    }
}
