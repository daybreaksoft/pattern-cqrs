using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Command.BookType
{
    public class DeleteBookTypeCommand : ICommand
    {
        public int Id { get; set; }
    }
}
