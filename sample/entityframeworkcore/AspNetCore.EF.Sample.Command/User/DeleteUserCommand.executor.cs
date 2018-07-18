using System.Threading.Tasks;
using AspNetCore.EF.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.User
{
    public class DeleteUserCommandExecutor : ICommandExecutor<DeleteUserCommand>
    {
        protected readonly IDomainServiceFactory DomainAppServiceFactory;

        public DeleteUserCommandExecutor(IDomainServiceFactory domainAppServiceFactory)
        {
            DomainAppServiceFactory = domainAppServiceFactory;
        }

        public async Task ExecuteAsync(DeleteUserCommand command)
        {
            //var userAppService = DomainAppServiceFactory.GetDomainService<UserEntity>();

            //await userAppService.DeleteAsync(command.UserId);
        }
    }
}
