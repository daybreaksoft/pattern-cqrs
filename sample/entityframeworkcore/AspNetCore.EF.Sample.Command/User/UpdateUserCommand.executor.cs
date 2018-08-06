using System.Threading.Tasks;
using AspNetCore.EF.Sample.Core.User;
using AspNetCore.EF.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.User
{
    public class UpdateUserCommandExecutor : ICommandExecutor<UpdateUserCommand>
    {
        private readonly IApplicationService<UserModel> _userService;

        public UpdateUserCommandExecutor(IApplicationService<UserModel> userService)
        {
            _userService = userService;
        }

        public async Task ExecuteAsync(UpdateUserCommand command)
        {
            var userModel = new UserModel(command.UserId, command.Username, command.Point);

            await _userService.UpdateAsync(userModel);
        }
    }
}
