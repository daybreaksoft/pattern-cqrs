using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Daybreaksoft.Pattern.CQRS.Extensions.AspNetCore
{
    /// <summary>
    /// CQRS options builder
    /// </summary>
    public class CQRSOptionBuilder
    {
        /// <summary>
        /// Action that add repository implemented class
        /// </summary>
        public Action<IServiceCollection> AddRepositoryAction { get; protected set; }

        /// <summary>
        /// Action that add dependency injection implemented class
        /// </summary>
        public Action<IServiceCollection> AddDependencyInjectionAction { get; protected set; }

        /// <summary>
        /// Action that add command bus implemented class
        /// </summary>
        public Action<IServiceCollection> AddCommandBusAction { get; protected set; }

        /// <summary>
        /// Action that add command executor implemented class
        /// </summary>
        public Action<IServiceCollection> AddCommandExecutorAction { get; protected set; }

        /// <summary>
        /// The assembly where the implemented type of command executor belong to
        /// </summary>
        public Assembly CommandExecutorAssembly { get; protected set; }

        /// <summary>
        /// Set custom DI action for repository
        /// </summary>
        public void ForRepository(Action<IServiceCollection> action)
        {
            AddRepositoryAction = action;
        }

        /// <summary>
        /// Set custom DI action for dependency injection
        /// </summary>
        public void ForDependencyInjection(Action<IServiceCollection> action)
        {
            AddDependencyInjectionAction = action;
        }

        /// <summary>
        /// Set custom DI action for command bus
        /// </summary>
        public void ForCommandBus(Action<IServiceCollection> action)
        {
            AddCommandBusAction = action;
        }

        /// <summary>
        /// Set custom DI action for command executor
        /// </summary>
        public void ForCommandExecutor(Action<IServiceCollection> action)
        {
            AddCommandExecutorAction = action;

            CommandExecutorAssembly = null;
        }

        /// <summary>
        /// Set the assemble where the implemented type of command executor is belong to
        /// </summary>
        public void ForCommandExecutor(Assembly  assembly)
        {
            CommandExecutorAssembly = assembly;

            AddCommandExecutorAction = null;
        }
    }
}
