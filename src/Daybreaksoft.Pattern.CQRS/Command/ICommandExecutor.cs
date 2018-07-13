using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.Command
{
    /// <summary>
    /// CommandExecutor接口
    /// 一个Command只能有一个Executor
    /// </summary>
    /// <typeparam name="TCommand">Command类型，必须继承Daybreaksoft.Pattern.CQRS.ICommand</typeparam>
    public interface ICommandExecutor<in TCommand>
        where TCommand : ICommand
    {
        /// <summary>
        /// 执行Command
        /// </summary>
        /// <param name="command">Command实例</param>
        /// <returns></returns>
        Task ExecuteAsync(TCommand command);
    }
}
