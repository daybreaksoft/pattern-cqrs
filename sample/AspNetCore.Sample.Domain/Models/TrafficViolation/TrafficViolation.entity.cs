using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Sample.Domain.Models
{
    public partial class TrafficViolation
    {
        [Key]
        public int TrafficViolationId { get; set; }

        [Required]
        public int VehicleId { get; set; }

        [Required]
        public int DeductPoint { get; set; }
    }
}
