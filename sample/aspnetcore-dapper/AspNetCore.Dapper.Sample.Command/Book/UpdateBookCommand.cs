namespace AspNetCore.Dapper.Sample.Command.Book
{
    public class UpdateBookCommand : CreateBookCommand
    {
        public int Id { get; set; }
    }
}
