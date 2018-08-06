using System.Threading.Tasks;
using AspNetCore.EF.Sample.Core.User;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.User
{
    public class CreateUserCommandExecutor : ICommandExecutor<CreateUserCommand>
    {
        private readonly IApplicationService<UserModel> _userService;

        public CreateUserCommandExecutor(IApplicationService<UserModel> userService)
        {
            _userService = userService;
        }

        public async Task ExecuteAsync(CreateUserCommand command)
        {
            var user = new UserModel(command.Username, command.Point);

            await _userService.InsertAsync(user);
        }
    }
}
