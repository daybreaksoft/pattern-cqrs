using AspNetCore.Dapper.Sample.Data.Const;

namespace AspNetCore.Dapper.Sample.Query.ViewModels
{
    public class AuthorViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SexConst Sex { get; set; }
    }

    public class AuthorListItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Sex { get; set; }
    }
}
