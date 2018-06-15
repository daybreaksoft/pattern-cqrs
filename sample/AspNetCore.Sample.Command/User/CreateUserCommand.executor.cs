using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command
{
    public class CreateUserCommandExecutor : ICommandExecutor<CreateUserCommand>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public CreateUserCommandExecutor(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(CreateUserCommand command)
        {
            var newModel = UnitOfWork.BuildAggregate<Domain.Models.User>();

            command.CopyValueTo(newModel);

            newModel.Add();

            await Task.CompletedTask;
        }
    }
}
