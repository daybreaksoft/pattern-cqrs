using System;
using AspNetCore.EF.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Core.Vehicle
{
    public class VehicleModel : VehicleEntity, IAggregateRoot, IAggregateRootSetKey
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

        #region IAggregateRoot Implementation

        object IAggregateRoot.Id => Id;

        void IAggregateRootSetKey.SetKey(object id)
        {
            Id = Convert.ToInt32(id);
        }

        #endregion
    }
}
