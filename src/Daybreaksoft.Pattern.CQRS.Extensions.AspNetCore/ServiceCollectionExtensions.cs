using Daybreaksoft.Pattern.CQRS.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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

            // Execute custom DI action
            builder.AddRepositoryAction?.Invoke(services);

            // Add DefaultDependencyInjection as IDependencyInjection if don't have custom DI action
            if (builder.AddDependencyInjectionAction == null)
            {
                services.TryAddScoped<IDependencyInjection, DefaultDependencyInjection>();
            }
            else
            {
                builder.AddRepositoryAction(services);
            }

            // Add DefaultCommandBus as ICommandBus if don't have custom DI action
            if (builder.AddCommandBusAction == null)
            {
                services.TryAddScoped<ICommandBus, DefaultCommandBus>();
            }
            else
            {
                builder.AddCommandBusAction(services);
            }

            // Default to add all CommandExecutor that implements ICommandExecutor<> if don't have custom DI action
            if (builder.AddCommandExecutorAction == null)
            {
                var baseInterfaceName = typeof(ICommandExecutor<>).Name;
                var registeredInterface = new List<Type>();

                foreach (var implementationType in builder.CommandExecutorAssembly.GetExportedTypes())
                {
                    var targetInterface = implementationType.GetInterfaces().SingleOrDefault(p => p.Name == baseInterfaceName);

                    if (targetInterface != null)
                    {
                        if (registeredInterface.Any(p => p == targetInterface))
                        {
                            throw new InvalidOperationException($"{targetInterface.FullName} cannot be registered twice.");
                        }
                        else
                        {
                            services.TryAddTransient(targetInterface, implementationType);
                            registeredInterface.Add(targetInterface);
                        }

                    }
                }
            }
            else
            {
                builder.AddCommandExecutorAction(services);
            }

            return services;
        }
    }
}
