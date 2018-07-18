using System.ComponentModel.DataAnnotations;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Data.Entities
{
    public class ConstEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string DisplayText { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }
    }
}
