using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command.User
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
            var userModel = UnitOfWork.BuildAggregate<Domain.Models.User>();
            userModel.UserId = command.UserId;
            
            userModel.Remove();
        }
    }
}
