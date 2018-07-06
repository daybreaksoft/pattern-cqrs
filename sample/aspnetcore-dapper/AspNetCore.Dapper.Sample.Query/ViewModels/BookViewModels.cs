using System.Collections.Generic;

namespace AspNetCore.Dapper.Sample.Query.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int BookTypeId { get; set; }

        public IEnumerable<int> AuthorIds { get; set; }
    }

    public class BookListItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }
}
