using Daybreaksoft.Pattern.CQRS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCore.Dapper.Sample.Data.Entities
{
    [Table("Books")]
    public class BookEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int BookTypeId { get; set; }

        public string Name { get; set; }

        public int AuthorId { get; set; }
    }
}
