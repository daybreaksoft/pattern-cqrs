using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Command.Book
{
    public class CreateBookCommand : ICommand
    {
        public string Name { get; set; }

        public int BookTypeId { get; set; }

        public int[] AuthorIds { get; set; }
    }
}
