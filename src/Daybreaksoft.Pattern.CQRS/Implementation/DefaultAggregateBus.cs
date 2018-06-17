using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS
{
    /// <summary>
    /// Defualt implemention of IAggregateBuilder
    /// </summary>
    public class DefaultAggregateBus : IAggregateBus
    {
        protected readonly IDependencyInjection DI;
        protected readonly IDynamicRepositoryFactory DynamicRepositoryFactory;

        public DefaultAggregateBus(IDependencyInjection di, IDynamicRepositoryFactory dynamicRepositoryFactory)
        {
            DI = di;
            DynamicRepositoryFactory = dynamicRepositoryFactory;
        }

        public AggregateCollection Aggregates { get; protected set; } = new AggregateCollection();

        public TAggregateRoot BuildAggregate<TAggregateRoot>() where TAggregateRoot : IAggregateRoot
        {
            var aggregate = DI.GetService<TAggregateRoot>();

            Aggregates.Add(aggregate);

            return aggregate;
        }

        public TAggregateRoot BuildAggregate<TAggregateRoot>(object id) where TAggregateRoot : IAggregateRoot
        {
            var aggregate = DI.GetService<TAggregateRoot>();
            aggregate.Id = id;

            Aggregates.Add(aggregate);

            return aggregate;
        }

        public async Task<TAggregateRoot> GetExsitsAggregate<TAggregateRoot>(object id) where TAggregateRoot : IAggregateRoot
        {
            var existsAggregate = Aggregates.SingleOrDefault(p => p.Id == id);

            if (existsAggregate == null)
            {
                var aggregate = await DynamicRepositoryFactory.InvokeFindAsync<TAggregateRoot>(id);

                Aggregates.Add(aggregate);

                return aggregate;
            }
            else
            {
                return (TAggregateRoot)existsAggregate;
            }
        }
    }
}
