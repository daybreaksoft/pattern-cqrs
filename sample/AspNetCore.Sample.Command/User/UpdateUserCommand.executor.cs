using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command
{
    public class UpdateUserCommandExecutor : ICommandExecutor<UpdateUserCommand>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public UpdateUserCommandExecutor(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(UpdateUserCommand command)
        {
            var model = await UnitOfWork.GetAggregate<Domain.Models.User>(command.UserId);

            command.CopyValueTo(model);
        }
    }
}
