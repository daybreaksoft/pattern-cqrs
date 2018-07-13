using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.Sample.Command
{
    public class DeleteUserCommand : ICommand
    {
        public int UserId { get; set; }
    }
}
