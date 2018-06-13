using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS
{
    public interface ICommandExecutor<in TCommand>
        where TCommand : ICommand
    {
        Task ExecuteAsync(TCommand command);
    }
}
