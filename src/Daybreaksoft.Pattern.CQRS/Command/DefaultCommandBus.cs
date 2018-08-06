using Daybreaksoft.Pattern.CQRS.DomainModel;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.Command
{
    /// <summary>
    /// Defualt implemention of ICommandBus
    /// </summary>
    public class DefaultCommandBus : ICommandBus
    {
        protected readonly IDependencyInjection DI;
        protected readonly IUnitOfWork UnitOfWork;
        
        public DefaultCommandBus(IDependencyInjection di, IUnitOfWork unitOfWork)
        {
            DI = di;
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Call command exectuor that find it via DI with ICommand
        /// </summary>
        public virtual async Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            // Get command executor via DI.
            var executor = DI.GetService<ICommandExecutor<TCommand>>();

            // Start to execute command.
            await UnitOfWork.BeginAsync();

            await executor.ExecuteAsync(command);

            await UnitOfWork.CommitAsync();
        }
    }
}
