using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;

namespace AspNetCore.Sample.Command
{
    public class CreateUserCommand : ICommand
    {
        public string Username { get; set; }

        public int Point { get; set; }
    }
}
