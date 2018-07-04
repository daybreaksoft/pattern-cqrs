using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Command.BookType
{
    public class UpdateBookTypeCommand : CreateBookTypeCommand
    {
        public int Id { get; set; }
    }
}
