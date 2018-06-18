using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS
{
    public interface IEventBus
    {
        EventStream Events { get; }

        Task PublishAsync(IEvent evnt);
    }
}
