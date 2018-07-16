using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Core.Vehicle
{
    [Table("Vehicles")]
    public class VehicleModel : IAggregateRoot, IEntity
    {
        public VehicleModel(int userId, string plateNumber) : this(0, userId, plateNumber)
        {
        }

        public VehicleModel(int id, int userId, string plateNumber)
        {
            Id = id;
            UserId = userId;
            PlateNumber = plateNumber;
        }

        object IAggregateRoot.Id => Id;

        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string PlateNumber { get; set; }
    }
}
