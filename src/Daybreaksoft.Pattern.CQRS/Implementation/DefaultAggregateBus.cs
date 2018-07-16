using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Daybreaksoft.Pattern.CQRS.Definition;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace Daybreaksoft.Pattern.CQRS
{
    /// <summary>
    /// Defualt implemention of IAggregateBuilder
    /// </summary>
    public class DefaultAggregateBus : IAggregateBus
    {
        protected readonly IDependencyInjection DI;
        protected readonly IRepositoryFactory DynamicRepositoryFactory;

        public DefaultAggregateBus(IDependencyInjection di, IRepositoryFactory dynamicRepositoryFactory)
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
            //aggregate.Id = id;

            Aggregates.Add(aggregate);

            return aggregate;
        }

        public async Task<TAggregateRoot> GetExsitsAggregate<TAggregateRoot>(object id) where TAggregateRoot : IAggregateRoot
        {
          throw new NotSupportedException();
        }
    }
}
