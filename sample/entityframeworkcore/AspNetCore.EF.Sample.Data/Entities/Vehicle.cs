using System.ComponentModel.DataAnnotations;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Data.Entities
{
    public class VehicleEntity : IAggregateRoot, IEntity
    {
        public VehicleEntity(int userId, string plateNumber) : this(0, userId, plateNumber)
        {
        }

        public VehicleEntity(int id, int userId, string plateNumber)
        {
            Id = id;
            UserId = userId;
            PlateNumber = plateNumber;
        }

        object IAggregateRoot.Id => Id;

        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string PlateNumber { get; set; }

    }
}
