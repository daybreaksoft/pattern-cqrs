using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Domain.Models
{
    public class Vehicle : DefaultAggregateRoot
    {
        public int UserId { get; set; }

        public string PlateNumber { get; set; }
    }
}
