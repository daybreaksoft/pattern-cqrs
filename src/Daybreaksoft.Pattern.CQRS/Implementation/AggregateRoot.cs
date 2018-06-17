namespace Daybreaksoft.Pattern.CQRS
{
    /// <summary>
    /// Abstract AggregateRoot
    /// </summary>
    public abstract class AggregateRoot : IAggregateRoot, IEventSource
    {
        protected readonly IEventBus EventBus;

        public AggregateRoot(IEventBus eventBus)
        {
            EventBus = eventBus;
        }

        public object Id { get; set; }

        protected AggregateState _state;
        public virtual AggregateState State => _state;

        #region Events

        protected EventStream _events = new EventStream();
        EventStream IEventSource.Events => _events;

        protected virtual void AppendEvent(IEvent evnt)
        {
            _events.Append(evnt);
        }

        #endregion
    }
}
