using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command.User
{
    public class DeleteUserCommand : ICommand
    {
        public int UserId { get; set; }
    }
}
