using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command.User
{
    public class UpdateUserCommandExecutor : ICommandExecutor<UpdateUserCommand>
    {
        protected readonly IRepository<Repository.Entities.User> UserRepository;

        public UpdateUserCommandExecutor(IRepository<Repository.Entities.User> userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task ExecuteAsync(UpdateUserCommand command)
        {
            // Load user
            var userModel = new UserModel(command.UserId, UserRepository);
            await userModel.LoadAsync();

            // Copy value to model
            command.CopyValueTo(userModel);

            // Update user
            await userModel.UpdateAsync();
        }
    }
}
