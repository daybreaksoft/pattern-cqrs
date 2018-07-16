using AspNetCore.EF.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Core.Vehicle
{
    public class VehicleModel : VehicleEntity, IAggregateRoot
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
    }
}
