using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command
{
    public class DeleteUserCommandExecutor : ICommandExecutor<DeleteUserCommand>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public DeleteUserCommandExecutor(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(DeleteUserCommand command)
        {
            var model = UnitOfWork.BuildAggregate<User>();
            model.Id = command.UserId;

            model.Remove();

            await Task.CompletedTask;
        }
    }
}
