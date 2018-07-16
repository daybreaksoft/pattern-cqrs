namespace AspNetCore.EF.Sample.Core.TrafficViolation
{
    //public class TrafficViolationAddedEventHandler : IEventHandler<TrafficViolationAddedEvent>
    //{
    //    protected readonly IAggregateBus AggregateBus;

    //    public TrafficViolationAddedEventHandler(IAggregateBus aggregateBus)
    //    {
    //        AggregateBus = aggregateBus;
    //    }

    //    public async Task HandleAsync(TrafficViolationAddedEvent evnt)
    //    {
    //        var vehicleModel = await AggregateBus.GetExsitsAggregate<Vehicle>(evnt.VehicleId);

    //        var vehicle1 = await AggregateBus.GetExsitsAggregate<Vehicle>(evnt.VehicleId);

    //        var userModel = await AggregateBus.GetExsitsAggregate<User>(vehicleModel.UserId);

    //        userModel.DeductPoint(evnt.DeductPoint);
    //    }
    //}
}
