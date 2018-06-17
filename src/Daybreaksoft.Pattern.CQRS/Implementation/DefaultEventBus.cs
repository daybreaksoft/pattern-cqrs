using Daybreaksoft.Extensions.Functions;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.Implementation
{
    /// <summary>
    /// Defualt implemention of ICommandBus
    /// </summary>
    public class DefaultEventBus : IEventBus
    {
        protected readonly IDependencyInjection DI;
        
        public DefaultEventBus(IDependencyInjection di)
        {
            DI = di;
        }

        public async Task PublishAsync(IEvent evnt)
        {
            var handlerType = typeof(IEventHandler<>);
            handlerType = handlerType.MakeGenericType(evnt.GetType());

            var handler = DI.GetService(handlerType);

            await (Task)handlerType.InvokeMethod("HandleAsync", handler, evnt);
        }
    }
}
