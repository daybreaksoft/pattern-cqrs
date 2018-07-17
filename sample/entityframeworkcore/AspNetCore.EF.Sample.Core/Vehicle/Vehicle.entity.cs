using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Core.Vehicle
{
    [Table("Vehicles")]
    public class VehicleEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string PlateNumber { get; set; }
    }
}
