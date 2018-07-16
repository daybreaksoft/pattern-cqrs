using System.Threading.Tasks;
using AspNetCore.EF.Sample.Core.User;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.EF.Sample.Command.User
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
            var model = await AggregateBus.GetExsitsAggregate<UserModel>(command.UserId);

            command.CopyValueTo(model);

            //await model.ModifyAsync();
        }
    }
}
