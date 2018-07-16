using System.Collections.Generic;
using System.Threading.Tasks;
using Daybreaksoft.Pattern.CQRS.Definition;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace Daybreaksoft.Pattern.CQRS
{
    public interface IAggregateBus
    {
        AggregateCollection Aggregates { get; }

        TAggregateRoot BuildAggregate<TAggregateRoot>() where TAggregateRoot : IAggregateRoot;

        TAggregateRoot BuildAggregate<TAggregateRoot>(object id) where TAggregateRoot : IAggregateRoot;

        Task<TAggregateRoot> GetExsitsAggregate<TAggregateRoot>(object id) where TAggregateRoot : IAggregateRoot;
    }
}
