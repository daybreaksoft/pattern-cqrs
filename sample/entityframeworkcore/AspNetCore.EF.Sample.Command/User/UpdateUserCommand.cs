namespace AspNetCore.EF.Sample.Command.User
{
    public class UpdateUserCommand : CreateUserCommand
    {
        public int UserId { get; set; }
    }
}
