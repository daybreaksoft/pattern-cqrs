using System.Threading.Tasks;
using Daybreaksoft.Pattern.CQRS.Event;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public class DefaultUnitOfWork : IUnitOfWork
    {
        protected readonly IEventBus EventBus;

        public DefaultUnitOfWork(IEventBus eventBus)
        {
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

        public virtual void Dispose()
        {
        }
    }
}
