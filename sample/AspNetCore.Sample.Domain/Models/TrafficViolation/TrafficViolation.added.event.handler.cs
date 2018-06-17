using Daybreaksoft.Pattern.CQRS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Sample.Domain.Models
{
    public class TrafficViolationAddedEventHandler : IEventHandler<TrafficViolationAddedEvent>
    {
        protected readonly IAggregateBus AggregateBus;

        public TrafficViolationAddedEventHandler(IAggregateBus aggregateBus)
        {
            AggregateBus = aggregateBus;
        }

        public async Task HandleAsync(TrafficViolationAddedEvent evnt)
        {
            var vehicleModel = await AggregateBus.GetExsitsAggregate<Vehicle>(evnt.VehicleId);

            var userModel = await AggregateBus.GetExsitsAggregate<User>(vehicleModel.UserId);

            userModel.DeductPoint(evnt.DeductPoint);
        }
    }
}
