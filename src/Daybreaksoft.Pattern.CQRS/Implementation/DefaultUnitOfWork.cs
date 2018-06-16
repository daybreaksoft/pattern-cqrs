using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS
{
    public class DefaultUnitOfWork : IUnitOfWork
    {
        protected readonly IAggregateBuilder AggregateBuilder;
        protected readonly IDynamicRepositoryFactory DynamicRepositoryFactory;

        public DefaultUnitOfWork(IAggregateBuilder aggregateBuilder, IDynamicRepositoryFactory dynamicRepositoryFactory)
        {
            AggregateBuilder = aggregateBuilder;
            DynamicRepositoryFactory = dynamicRepositoryFactory;
        }

        public List<IAggregateRoot> UnCommittedAggregate { get; protected set; } = new List<IAggregateRoot>();

        public virtual async Task OpenAsync()
        {
            //throw new NotImplementedException();
        }

        public virtual async Task CommitAsync()
        {
            await StoreAggreateAsync();

            await PublishEventsAsync();
        }

        #region Store Aggreate

        protected virtual async Task StoreAggreateAsync()
        {
            foreach (var aggreate in UnCommittedAggregate)
            {
                if (aggreate.State == AggregateState.Added)
                {
                    await InsertAggreateAsync(aggreate);
                }
                else if (aggreate.State == AggregateState.Modified)
                {
                    await UpdateAggreateAsync(aggreate);
                }
                else if (aggreate.State == AggregateState.Deleted)
                {
                    await RemoveAggreateAsync(aggreate);
                }
            }
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

        protected virtual async Task PublishEventsAsync()
        {
            foreach (var aggreate in UnCommittedAggregate.Where(p => p.State != AggregateState.Unchanged))
            {
                if (aggreate is IEventSource)
                {
                    var aggreateEvents = ((IEventSource)aggreate).Events;

                    if (aggreateEvents.Any())
                    {

                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        #endregion

        public virtual TAggregateRoot BuildAggregate<TAggregateRoot>(bool addToUnCommitted = true) where TAggregateRoot : IAggregateRoot, new()
        {
            var aggregate = AggregateBuilder.BuildAggregate<TAggregateRoot>();

            if (addToUnCommitted) UnCommittedAggregate.Add(aggregate);

            return aggregate;
        }

        public virtual async Task<TAggregateRoot> GetAggregate<TAggregateRoot>(object id, bool addToUnCommitted = true) where TAggregateRoot : IAggregateRoot, new()
        {
            var aggregate = await AggregateBuilder.GetAggregate<TAggregateRoot>(id);

            if (addToUnCommitted) UnCommittedAggregate.Add(aggregate);

            return aggregate;
        }
    }
}
