using System.Threading.Tasks;
using AspNetCore.EF.Sample.Core.User;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.User
{
    public class CreateUserCommandExecutor : ICommandExecutor<CreateUserCommand>
    {
        protected readonly IDomainAppServiceFactory DomainAppServiceFactory;

        public CreateUserCommandExecutor(IDomainAppServiceFactory domainAppServiceFactory)
        {
            DomainAppServiceFactory = domainAppServiceFactory;
        }

        public async Task ExecuteAsync(CreateUserCommand command)
        {
            var user = new UserModel(command.Username, command.Point);

            var userAppService = DomainAppServiceFactory.GetDomainAppService<UserModel>();

            await userAppService.InsertAsync(user);
        }
    }
}
