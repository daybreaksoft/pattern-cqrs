using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.EF.Sample.Core.TrafficViolation
{
    public class TrafficViolationAddedEvent : IEvent
    {
        public int VehicleId { get; set; }

        public int DeductPoint { get; set; }
    }
}
