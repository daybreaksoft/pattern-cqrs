using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.Sample.Data.Entities
{
    [Table("TrafficViolations")]
    public class TrafficViolationEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("Vehicle")]
        public int VehicleId { get; set; }

        [Required]
        public int DeductPoint { get; set; }

        public virtual VehicleEntity Vehicle { get; set; }
    }
}
