using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Daybreaksoft.Pattern.CQRS.Extensions.AspNetCore
{
    /// <summary>
    /// CQRS options builder
    /// </summary>
    public class CQRSOptionBuilder
    {
        public CQRSOptionBuilder()
        {
            RegisterImplementationActions = new Dictionary<string, Action<IServiceCollection>>();
        }

        /// <summary>
        /// A collection that encapsulates a method that registers the implementation of interface as a service.
        /// Uses the name of interface as key.
        /// </summary>
        public Dictionary<string, Action<IServiceCollection>> RegisterImplementationActions { get; protected set; }

        #region Properties

        /// <summary>
        /// Encapsulates a method that registers the implementation of IRepository<> as a service.
        /// </summary>
        public Action<IServiceCollection> RegisterRepositoryImplementationAction { get; protected set; }

        /// <summary>
        /// Encapsulates a method that registers the implementation of IDependencyInjection as a service.
        /// </summary>
        public Action<IServiceCollection> RegisterDependencyInjectionImplementationAction { get; protected set; }

        /// <summary>
        /// Encapsulates a method that registers the implementation of ICommandBus as a service.
        /// </summary>
        public Action<IServiceCollection> RegisterCommandBusImplementationAction { get; protected set; }

        /// <summary>
        /// Encapsulates a method that registers the implementation of ICommandExecute as a service.
        /// </summary>
        public Action<IServiceCollection> RegisterCommandExecutorImplementationAction { get; protected set; }

        /// <summary>
        /// The assembly where the implemented type of command executor belong to
        /// </summary>
        public Assembly CommandExecutorAssembly { get; protected set; }

        /// <summary>
        /// Encapsulates a method that registers the implementation of IEventBus as a service.
        /// </summary>
        public Action<IServiceCollection> RegisterEventBusImplementationAction { get; protected set; }

        /// <summary>
        /// Encapsulates a method that registers the implementation of IAggregateBuilder as a service.
        /// </summary>
        public Action<IServiceCollection> RegisterAggregateBuilderImplementationAction { get; protected set; }

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
        /// Adds a method that registers the implementation of IRepository<> as a service
        /// </summary>
        public void ForRepository(Action<IServiceCollection> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            RegisterImplementationActions.Add(typeof(IRepository<>).Name, action);
        }

        /// <summary>
        /// Adds a method that registers the implementation of IDependencyInjection as a service
        /// </summary>
        public void ForDependencyInjection(Action<IServiceCollection> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            RegisterImplementationActions.Add(typeof(IDependencyInjection).Name, action);
        }

        /// <summary>
        /// Adds a method that registers the implementation of ICommandBus as a service
        /// </summary>
        public void ForCommandBus(Action<IServiceCollection> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            RegisterImplementationActions.Add(typeof(ICommandBus).Name, action);
        }

        /// <summary>
        /// Adds a method that registers the implementation of ICommandExecutor<> as a service
        /// </summary>
        public void ForCommandExecutor(Action<IServiceCollection> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            RegisterImplementationActions.Add(typeof(ICommandExecutor<>).Name, action);
        }

        /// <summary>
        /// Set the assemble where the implemented type of command executor is belong to
        /// </summary>
        public void ForCommandExecutor(Assembly assembly)
        {
            CommandExecutorAssembly = assembly;

            RegisterCommandExecutorImplementationAction = null;
        }

        /// <summary>
        /// Adds a method that registers the implementation of IEventBus as a service
        /// </summary>
        public void ForEventBus(Action<IServiceCollection> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            RegisterImplementationActions.Add(typeof(IEventBus).Name, action);
        }

        /// <summary>
        /// Adds a method that registers the implementation of IAggregateBuilder as a service
        /// </summary>
        public void ForAggregateBuilder(Action<IServiceCollection> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            RegisterImplementationActions.Add(typeof(IAggregateBuilder).Name, action);
        }

        /// <summary>
        /// Adds a method that registers the implementation of IDynamicRepositoryFactory as a service
        /// </summary>
        public void ForDynamicRepositoryFactory(Action<IServiceCollection> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            RegisterImplementationActions.Add(typeof(IDynamicRepositoryFactory).Name, action);
        }

        /// <summary>
        /// Adds a method that registers the implementation of IQuery as a service
        /// </summary>
        public void ForQuery(Action<IServiceCollection> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            RegisterImplementationActions.Add(typeof(IQuery).Name, action);
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
