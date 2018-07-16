using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.Event
{
    public interface IEventHandler<in TEvent>
        where TEvent : IEvent
    {
        Task HandleAsync(TEvent evnt);
    }
}
