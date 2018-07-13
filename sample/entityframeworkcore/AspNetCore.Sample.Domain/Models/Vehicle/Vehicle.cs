using AspNetCore.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.Sample.Domain.Models
{
    public class Vehicle : VehicleEntity, IAggregateRoot
    {
        public Vehicle(int userId, string plateNumber)
        {
            UserId = userId;
            PlateNumber = plateNumber;
        }

        public Vehicle(int id, int userId, string plateNumber)
        {
            Id = id;
            UserId = userId;
            PlateNumber = plateNumber;
        }

        object IAggregateRoot.Id => Id;
    }
}
