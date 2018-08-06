using System.Threading.Tasks;
using AspNetCore.EF.Sample.Core.User;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.User
{
    public class DeleteUserCommandExecutor : ICommandExecutor<DeleteUserCommand>
    {
        private readonly IApplicationService<UserModel> _userService;

        public DeleteUserCommandExecutor(IApplicationService<UserModel> userService)
        {
            _userService = userService;
        }

        public async Task ExecuteAsync(DeleteUserCommand command)
        {
            await _userService.DeleteAsync(command.UserId);
        }
    }
}
