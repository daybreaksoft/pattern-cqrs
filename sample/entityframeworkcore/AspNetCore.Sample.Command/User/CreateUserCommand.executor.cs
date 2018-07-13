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
        protected readonly IDomainServiceFactory DomainServiceFactory;

        public CreateUserCommandExecutor(IDomainServiceFactory domainServiceFactory)
        {
            DomainServiceFactory = domainServiceFactory;
        }

        public async Task ExecuteAsync(CreateUserCommand command)
        {
            var model = new User(command.Username, command.Point);

            //var userDomainAppService = DomainAppServiceFactory.GetDomainAppService<User>();
        }
    }
}
