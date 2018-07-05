using Daybreaksoft.Pattern.CQRS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCore.Dapper.Sample.Data.Entities
{
    [Table("BookAuthors")]
    public class BookAuthorEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int BookId { get; set; }

        public int AuthorId { get; set; }
    }
}
