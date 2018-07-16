using System.Threading.Tasks;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.EF.Sample.Core.TrafficViolation
{
    public class TrafficViolationModel : DefaultAggregateRoot
    {
        public TrafficViolationModel(IEventBus eventBus) : base(eventBus)
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
