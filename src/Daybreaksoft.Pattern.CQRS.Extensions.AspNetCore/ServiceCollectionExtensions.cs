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

            if (builder.AddCommandExecutorAction == null && builder.CommandExecutorAssembly == null)
            {
                throw new ArgumentNullException($"{nameof(builder.AddCommandExecutorAction)} and {builder.CommandExecutorAssembly} can't all be null");
            }

            if (builder.AddDomainModelAction == null && builder.DomainModelAssembly == null)
            {
                throw new ArgumentNullException($"{nameof(builder.AddDomainModelAction)} and {builder.DomainModelAssembly} can't all be null");
            }

            AddRepositoryImplemention(services, builder);

            AddDependencyInjectionImplemention(services, builder);

            AddCommandBusImplemention(services, builder);

            AddCommandExecutorImplemention(services, builder);

            AddDomainModelBuilderImplemention(services, builder);

            AddModelImplemention(services, builder);

            AddQueryImplemention(services, builder);

            return services;
        }

        /// <summary>
        /// Execute custom DI action
        /// </summary>
        private static void AddRepositoryImplemention(IServiceCollection services, CQRSOptionBuilder builder)
        {
            builder.AddRepositoryAction?.Invoke(services);
        }

        /// <summary>
        /// Add DefaultDependencyInjection as IDependencyInjection if don't have custom DI action
        /// </summary>
        private static void AddDependencyInjectionImplemention(IServiceCollection services, CQRSOptionBuilder builder)
        {
            if (builder.AddDependencyInjectionAction == null)
            {
                services.AddScoped<IDependencyInjection, DefaultDependencyInjection>();
            }
            else
            {
                builder.AddRepositoryAction(services);
            }
        }

        /// <summary>
        /// Add DefaultCommandBus as ICommandBus if don't have custom DI action
        /// </summary>
        private static void AddCommandBusImplemention(IServiceCollection services, CQRSOptionBuilder builder)
        {
            if (builder.AddCommandBusAction == null)
            {
                services.AddScoped<ICommandBus, DefaultCommandBus>();
            }
            else
            {
                builder.AddCommandBusAction(services);
            }
        }

        /// <summary>
        /// Default to add all CommandExecutor that implements ICommandExecutor<> if don't have custom DI action
        /// </summary>
        private static void AddCommandExecutorImplemention(IServiceCollection services, CQRSOptionBuilder builder)
        {
            if (builder.AddCommandExecutorAction == null)
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
                builder.AddCommandExecutorAction(services);
            }
        }

        /// <summary>
        /// Add DefaultDomainModelBuilder as IDomainModelBuilder if don't have custom DI action
        /// </summary>
        private static void AddDomainModelBuilderImplemention(IServiceCollection services, CQRSOptionBuilder builder)
        {
            if (builder.AddDomainModelBuilderAction == null)
            {
                services.AddScoped<IDomainModelBuilder, DefaultDomainModelBuilder>();
            }
            else
            {
                builder.AddDomainModelBuilderAction(services);
            }
        }

        /// <summary>
        /// Default to add all Model implement class if don't have custom DI action
        /// </summary>
        private static void AddModelImplemention(IServiceCollection services, CQRSOptionBuilder builder)
        {
            if (builder.AddDomainModelAction == null)
            {
                if (builder.DomainModelAssembly != null)
                {
                    var baseDomainModelType = typeof(IDomainModel);

                    // Find all exported types
                    foreach (var implementationType in builder.DomainModelAssembly.GetExportedTypes())
                    {
                        // Find implementation type that base of IDomainModel
                        var targentInterface = implementationType.GetInterfaces().SingleOrDefault(p => p == baseDomainModelType);
                        if (targentInterface != null)
                        {
                            services.AddTransient(implementationType);
                        }
                    }
                }
            }
            else
            {
                builder.AddDomainModelAction(services);
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
    }
}
