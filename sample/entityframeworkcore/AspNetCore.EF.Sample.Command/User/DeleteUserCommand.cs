using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.EF.Sample.Command.User
{
    public class DeleteUserCommand : ICommand
    {
        public int UserId { get; set; }
    }
}
