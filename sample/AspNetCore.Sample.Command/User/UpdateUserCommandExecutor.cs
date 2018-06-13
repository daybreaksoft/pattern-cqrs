using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command.User
{
    public class UpdateUserCommandExecutor : ICommandExecutor<UpdateUserCommand>
    {
        protected readonly IRepository<Repository.Entities.User> UserRepository;
        protected readonly IDomainModelBuilder DomainModelBuilder;

        public UpdateUserCommandExecutor(IRepository<Repository.Entities.User> userRepository, IDomainModelBuilder domainModelBuilder)
        {
            UserRepository = userRepository;
            DomainModelBuilder = domainModelBuilder;
        }

        public async Task ExecuteAsync(UpdateUserCommand command)
        {
            // Load user
            var userModel = new UserModel(UserRepository);
            userModel.Id = command.UserId;
            await userModel.LoadAsync();

            // Copy value to model
            command.CopyValueTo(userModel);

            // Update user
            await userModel.UpdateAsync();
        }
    }
}
