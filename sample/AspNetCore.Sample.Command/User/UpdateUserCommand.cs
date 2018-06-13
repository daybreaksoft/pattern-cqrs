namespace AspNetCore.Sample.Command.User
{
    public class UpdateUserCommand : CreateUserCommand
    {
        public int UserId { get; set; }
    }
}
