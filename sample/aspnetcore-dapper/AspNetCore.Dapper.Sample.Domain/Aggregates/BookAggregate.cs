using System.Collections.Generic;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Domain.Aggregates
{
    public class BookAggregate : DefaultAggregateRoot
    {
        public BookAggregate(IEventBus eventBus) : base(eventBus)
        {
        }

        public string Name { get; set; }

        public int BookTypeId { get; set; }

        public IReadOnlyCollection<int> AuthorIds { get; set; }
    }
}