using System.Threading.Tasks;
using Daybreaksoft.Pattern.CQRS.Definition;
using Daybreaksoft.Pattern.CQRS.DomainModel;
using Daybreaksoft.Pattern.CQRS.Event;

namespace Daybreaksoft.Pattern.CQRS
{
    /// <summary>
    /// Abstract AggregateRoot
    /// </summary>
    public abstract class AggregateRoot : IAggregateRoot
    {
        protected readonly IEventBus EventBus;

        protected AggregateRoot(IEventBus eventBus)
        {
            EventBus = eventBus;
        }

        public object Id { get; set; }

        protected AggregateState _state;
        public virtual AggregateState State => _state;

        protected virtual async Task PublishEventAsync(IEvent evnt)
        {
            await EventBus.PublishAsync(evnt);
        }
    }
}
