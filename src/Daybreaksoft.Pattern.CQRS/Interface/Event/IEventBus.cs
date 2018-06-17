using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS
{
    public interface IEventBus
    {
        Task PublishAsync(IEvent evnt);
    }
}
