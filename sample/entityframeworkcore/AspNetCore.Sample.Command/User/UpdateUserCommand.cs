namespace AspNetCore.Sample.Command
{
    public class UpdateUserCommand : CreateUserCommand
    {
        public int UserId { get; set; }
    }
}
