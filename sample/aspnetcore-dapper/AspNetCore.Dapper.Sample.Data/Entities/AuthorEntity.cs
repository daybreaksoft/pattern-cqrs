using AspNetCore.Dapper.Sample.Data.Const;
using Daybreaksoft.Pattern.CQRS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
