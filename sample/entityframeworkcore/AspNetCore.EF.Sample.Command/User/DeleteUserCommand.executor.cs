using System.Threading.Tasks;
using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.EF.Sample.Command.User
{
    public class DeleteUserCommandExecutor : ICommandExecutor<DeleteUserCommand>
    {
        protected readonly IAggregateBus AggregateBus;

        public DeleteUserCommandExecutor(IAggregateBus aggregateBus)
        {
            AggregateBus = aggregateBus;
        }

        public async Task ExecuteAsync(DeleteUserCommand command)
        {
            //var model = AggregateBus.BuildAggregate<User>(command.UserId);

            //await model.RemoveAsync();
        }
    }
}
