using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Command.Author
{
    public class DeleteAuthorCommand : ICommand
    {
        public int Id { get; set; }
    }
}
