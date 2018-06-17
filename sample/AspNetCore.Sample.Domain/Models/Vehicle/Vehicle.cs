using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Domain.Models
{
    public class Vehicle : DefaultAggregateRoot
    {
        public Vehicle(IEventBus eventBus) : base(eventBus)
        {
        }

        public int UserId { get; set; }

        public string PlateNumber { get; set; }
    }
}
