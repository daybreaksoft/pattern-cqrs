using System.Collections.Generic;
using System.Threading.Tasks;
using Daybreaksoft.Pattern.CQRS.Definition;
using Daybreaksoft.Pattern.CQRS.Event;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public class DefaultUnitOfWork : IUnitOfWork
    {
        protected readonly IDomainServiceFactory DomainServiceFactory;
        protected readonly IEventBus EventBus;

        public DefaultUnitOfWork(IDomainServiceFactory domainServiceFactory, IEventBus eventBus)
        {
            DomainServiceFactory = domainServiceFactory;
            EventBus = eventBus;
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
#if !Net451
            await Task.CompletedTask;
#else
            await Task.FromResult(0);
#endif
        }

        public IDomainService<TAggregate> DomainService<TAggregate>() where TAggregate : IAggregateRoot
        {
            return DomainServiceFactory.GetDomainService<TAggregate>();
        }

        #region Store Aggreate

        public async Task AddToStorageAsync<TAggregate>(TAggregate aggregate) where TAggregate : IAggregateRoot
        {
            if (aggregate is IAggregateRootVerification verificationAggregate)
            {
                verificationAggregate.Verify();
            }

            await DomainService<TAggregate>().InsertAsync(aggregate);
        }

        public async Task ModifyWithinStorageAsync<TAggregate>(TAggregate aggregate) where TAggregate : IAggregateRoot
        {
            if (aggregate is IAggregateRootVerification verificationAggregate)
            {
                verificationAggregate.Verify();
            }

            await DomainService<TAggregate>().UpdateAsync(aggregate);
        }

        public async Task RemoveFromStorageAsync<TAggregate>(object id) where TAggregate : IAggregateRoot
        {
            await DomainService<TAggregate>().DeleteAsync(id);
        }

        #endregion

        public virtual void Dispose()
        {
        }
    }
}
