using System.Threading.Tasks;
using AspNetCore.EF.Sample.Core.User;
using AspNetCore.EF.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.User
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
            var userModel = new UserModel(command.UserId, command.Username, command.Point);

            await UnitOfWork.ModifyWithinStorageAsync(userModel);
        }
    }
}
