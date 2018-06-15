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
        #region Properties

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
        /// Action that add implementation class of IEventBus
        /// </summary>
        public Action<IServiceCollection> AddEventBusAction { get; protected set; }

        /// <summary>
        /// Action that add aggregate builder implemented class
        /// </summary>
        public Action<IServiceCollection> AddAggregateBuilderAction { get; protected set; }

        /// <summary>
        /// Action that add IDynamicRepositoryFactory implemented class
        /// </summary>
        public Action<IServiceCollection> AddDynamicRepositoryFactoryAction { get; protected set; }

        /// <summary>
        /// Action that add query implemented class
        /// </summary>
        public Action<IServiceCollection> AddQueryAction { get; protected set; }

        /// <summary>
        /// The assembly where the implemented type of query belong to
        /// </summary>
        public Assembly QueryAssembly { get; protected set; }

        /// <summary>
        /// Action that add unit of work implemented class
        /// </summary>
        public Action<IServiceCollection> AddUnitOfWorkAction { get; protected set; }

        #endregion

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

        /// <summary>
        /// Set custom DI action for IEventBus
        /// </summary>
        public void ForEventBus(Action<IServiceCollection> action)
        {
            AddEventBusAction = action;
        }

        /// <summary>
        /// Set custom DI action for aggregate builder
        /// </summary>
        public void ForAggregateBuilder(Action<IServiceCollection> action)
        {
            AddAggregateBuilderAction = action;
        }

        /// <summary>
        /// Set custom DI action for dynamic repository factory
        /// </summary>
        public void ForDynamicRepositoryFactory(Action<IServiceCollection> action)
        {
            AddDynamicRepositoryFactoryAction = action;
        }

        /// <summary>
        /// Set custom DI action for query
        /// </summary>
        public void ForQuery(Action<IServiceCollection> action)
        {
            AddQueryAction = action;

            QueryAssembly = null;
        }

        /// <summary>
        /// Set the assemble where the implemented type of query is belong to
        /// </summary>
        public void ForQuery(Assembly assembly)
        {
            QueryAssembly = assembly;

            AddQueryAction = null;
        }
    }
}
