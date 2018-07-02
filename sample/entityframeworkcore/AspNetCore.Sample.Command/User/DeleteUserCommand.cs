using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command
{
    public class DeleteUserCommand : ICommand
    {
        public int UserId { get; set; }
    }
}
