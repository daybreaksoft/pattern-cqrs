using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS
{
    public interface ICommandBus
    {
        Task SendAsync<TCommand>(TCommand cmd) where TCommand : ICommand;
    }
}
