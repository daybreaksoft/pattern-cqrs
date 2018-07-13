using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using AspNetCore.Sample.Domain.Models.UserDomain;
using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.Sample.Command
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
