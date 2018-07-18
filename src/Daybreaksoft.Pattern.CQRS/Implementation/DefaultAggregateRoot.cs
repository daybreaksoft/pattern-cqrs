using System.Threading.Tasks;
using Daybreaksoft.Pattern.CQRS.Definition;
using Daybreaksoft.Pattern.CQRS.Event;

namespace Daybreaksoft.Pattern.CQRS
{
    /// <summary>
    /// Abstract AggregateRoot
    /// </summary>
    public abstract class DefaultAggregateRoot : AggregateRoot
    {
        public DefaultAggregateRoot(IEventBus eventBus):base(eventBus)
        {
        }

        public virtual async Task AddAsync()
        {
            //_state = AggregateAction.Added;

#if !Net451
            await Task.CompletedTask;
#else
            await Task.FromResult(0);
#endif
        }


        public virtual async Task ModifyAsync()
        {
            //_state = AggregateAction.Modified;

#if !Net451
            await Task.CompletedTask;
#else
            await Task.FromResult(0);
#endif
        }

        public virtual async Task RemoveAsync()
        {
            //_state = AggregateAction.Deleted;

#if !Net451
            await Task.CompletedTask;
#else
            await Task.FromResult(0);
#endif
        }
    }
}
