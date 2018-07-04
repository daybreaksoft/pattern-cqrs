using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Command.BookType
{
    public class CreateBookTypeCommand : ICommand
    {
        public string Type { get; set; }
    }
}
