namespace AspNetCore.Dapper.Sample.Query
{
    public class BookTypeViewModel
    {
        public int Id { get; set; }

        public string Type { get; set; }
    }

    public class BookTypeListItemViewModel : BookTypeViewModel
    {
    }
}
