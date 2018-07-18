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
      

        protected Definition.AggregateAction _state;
        public virtual AggregateAction State => _state;

        protected virtual async Task PublishEventAsync(IEvent evnt)
        {
            await EventBus.PublishAsync(evnt);
        }

        public void Verify()
        {
            throw new System.NotImplementedException();
        }
    }
}
