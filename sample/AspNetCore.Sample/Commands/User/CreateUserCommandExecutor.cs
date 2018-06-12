using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Commands.User
{
    public class CreateUserCommandExecutor : ICommandExecutor<SubmitUserCommand>
    {
        protected readonly IRepository<Repository.Entities.User> UserRepository;

        public CreateUserCommandExecutor(IRepository<Repository.Entities.User> userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task ExecuteAsync(SubmitUserCommand command)
        {
            // Create user model via command values
            var userModel = new UserModel(UserRepository);
            command.CopyValueTo(userModel);

            // Insert user
            await userModel.AddAsync();
        }
    }
}
