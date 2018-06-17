using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Data.Entities
{
    [Table("Vehicles")]
    public class VehicleEntity : IEntity
    {
        [Key]
        public int VehicleId { get; set; }

        [Required, ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string PlateNumber { get; set; }

        public virtual UserEntity UserEntity { get; set; }

        public virtual ICollection<TrafficViolationEntity> TrafficViolations { get; set; }
    }
}
