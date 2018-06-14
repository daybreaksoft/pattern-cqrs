using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command.User
{
    public class CreateUserCommandExecutor : ICommandExecutor<CreateUserCommand>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public CreateUserCommandExecutor(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(CreateUserCommand command)
        {
            var newUserModel = UnitOfWork.BuildAggregate<Domain.Models.User>();
            command.CopyValueTo(newUserModel);
            //// Create user model via command values
            //var userModel = DomainModelBuilder.BuildModel<Domain.Models.User>();
            //command.CopyValueTo(userModel);

            //// Insert user
            //await userModel.AddAsync();
        }
    }
}
