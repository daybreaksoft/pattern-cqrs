using System.Threading.Tasks;

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
            _state = AggregateState.Added;

#if !Net451
            await Task.CompletedTask;
#else
            await Task.FromResult(0);
#endif
        }


        public virtual async Task ModifyAsync()
        {
            _state = AggregateState.Modified;

#if !Net451
            await Task.CompletedTask;
#else
            await Task.FromResult(0);
#endif
        }

        public virtual async Task RemoveAsync()
        {
            _state = AggregateState.Deleted;

#if !Net451
            await Task.CompletedTask;
#else
            await Task.FromResult(0);
#endif
        }
    }
}
