using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS
{
    /// <summary>
    /// Abstract AggregateRoot
    /// </summary>
    public abstract class AggregateRoot : IAggregateRoot
    {
        protected readonly IEventBus EventBus;

        public AggregateRoot(IEventBus eventBus)
        {
            EventBus = eventBus;
        }

        public object Id { get; set; }

        protected AggregateState _state;
        public virtual AggregateState State => _state;

        protected async virtual Task PublishEventAsync(IEvent evnt)
        {
            await EventBus.PublishAsync(evnt);
        }
    }
}
