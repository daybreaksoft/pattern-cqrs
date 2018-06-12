using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Repository.Entities
{
    public class Vehicle : IEntity
    {
        [Key]
        public int VehicleId { get; set; }

        [Required, ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string PlateNumber { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<TrafficViolation> TrafficViolations { get; set; }
    }
}
