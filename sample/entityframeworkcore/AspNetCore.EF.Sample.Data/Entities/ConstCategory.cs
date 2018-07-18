using System.ComponentModel.DataAnnotations;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Data.Entities
{
    public class ConstCategoryEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Category { get; set; }

        public bool Enabled { get; set; }
    }
}
