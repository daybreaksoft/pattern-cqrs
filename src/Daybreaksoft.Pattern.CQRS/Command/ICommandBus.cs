using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS.Command
{
    /// <summary>
    /// CommandBus接口
    /// 实现这个接口的类将统一调用CommandExecutor来执行Command
    /// </summary>
    public interface ICommandBus
    {
        /// <summary>
        /// 根据Command的类型获取对应的CommandExecutor并执行Command
        /// </summary>
        /// <typeparam name="TCommand">Command类型，必须继承Daybreaksoft.Pattern.CQRS.ICommand</typeparam>
        /// <param name="cmd">Command实例</param>
        /// <returns></returns>
        Task SendAsync<TCommand>(TCommand cmd) where TCommand : ICommand;
    }
}
