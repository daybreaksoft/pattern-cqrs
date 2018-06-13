using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.Implementation
{
    /// <summary>
    /// Defualt implemention of ICommandBus
    /// </summary>
    public class DefaultCommandBus : ICommandBus
    {
        protected readonly IDependencyInjection DI;
        
        public DefaultCommandBus(IDependencyInjection di)
        {
            DI = di;
        }

        /// <summary>
        /// Call command exectuor that find it via DI with ICommand
        /// </summary>
        public async Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var executor = DI.GetService<ICommandExecutor<TCommand>>();

            await executor.ExecuteAsync(command);
        }
    }
}
