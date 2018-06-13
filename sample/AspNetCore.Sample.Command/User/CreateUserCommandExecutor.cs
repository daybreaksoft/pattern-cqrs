using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command.User
{
    public class CreateUserCommandExecutor : ICommandExecutor<CreateUserCommand>
    {
        protected readonly IRepository<Repository.Entities.User> UserRepository;

        public CreateUserCommandExecutor(IRepository<Repository.Entities.User> userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task ExecuteAsync(CreateUserCommand command)
        {
            // Create user model via command values
            var userModel = new UserModel(UserRepository);
            command.CopyValueTo(userModel);

            // Insert user
            await userModel.AddAsync();
        }
    }
}
