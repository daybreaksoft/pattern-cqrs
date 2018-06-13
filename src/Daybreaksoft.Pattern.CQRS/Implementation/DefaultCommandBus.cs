using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.Implementation
{
    public class DefaultCommandBus : ICommandBus
    {
        protected readonly IDependencyInjection DI;
        
        public DefaultCommandBus(IDependencyInjection di)
        {
            DI = di;
        }

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var executor = DI.GetService<ICommandExecutor<TCommand>>();

            await executor.ExecuteAsync(command);
        }
    }
}
