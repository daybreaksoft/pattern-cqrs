using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS
{
    public class DefaultUnitOfWork : IUnitOfWork
    {
        protected readonly IAggregateBus AggregateBus;
        protected readonly IDynamicRepositoryFactory DynamicRepositoryFactory;
        protected readonly IEventBus EventBus;

        public DefaultUnitOfWork(IAggregateBus aggregateBus, IDynamicRepositoryFactory dynamicRepositoryFactory, IEventBus eventBus)
        {
            AggregateBus = aggregateBus;
            DynamicRepositoryFactory = dynamicRepositoryFactory;
            EventBus = eventBus;
        }

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
            foreach (var aggregate in AggregateBus.Aggregates)
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
    }
}
