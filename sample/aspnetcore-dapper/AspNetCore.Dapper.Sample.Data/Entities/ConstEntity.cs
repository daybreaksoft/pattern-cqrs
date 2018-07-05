using Daybreaksoft.Pattern.CQRS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCore.Dapper.Sample.Data.Entities
{
    [Table("Const")]
    public class ConstEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string DisplayText { get; set; }

        public int Description { get; set; }

        public bool Enbaled { get; set; }
    }
}
