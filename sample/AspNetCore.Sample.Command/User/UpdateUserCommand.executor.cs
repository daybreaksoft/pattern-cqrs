using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command
{
    public class UpdateUserCommandExecutor : ICommandExecutor<UpdateUserCommand>
    {
        protected readonly IAggregateBus AggregateBus;

        public UpdateUserCommandExecutor(IAggregateBus aggregateBus)
        {
            AggregateBus = aggregateBus;
        }

        public async Task ExecuteAsync(UpdateUserCommand command)
        {
            var model = await AggregateBus.GetExsitsAggregate<User>(command.UserId);

            command.CopyValueTo(model);

            await model.ModifyAsync();
        }
    }
}
