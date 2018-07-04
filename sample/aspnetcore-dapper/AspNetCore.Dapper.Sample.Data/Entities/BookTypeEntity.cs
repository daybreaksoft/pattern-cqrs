using System.ComponentModel.DataAnnotations.Schema;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Data.Entities
{
    [Table("BookTypes")]
    public class BookTypeEntity : IEntity
    {
        public int Id { get; set; }

        public string Type { get; set; }
    }
}
