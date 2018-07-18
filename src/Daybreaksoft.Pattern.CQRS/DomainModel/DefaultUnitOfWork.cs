using System.Collections.Generic;
using System.Threading.Tasks;
using Daybreaksoft.Pattern.CQRS.Definition;
using Daybreaksoft.Pattern.CQRS.Event;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public class DefaultUnitOfWork : IUnitOfWork
    {
        protected readonly IRepositoryFactory RepositoryFactory;
        protected readonly IRepositoryInvoker RepositoryInvoker;
        protected readonly IEventBus EventBus;
        protected readonly List<AggregateOperator> AggregateOperators;

        public DefaultUnitOfWork(IRepositoryFactory repositoryFactory, IRepositoryInvoker repositoryInvoker, IEventBus eventBus)
        {
            RepositoryFactory = repositoryFactory;
            RepositoryInvoker = repositoryInvoker;
            EventBus = eventBus;
            AggregateOperators = new List<AggregateOperator>();
        }

        public virtual async Task BeginAsync()
        {
#if !Net451
            await Task.CompletedTask;
#else
            await Task.FromResult(0);
#endif
        }

        public virtual async Task CommitAsync()
        {
            foreach (var aggregateOperator in AggregateOperators)
            {
                if (aggregateOperator.Action == AggregateAction.Add)
                {
                    await AddToStorageAsync(aggregateOperator.Aggregate);
                }
                else if (aggregateOperator.Action == AggregateAction.Delete)
                {
                    await RemoveFromStorageAsync(aggregateOperator.Aggregate);
                }
            }
        }

        public void ReadyToAdd(IAggregateRoot aggregate)
        {
            AggregateOperators.Add(new AggregateOperator(aggregate, AggregateAction.Add));
        }

        public void ReadyToRemove(IAggregateRoot aggregate)
        {
            AggregateOperators.Add(new AggregateOperator(aggregate, AggregateAction.Delete));
        }

        #region Store Aggreate

        protected virtual async Task AddToStorageAsync(IAggregateRoot aggregate)
        {
            IEntity entity = null;
            if (aggregate is IEntity)
            {
                entity = (IEntity)aggregate;
            }

            var repositoryType = RepositoryFactory.GetRepositoryType(entity);
            var repository = RepositoryFactory.GetRepository(repositoryType);
            await RepositoryInvoker.InsertAsync(repository, repositoryType, entity);
        }

        protected virtual async Task RemoveFromStorageAsync(IAggregateRoot aggregate)
        {
            IEntity entity = null;
            if (aggregate is IEntity)
            {
                entity = (IEntity)aggregate;
            }

            var repositoryType = RepositoryFactory.GetRepositoryType(entity);
            var repository = RepositoryFactory.GetRepository(repositoryType);
            await RepositoryInvoker.RemoveAsync(repository, repositoryType, aggregate.Id);
        }

        #endregion

        public virtual void Dispose()
        {
        }
    }
}
