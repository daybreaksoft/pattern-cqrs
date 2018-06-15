using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS
{
    public interface IEventBus
    {
        Task PublishAsync<TEvent>(TEvent evnt) where TEvent : IEvent;
    }
}
