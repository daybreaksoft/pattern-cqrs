using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command.User
{
    public class CreateUserCommandExecutor : ICommandExecutor<CreateUserCommand>
    {
        protected readonly IDomainModelBuilder DomainModelBuilder;

        public CreateUserCommandExecutor(IDomainModelBuilder domainModelBuilder)
        {
            DomainModelBuilder = domainModelBuilder;
        }

        public async Task ExecuteAsync(CreateUserCommand command)
        {
            // Create user model via command values
            var userModel = DomainModelBuilder.BuildModel<UserModel>();
            command.CopyValueTo(userModel);

            // Insert user
            await userModel.AddAsync();
        }
    }
}
