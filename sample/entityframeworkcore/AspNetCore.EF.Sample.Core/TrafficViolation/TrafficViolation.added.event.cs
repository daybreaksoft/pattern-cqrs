using Daybreaksoft.Pattern.CQRS.Event;

namespace AspNetCore.EF.Sample.Core.TrafficViolation
{
    public class TrafficViolationAddedEvent : IEvent
    {
        public int VehicleId { get; set; }

        public int DeductPoint { get; set; }
    }
}
