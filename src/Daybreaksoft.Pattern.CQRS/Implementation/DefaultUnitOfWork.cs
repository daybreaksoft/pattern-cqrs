using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS.Interface.Domain;

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
        }

        #region Store Aggreate

        protected virtual async Task StoreAggreateAsync()
        {
            foreach (var aggreate in UnCommittedAggregate)
            {
                if (HasKeyValue(aggreate.Id))
                {
                    if (!aggreate.Deleted)
                    {
                        await UpdateAggreateAsync(aggreate);
                    }
                    else
                    {
                        await RemoveAggreateAsync(aggreate);
                    }
                }
                else
                {
                    await InsertAggreateAsync(aggreate);
                }
            }
        }

        protected virtual bool HasKeyValue(object key)
        {
            if (key is string)
            {
                return !string.IsNullOrEmpty(key.ToString());
            }
            else if (key is int)
            {

                if (int.TryParse(key.ToString(), out int result))
                {
                    return result > 0;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        protected virtual Type GetRepositoryType(IAggregateRoot aggregate)
        {
            return GetRepositoryType(aggregate.GetType());
        }

        protected virtual Type GetRepositoryType(Type type)
        {
            return typeof(IRepository<>).MakeGenericType(type);
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
