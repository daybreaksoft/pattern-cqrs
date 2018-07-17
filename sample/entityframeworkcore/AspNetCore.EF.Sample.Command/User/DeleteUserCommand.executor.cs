﻿using System.Threading.Tasks;
using AspNetCore.EF.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.User
{
    public class DeleteUserCommandExecutor : ICommandExecutor<DeleteUserCommand>
    {
        protected readonly IDomainAppServiceFactory DomainAppServiceFactory;

        public DeleteUserCommandExecutor(IDomainAppServiceFactory domainAppServiceFactory)
        {
            DomainAppServiceFactory = domainAppServiceFactory;
        }

        public async Task ExecuteAsync(DeleteUserCommand command)
        {
            var userAppService = DomainAppServiceFactory.GetDomainAppService<UserEntity>();

            await userAppService.DeleteAsync(command.UserId);
        }
    }
}
