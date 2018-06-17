using Daybreaksoft.Pattern.CQRS;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCore.Sample.Domain.Models
{
    public partial class TrafficViolation : DefaultAggregateRoot
    {
        [NotMapped]
        public override object Id => TrafficViolationId;

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
