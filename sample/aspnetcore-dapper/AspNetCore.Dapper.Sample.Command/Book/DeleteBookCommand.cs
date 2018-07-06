using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Command.Book
{
    public class DeleteBookCommand : ICommand
    {
        public int Id { get; set; }
    }
}
