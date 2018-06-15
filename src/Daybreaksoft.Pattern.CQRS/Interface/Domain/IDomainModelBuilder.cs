using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS
{
    public interface IAggregateBuilder
    {
        TAggregateRoot BuildAggregate<TAggregateRoot>() where TAggregateRoot : IAggregateRoot, new();

        Task<TAggregateRoot> GetAggregate<TAggregateRoot>(object id) where TAggregateRoot : IAggregateRoot;
    }
}
