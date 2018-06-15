using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.Implementation
{
    /// <summary>
    /// Defualt implemention of ICommandBus
    /// </summary>
    public class DefaultEventBus : IEventBus
    {
        protected readonly IDependencyInjection DI;
        protected readonly IUnitOfWork UnitOfWork;
        
        public DefaultEventBus(IDependencyInjection di, IUnitOfWork unitOfWork)
        {
            DI = di;
            UnitOfWork = unitOfWork;
        }

        public Task PublishAsync<TEvent>(TEvent evnt) where TEvent : IEvent
        {
            throw new System.NotImplementedException();
        }
    }
}
