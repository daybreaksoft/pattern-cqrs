using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using AspNetCore.Sample.Domain.Models.UserDomain;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.Sample.Command
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
            var user = new User(command.Username, command.Point);

            var userAppService = DomainAppServiceFactory.GetDomainAppService<User>();

            await userAppService.InsertAsync(user);
        }
    }
}
