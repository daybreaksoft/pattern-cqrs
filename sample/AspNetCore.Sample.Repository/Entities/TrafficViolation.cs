using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Repository.Entities
{
    public class TrafficViolation : IEntity
    {
        [Key]
        public int TrafficViolationId { get; set; }

        [Required, ForeignKey("Vehicle")]
        public int VehicleId { get; set; }

        [Required]
        public int DeductPoint { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
