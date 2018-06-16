using Daybreaksoft.Pattern.CQRS.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Daybreaksoft.Pattern.CQRS.Extensions.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCQRS(this IServiceCollection services, Action<CQRSOptionBuilder> builderAction)
        {
            var builder = new CQRSOptionBuilder();

            builderAction?.Invoke(builder);

            return services.AddCQRS(builder);
        }

        public static IServiceCollection AddCQRS(this IServiceCollection services, CQRSOptionBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            if (builder.RegisterCommandExecutorImplementationAction == null && builder.CommandExecutorAssembly == null)
            {
                throw new ArgumentNullException($"{nameof(builder.RegisterCommandExecutorImplementationAction)} and {builder.CommandExecutorAssembly} can't all be null");
            }

            AddRepositoryImplemention(services, builder);

            AddDependencyInjectionImplemention(services, builder);

            AddCommandBusImplemention(services, builder);

            AddCommandExecutorImplemention(services, builder);

            AddEventBusImplemention(services, builder);

            AddAggregateBuilderImplemention(services, builder);

            AddDynamicRepositoryFactoryImplemention(services, builder);

            AddQueryImplemention(services, builder);

            AddUnitOfWorkImplemention(services, builder);

            return services;
        }

        /// <summary>
        /// Execute custom DI action
        /// </summary>
        private static void AddRepositoryImplemention(IServiceCollection services, CQRSOptionBuilder builder)
        {
            builder.RegisterRepositoryImplementationAction?.Invoke(services);
        }

        /// <summary>
        /// Add DefaultDependencyInjection as IDependencyInjection if don't have custom DI action
        /// </summary>
        private static void AddDependencyInjectionImplemention(IServiceCollection services, CQRSOptionBuilder builder)
        {
            if (builder.RegisterDependencyInjectionImplementationAction == null)
            {
                services.AddScoped<IDependencyInjection, DefaultDependencyInjection>();
            }
            else
            {
                builder.RegisterRepositoryImplementationAction(services);
            }
        }

        /// <summary>
        /// Add DefaultCommandBus as ICommandBus if don't have custom DI action
        /// </summary>
        private static void AddCommandBusImplemention(IServiceCollection services, CQRSOptionBuilder builder)
        {
            if (builder.RegisterCommandBusImplementationAction == null)
            {
                services.AddScoped<ICommandBus, DefaultCommandBus>();
            }
            else
            {
                builder.RegisterCommandBusImplementationAction(services);
            }
        }

        /// <summary>
        /// Default to add all CommandExecutor that implements ICommandExecutor<> if don't have custom DI action
        /// </summary>
        private static void AddCommandExecutorImplemention(IServiceCollection services, CQRSOptionBuilder builder)
        {
            if (builder.RegisterCommandExecutorImplementationAction == null)
            {
                if (builder.CommandExecutorAssembly != null)
                {
                    var baseInterfaceName = typeof(ICommandExecutor<>).Name;
                    var registeredInterface = new List<Type>();

                    // Find all exported types
                    foreach (var implementationType in builder.CommandExecutorAssembly.GetExportedTypes())
                    {
                        // Find interface that base of ICommandExecutor<>
                        var targetInterface = implementationType.GetInterfaces().SingleOrDefault(p => p.IsGenericType && p.Name == baseInterfaceName);

                        if (targetInterface != null)
                        {
                            // One command only can be used by one command executor
                            if (registeredInterface.Any(p => p == targetInterface))
                            {
                                throw new InvalidOperationException($"{targetInterface.FullName} cannot be registered twice.");
                            }
                            else
                            {
                                services.AddTransient(targetInterface, implementationType);
                                registeredInterface.Add(targetInterface);
                            }
                        }
                    }
                }
            }
            else
            {
                builder.RegisterCommandExecutorImplementationAction(services);
            }
        }

        /// <summary>
        /// Add DefaultEventBus as IEventBus if don't have custom DI action
        /// </summary>
        private static void AddEventBusImplemention(IServiceCollection services, CQRSOptionBuilder builder)
        {
            if (builder.RegisterEventBusImplementationAction == null)
            {
                services.AddScoped<IEventBus, DefaultEventBus>();
            }
            else
            {
                builder.RegisterEventBusImplementationAction(services);
            }
        }

        /// <summary>
        /// Add DefaultAggregateBuilder as IAggregateBuilder if don't have custom DI action
        /// </summary>
        private static void AddAggregateBuilderImplemention(IServiceCollection services, CQRSOptionBuilder builder)
        {
            if (builder.RegisterAggregateBuilderImplementationAction == null)
            {
                services.AddScoped<IAggregateBuilder, DefaultAggregateBuilder>();
            }
            else
            {
                builder.RegisterAggregateBuilderImplementationAction(services);
            }
        }

        /// <summary>
        /// Add DefaultAggregateBuilder as IDynamicRepositoryFactory if don't have custom DI action
        /// </summary>
        private static void AddDynamicRepositoryFactoryImplemention(IServiceCollection services, CQRSOptionBuilder builder)
        {
            if (builder.AddDynamicRepositoryFactoryAction == null)
            {
                services.AddScoped<IDynamicRepositoryFactory, DefaultDynamicRepositoryFactory>();
            }
            else
            {
                builder.AddDynamicRepositoryFactoryAction(services);
            }
        }

        /// <summary>
        /// Default to add all Query implement class if don't have custom DI action
        /// </summary>
        private static void AddQueryImplemention(IServiceCollection services, CQRSOptionBuilder builder)
        {
            if (builder.AddQueryAction == null)
            {
                if (builder.QueryAssembly != null)
                {
                    var baseQueryType = typeof(IQuery);

                    // Find all exported types
                    foreach (var implementationType in builder.QueryAssembly.GetExportedTypes())
                    {
                        // Find implementation type that base of IQuery
                        var targentInterface = implementationType.GetInterfaces().SingleOrDefault(p => p == baseQueryType);
                        if (targentInterface != null)
                        {
                            services.AddScoped(implementationType);
                        }
                    }
                }
            }
            else
            {
                builder.AddQueryAction(services);
            }
        }

        /// <summary>
        /// Add UnitOfWork as IUnitOfWork if don't have custom DI action
        /// </summary>
        private static void AddUnitOfWorkImplemention(IServiceCollection services, CQRSOptionBuilder builder)
        {
            if (builder.AddUnitOfWorkAction == null)
            {
                services.AddScoped<IUnitOfWork, DefaultUnitOfWork>();
            }
            else
            {
                builder.AddUnitOfWorkAction(services);
            }
        }
    }
}
