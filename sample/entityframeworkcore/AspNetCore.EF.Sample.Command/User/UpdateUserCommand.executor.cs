using System.Threading.Tasks;
using AspNetCore.EF.Sample.Core.User;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.User
{
    public class UpdateUserCommandExecutor : ICommandExecutor<UpdateUserCommand>
    {
        protected readonly IDomainAppServiceFactory DomainAppServiceFactory;

        public UpdateUserCommandExecutor(IDomainAppServiceFactory domainAppServiceFactory)
        {
            DomainAppServiceFactory = domainAppServiceFactory;
        }

        public async Task ExecuteAsync(UpdateUserCommand command)
        {
            var userAppService = DomainAppServiceFactory.GetDomainAppService<UserModel>();

            var userModel = await userAppService.FindAsync(command.UserId);
            userModel.Username = command.Username;
            userModel.Point = command.Point;

            await userAppService.UpdateAsync(userModel);
        }
    }
}
