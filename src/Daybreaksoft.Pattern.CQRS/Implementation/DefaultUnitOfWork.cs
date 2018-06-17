using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS
{
    public class DefaultUnitOfWork : IUnitOfWork
    {
        protected readonly IAggregateBuilder AggregateBuilder;
        protected readonly IDynamicRepositoryFactory DynamicRepositoryFactory;
        protected readonly IEventBus EventBus;

        public DefaultUnitOfWork(IAggregateBuilder aggregateBuilder, IDynamicRepositoryFactory dynamicRepositoryFactory, IEventBus eventBus)
        {
            AggregateBuilder = aggregateBuilder;
            DynamicRepositoryFactory = dynamicRepositoryFactory;
            EventBus = eventBus;
        }

        public List<IAggregateRoot> UncommittedAggregate { get; protected set; } = new List<IAggregateRoot>();

        public virtual async Task OpenAsync()
        {
            //throw new NotImplementedException();
        }

        public virtual async Task CommitAsync()
        {
            await StoreAggreateAsync();
        }

        #region Store Aggreate

        protected virtual async Task StoreAggreateAsync()
        {
            foreach (var aggregate in UncommittedAggregate)
            {
                await ExecutStoreAsync(aggregate);
            }
        }

        protected virtual async Task ExecutStoreAsync(IAggregateRoot aggregate)
        {
            if (aggregate.State == AggregateState.Added)
            {
                await InsertAggreateAsync(aggregate);
            }
            else if (aggregate.State == AggregateState.Modified)
            {
                await UpdateAggreateAsync(aggregate);
            }
            else if (aggregate.State == AggregateState.Deleted)
            {
                await RemoveAggreateAsync(aggregate);
            }

            await PublishEventsAsync(aggregate);
        }

        protected virtual async Task InsertAggreateAsync(IAggregateRoot aggregate)
        {
            await DynamicRepositoryFactory.InvokeInsertAsync(aggregate);
        }

        protected virtual async Task UpdateAggreateAsync(IAggregateRoot aggregate)
        {
            await DynamicRepositoryFactory.InvokeUpdateAsync(aggregate);
        }

        protected virtual async Task RemoveAggreateAsync(IAggregateRoot aggregate)
        {
            await DynamicRepositoryFactory.InvokeRemoveAsync(aggregate.GetType(), aggregate.Id);
        }

        #endregion

        #region Publish Events

        protected virtual async Task PublishEventsAsync(IAggregateRoot aggregate)
        {
            if (aggregate is IEventSource)
            {
                var aggregateEvents = ((IEventSource)aggregate).Events;

                if (aggregateEvents.Any())
                {
                    foreach (var evnt in aggregateEvents)
                    {
                        await EventBus.PublishAsync(evnt);
                    }
                }
            }
        }

        #endregion

        public virtual TAggregateRoot BuildAggregate<TAggregateRoot>(bool addToUncommitted = true) where TAggregateRoot : IAggregateRoot, new()
        {
            var aggregate = AggregateBuilder.BuildAggregate<TAggregateRoot>();

            if (addToUncommitted) UncommittedAggregate.Add(aggregate);

            return aggregate;
        }

        public virtual async Task<TAggregateRoot> GetAggregate<TAggregateRoot>(object id, bool addToUncommitted = true) where TAggregateRoot : IAggregateRoot, new()
        {
            var existsAggregate = UncommittedAggregate.SingleOrDefault(p => p.Id == id);

            if (existsAggregate == null)
            {
                var aggregate = await AggregateBuilder.GetAggregate<TAggregateRoot>(id);

                if (addToUncommitted) UncommittedAggregate.Add(aggregate);

                return aggregate;
            }
            else {
                return (TAggregateRoot)existsAggregate;
            }
        }
    }
}
