using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command.User
{
    public class DeleteUserCommandExecutor : ICommandExecutor<DeleteUserCommand>
    {
        protected readonly IDomainModelBuilder DomainModelBuilder;

        public DeleteUserCommandExecutor(IDomainModelBuilder domainModelBuilder)
        {
            DomainModelBuilder = domainModelBuilder;
        }

        public async Task ExecuteAsync(DeleteUserCommand command)
        {
            // Build user
            var userModel = DomainModelBuilder.BuildModel<UserModel>(command.UserId);

            // Remove user
            await userModel.RemoveAsync();
        }
    }
}
