using Daybreaksoft.Pattern.CQRS;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace AspNetCore.Sample.Domain.Models
{
    public class TrafficViolation : DefaultAggregateRoot
    {
        public TrafficViolation(IEventBus eventBus) : base(eventBus)
        {
        }

        public int VehicleId { get; set; }

        public int DeductPoint { get; set; }

        public override async Task AddAsync()
        {
            await base.AddAsync();

            // Append traffice violation added event
            await PublishEventAsync(new TrafficViolationAddedEvent
            {
                VehicleId = this.VehicleId,
                DeductPoint = this.DeductPoint
            });
        }
    }
}
