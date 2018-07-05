using AspNetCore.Dapper.Sample.Data.Const;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Domain.Aggregates
{
    public class AuthorAggregate : DefaultAggregateRoot
    {
        public AuthorAggregate(IEventBus eventBus) : base(eventBus)
        {
        }

        public string Name { get; set; }

        public SexConst Sex { get; set; }
    }
}
