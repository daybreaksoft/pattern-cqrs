using Daybreaksoft.Pattern.CQRS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
