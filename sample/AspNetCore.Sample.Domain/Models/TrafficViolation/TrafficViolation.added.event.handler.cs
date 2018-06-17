using Daybreaksoft.Pattern.CQRS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Sample.Domain.Models
{
    public class TrafficViolationAddedEventHandler : IEventHandler<TrafficViolationAddedEvent>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public TrafficViolationAddedEventHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task HandleAsync(TrafficViolationAddedEvent evnt)
        {
            var vehicleModel = await UnitOfWork.GetAggregate<Vehicle>(evnt.VehicleId, false);

            var userModel = await UnitOfWork.GetAggregate<User>(vehicleModel.UserId);

            userModel.DeductPoint(evnt.DeductPoint);
        }
    }
}
