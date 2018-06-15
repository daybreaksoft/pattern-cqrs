using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Sample.Domain.Models
{
    public partial class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string PlateNumber { get; set; }
    }
}
