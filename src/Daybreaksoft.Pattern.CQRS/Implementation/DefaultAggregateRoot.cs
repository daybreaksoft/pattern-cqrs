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

#if NetStandard20
            await Task.CompletedTask;
#else
            await Task.FromResult(0);
#endif

        }


        public virtual async Task Modify()
        {
            _state = AggregateState.Modified;

#if NetStandard20
            await Task.CompletedTask;
#else
            await Task.FromResult(0);
#endif
        }

        public virtual async Task Remove()
        {
            _state = AggregateState.Deleted;

#if NetStandard20
            await Task.CompletedTask;
#else
            await Task.FromResult(0);
#endif
        }
    }
}
