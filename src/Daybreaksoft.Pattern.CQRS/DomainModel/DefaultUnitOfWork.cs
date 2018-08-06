using System.Collections.Generic;
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
            RegisterdModels = new List<RegisterdModel>();
        }

        public ICollection<RegisterdModel> RegisterdModels { get; }

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
         
        }

        public virtual void Dispose()
        {
        }
    }
}
