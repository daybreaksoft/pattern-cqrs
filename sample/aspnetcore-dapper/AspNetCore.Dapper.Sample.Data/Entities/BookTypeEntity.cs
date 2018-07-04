using Daybreaksoft.Pattern.CQRS;
using Dapper.Contrib.Extensions;

namespace AspNetCore.Dapper.Sample.Data.Entities
{
    [Table("BookTypes")]
    public class BookTypeEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Type { get; set; }
    }
}
