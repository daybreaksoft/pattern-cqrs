using AspNetCore.EF.Sample.Data.Const;
using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.EF.Sample.Command.User
{
    public class CreateUserCommand : ICommand
    {
        public string Username { get; set; }

        public int Point { get; set; }

        public UserRoleConst[] Roles { get; set; }
    }
}
