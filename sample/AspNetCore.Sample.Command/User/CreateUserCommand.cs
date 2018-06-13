using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command.User
{
    public class CreateUserCommand : ICommand
    {
        public string Username { get; set; }

        public int Point { get; set; }
    }
}
