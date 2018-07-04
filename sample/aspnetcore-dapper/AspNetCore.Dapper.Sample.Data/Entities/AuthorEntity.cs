using AspNetCore.Dapper.Sample.Data.Const;
using Dapper.Contrib.Extensions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Data.Entities
{
    [Table("Authors")]
    public class AuthorEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public SexConst Sex { get; set; }
    }
}
