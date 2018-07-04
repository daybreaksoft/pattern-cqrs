using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Domain.Aggregates
{
    public class BookTypeAggregate : DefaultAggregateRoot
    {
        public BookTypeAggregate(IEventBus eventBus) : base(eventBus)
        {
        }

        public string Type { get; set; }
    }
}
