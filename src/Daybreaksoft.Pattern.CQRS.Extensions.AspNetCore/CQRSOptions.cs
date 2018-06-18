using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Daybreaksoft.Pattern.CQRS.Extensions.AspNetCore
{
    /// <summary>
    /// Provides programmatic configuration for the CQRS framework.
    /// </summary>
    public class CQRSOptions
    {
        /// <summary>
        /// Creates a new instance of CQRSOptions.
        /// </summary>
        public CQRSOptions()
        {
            RegisterImplementationActions = new Dictionary<string, Action<IServiceCollection>>();
            ImplementationSources = new Dictionary<string, ImplementationSource>();
        }

        /// <summary>
        /// A collection that encapsulates a method that registers the implementation of interface as a service.
        /// Uses the name of interface as key.
        /// </summary>
        public Dictionary<string, Action<IServiceCollection>> RegisterImplementationActions { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, ImplementationSource> ImplementationSources { get; protected set; }
        
        /// <summary>
        /// Adds a method that registers the implementation of IRepository<> as a service.
        /// </summary>
        public void ForRepository(Action<IServiceCollection> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            RegisterImplementationActions.Add(typeof(IRepository<>).Name, action);
        }

        /// <summary>
        /// Set the assemble where the implemented type of query is belong to
        /// </summary>
        public void ForRepository(Assembly assembly, string underNamespace = null)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            var serviceName = typeof(IRepository<>).Name;

            // Add implementation souce
            ImplementationSources.Add(serviceName, new ImplementationSource(assembly, underNamespace));

            // Remove action via service name
            RegisterImplementationActions.Remove(serviceName);
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
        /// Adds a method that registers the implementation of IUnitOfWork as a service
        /// </summary>
        public void ForUnitOfWork(Action<IServiceCollection> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            RegisterImplementationActions.Add(typeof(IUnitOfWork).Name, action);
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

            var serviceName = typeof(ICommandExecutor<>).Name;

            // Add action
            RegisterImplementationActions.Add(serviceName, action);

            // Remove implementation source via service name
            ImplementationSources.Remove(serviceName);
        }

        /// <summary>
        /// Set the assemble where the implemented type of command executor is belong to
        /// </summary>
        public void ForCommandExecutor(Assembly assembly, string underNamespace = null)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            var serviceName = typeof(ICommandExecutor<>).Name;

            // Add implementation souce
            ImplementationSources.Add(serviceName, new ImplementationSource(assembly, underNamespace));

            // Remove action via service name
            RegisterImplementationActions.Remove(serviceName);
        }

        /// <summary>
        /// Adds a method that registers the implementation of IEventBus as a service
        /// </summary>
        public void ForEventBus(Action<IServiceCollection> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            RegisterImplementationActions.Add(typeof(IEventBus).Name, action);
        }

        public void ForEventHandler(Action<IServiceCollection> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            var serviceName = typeof(IEventHandler<>).Name;

            // Add action
            RegisterImplementationActions.Add(serviceName, action);

            // Remove implementation source via service name
            ImplementationSources.Remove(serviceName);
        }

        /// <summary>
        /// Set the assemble where the implemented type of command executor is belong to
        /// </summary>
        public void ForEventHandler(Assembly assembly, string underNamespace = null)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            var serviceName = typeof(IEventHandler<>).Name;

            // Add implementation souce
            ImplementationSources.Add(serviceName, new ImplementationSource(assembly, underNamespace));

            // Remove action via service name
            RegisterImplementationActions.Remove(serviceName);
        }

        public void ForPostCommitEventHandler(Action<IServiceCollection> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            var serviceName = typeof(IPostCommitEventHandler<>).Name;

            // Add action
            RegisterImplementationActions.Add(serviceName, action);

            // Remove implementation source via service name
            ImplementationSources.Remove(serviceName);
        }

        /// <summary>
        /// Set the assemble where the implemented type of command executor is belong to
        /// </summary>
        public void ForPostCommitEventHandler(Assembly assembly, string underNamespace = null)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            var serviceName = typeof(IPostCommitEventHandler<>).Name;

            // Add implementation souce
            ImplementationSources.Add(serviceName, new ImplementationSource(assembly, underNamespace));

            // Remove action via service name
            RegisterImplementationActions.Remove(serviceName);
        }

        /// <summary>
        /// Adds a method that registers the implementation of IAggregateBuilder as a service
        /// </summary>
        public void ForAggregateBuilder(Action<IServiceCollection> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            RegisterImplementationActions.Add(typeof(IAggregateBus).Name, action);
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
        /// Adds a method that registers the implementation of IAggregateRoot as a service
        /// </summary>
        public void ForAggregate(Action<IServiceCollection> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            var serviceName = typeof(IAggregateRoot).Name;

            // Add action
            RegisterImplementationActions.Add(serviceName, action);

            // Remove implementation souce via service name
            ImplementationSources.Remove(serviceName);
        }

        /// <summary>
        /// Set the assemble where the implemented type of query is belong to
        /// </summary>
        public void ForAggregate(Assembly assembly, string underNamespace = null)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            var serviceName = typeof(IAggregateRoot).Name;

            // Add implementation souce
            ImplementationSources.Add(serviceName, new ImplementationSource(assembly, underNamespace));

            // Remove action via service name
            RegisterImplementationActions.Remove(serviceName);
        }

        /// <summary>
        /// Adds a method that registers the implementation of IQuery as a service
        /// </summary>
        public void ForQuery(Action<IServiceCollection> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            var serviceName = typeof(IQuery).Name;

            // Add action
            RegisterImplementationActions.Add(serviceName, action);

            // Remove implementation souce via service name
            ImplementationSources.Remove(serviceName);
        }

        /// <summary>
        /// Set the assemble where the implemented type of query is belong to
        /// </summary>
        public void ForQuery(Assembly assembly, string underNamespace = null)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            var serviceName = typeof(IQuery).Name;

            // Add implementation souce
            ImplementationSources.Add(serviceName, new ImplementationSource(assembly, underNamespace));

            // Remove action via service name
            RegisterImplementationActions.Remove(serviceName);
        }
    }
}
