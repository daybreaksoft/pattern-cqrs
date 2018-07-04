using System.ComponentModel.DataAnnotations.Schema;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Data.Entities
{
    [Table("Books")]
    public class BookEntity : IEntity
    {
        public int Id { get; set; }

        public int BookTypeId { get; set; }

        public string Name { get; set; }

        public int AuthorId { get; set; }
    }
}
