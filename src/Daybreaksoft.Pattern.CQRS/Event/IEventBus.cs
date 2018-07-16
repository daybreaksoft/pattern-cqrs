using System.Threading.Tasks;
using Daybreaksoft.Pattern.CQRS.Definition;

namespace Daybreaksoft.Pattern.CQRS.Event
{
    public interface IEventBus
    {
        EventStream Events { get; }

        Task PublishAsync(IEvent evnt);
    }
}
