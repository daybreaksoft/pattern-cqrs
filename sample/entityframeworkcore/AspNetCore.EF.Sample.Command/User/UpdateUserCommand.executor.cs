using System.Threading.Tasks;
using AspNetCore.EF.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.User
{
    public class UpdateUserCommandExecutor : ICommandExecutor<UpdateUserCommand>
    {
        protected readonly IDomainServiceFactory DomainAppServiceFactory;

        public UpdateUserCommandExecutor(IDomainServiceFactory domainAppServiceFactory)
        {
            DomainAppServiceFactory = domainAppServiceFactory;
        }

        public async Task ExecuteAsync(UpdateUserCommand command)
        {
            //var userAppService = DomainAppServiceFactory.GetDomainService<UserEntity>();

            //var userModel = await userAppService.FindAsync(command.UserId);
            //userModel.Username = command.Username;
            //userModel.Point = command.Point;

            //await userAppService.UpdateAsync(userModel);
        }
    }
}
