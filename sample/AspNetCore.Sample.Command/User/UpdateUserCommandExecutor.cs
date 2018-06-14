using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command.User
{
    public class UpdateUserCommandExecutor : ICommandExecutor<UpdateUserCommand>
    {
        protected readonly IDomainModelBuilder DomainModelBuilder;

        public UpdateUserCommandExecutor(IDomainModelBuilder domainModelBuilder)
        {
            DomainModelBuilder = domainModelBuilder;
        }

        public async Task ExecuteAsync(UpdateUserCommand command)
        {
            //// Load user
            //var userModel = DomainModelBuilder.BuildModel<Domain.Models.User>(command.UserId);
            //await userModel.LoadAsync();

            //// Copy value to model
            //command.CopyValueTo(userModel);

            //// Update user
            //await userModel.UpdateAsync();
        }
    }
}
