using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Daybreaksoft.Pattern.CQRS
{
    /// <summary>
    /// Abstract AggregateRoot
    /// </summary>
    public abstract class AggregateRoot : IAggregateRoot, IEventSource
    {
        [NotMapped]
        [JsonIgnore]
        public abstract object Id { get; }

        protected AggregateState _state;

        [NotMapped]
        [JsonIgnore]
        public virtual AggregateState State => _state;

        #region Events

        protected EventStream _events = new EventStream();

        [NotMapped]
        [JsonIgnore]
        EventStream IEventSource.Events => _events;

        protected virtual void AppendEvent(IEvent evnt)
        {
            _events.Append(evnt);
        }

        #endregion
    }
}
