using AspNetCore.Sample.Repository.Entities;
using Daybreaksoft.Pattern.CQRS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCore.Sample.Domain.Models
{
    public partial class TrafficViolation : AggregateRoot
    {
        [Key]
        public int TrafficViolationId { get; set; }

        [Required]
        public int VehicleId { get; set; }

        [Required]
        public int DeductPoint { get; set; }
    }
}
