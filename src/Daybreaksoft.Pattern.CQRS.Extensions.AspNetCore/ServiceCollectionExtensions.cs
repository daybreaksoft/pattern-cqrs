using Daybreaksoft.Pattern.CQRS.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace Daybreaksoft.Pattern.CQRS.Extensions.AspNetCore
{
    /// <summary>
    /// Extension methods for setting up CQRS related services in an Microsoft.Extensions.DependencyInjection.IServiceCollection.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds CQRS services to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection to add services to.</param>
        /// <param name="optionsAction">An System.Action`1 to configure the provided CQRSOptions.</param>
        /// <returns>An Microsoft.Extensions.DependencyInjection.IMvcBuilder that can be used to further configure the CQRS services.</returns>
        public static IServiceCollection AddCQRS(this IServiceCollection services, Action<CQRSOptions> optionsAction)
        {
            if (optionsAction == null) throw new ArgumentNullException(nameof(optionsAction));

            var options = new CQRSOptions();

            optionsAction.Invoke(options);

            return services.AddCQRS(options);
        }

        public static IServiceCollection AddCQRS(this IServiceCollection services, CQRSOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            // Add an service that implemented IDependencyInjection.
            AddSignleService(services, options, typeof(IDependencyInjection), typeof(DefaultDependencyInjection));

            // Add an service that implemented IAggregateBus.
            AddSignleService(services, options, typeof(IAggregateBus), typeof(DefaultAggregateBus));

            // Add an service that implemented IUnitOfWork.
            AddSignleService(services, options, typeof(IUnitOfWork), typeof(DefaultUnitOfWork));

            // Add services that implemented IRepository<>
            AddMultipleServices(services, options, typeof(IRepository<>));

            // Add an service that implemented IDynamicRepositoryFactory.
            AddSignleService(services, options, typeof(IRepositoryFactory), typeof(DefaultRepositoryFactory));

            // Add an service that implemented IDomainAppServiceFactory.
            AddSignleService(services, options, typeof(IDomainServiceFactory), typeof(DefaultDomainServiceFactory));

            // Add services that implemented ICommandExecutor<>
            AddMultipleServices(services, options, typeof(ICommandExecutor<>));

            // Add an service that implemented ICommandBus.
            AddSignleService(services, options, typeof(ICommandBus), typeof(DefaultCommandBus));

            // Add an service that implemented IEventBus.
            AddSignleService(services, options, typeof(IEventBus), typeof(DefaultEventBus));

            // Add services that implemented IEventHandler<>
            AddMultipleServices(services, options, typeof(IEventHandler<>));

            // Add services that implemented IPostCommitEventHandler<>
            AddMultipleServices(services, options, typeof(IPostCommitEventHandler<>));

            // Add services that implemented IQuery
            AddMultipleServices(services, options, typeof(IQuery));

            return services;
        }

        /// <summary>
        /// Add an signle service using provided programmatic configuration of options.
        /// It will use an default implementation as service if not find an action in optins via interface name.
        /// </summary>
        /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection to add services to.</param>
        /// <param name="options">Provides programmatic configuration for the CQRS framework.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="defaultImplementationType">The implementation type of the service.</param>
        public static void AddSignleService(IServiceCollection services, CQRSOptions options, Type serviceType, Type defaultImplementationType, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));

            if (defaultImplementationType == null) throw new ArgumentNullException(nameof(defaultImplementationType));

            var serviceTypeName = serviceType.Name;

            if (options.RegisterImplementationActions.ContainsKey(serviceTypeName))
            {
                options.RegisterImplementationActions[serviceTypeName].Invoke(services);
            }
            else
            {
                services.Add(new ServiceDescriptor(serviceType, defaultImplementationType, lifetime));
            }
        }

        public static void AddMultipleServices(IServiceCollection services, CQRSOptions options, Type serviceType, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var serviceTypeName = serviceType.Name;

            if (options.RegisterImplementationActions.ContainsKey(serviceTypeName))
            {
                options.RegisterImplementationActions[serviceTypeName].Invoke(services);
            }
            else
            {
                if (!options.ImplementationSources.ContainsKey(serviceTypeName))
                    throw new Exception($"Cannot found implementation souce via {serviceTypeName}");

                var implementationSouce = options.ImplementationSources[serviceTypeName];

                var exportedTypes = implementationSouce.Assembly.GetExportedTypes();

                if (!string.IsNullOrEmpty(implementationSouce.UnderNamespace))
                {
                    exportedTypes = exportedTypes.Where(p => p.Namespace.Contains(implementationSouce.UnderNamespace)).ToArray();
                }

                foreach (var exportedType in exportedTypes)
                {
                    var registersService = exportedType.GetInterfaces().SingleOrDefault(p => p.Name == serviceTypeName);

                    if (registersService != null)
                    {
                        if (registersService.IsGenericType)
                        {
                            services.Add(new ServiceDescriptor(registersService, exportedType, lifetime));
                        }
                        else
                        {
                            if (lifetime == ServiceLifetime.Scoped)
                            {
                                services.AddScoped(exportedType);
                            }
                            else if (lifetime == ServiceLifetime.Transient)
                            {
                                services.AddTransient(exportedType);
                            }
                            else if (lifetime == ServiceLifetime.Singleton)
                            {
                                services.AddSingleton(exportedType);
                            }

                        }
                    }
                }
            }
        }
    }
}
