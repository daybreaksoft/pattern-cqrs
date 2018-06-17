using Daybreaksoft.Pattern.CQRS;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCore.Sample.Domain.Models
{
    public class TrafficViolation : DefaultAggregateRoot
    {
        public int VehicleId { get; set; }

        public int DeductPoint { get; set; }

        public override void Add()
        {
            base.Add();

            // Append traffice violation added event
            AppendEvent(new TrafficViolationAddedEvent
            {
                VehicleId = this.VehicleId,
                DeductPoint = this.DeductPoint
            });
        }
    }
}
