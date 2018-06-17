using Daybreaksoft.Pattern.CQRS.Extensions.AspNetCore;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Daybreaksoft.Pattern.CQRS.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCQRSWithEntityFramework(this IServiceCollection services, Action<CQRSEntityFrameworkOptions> optionsAction)
        {
            if (optionsAction == null) throw new ArgumentNullException(nameof(optionsAction));

            var options = new CQRSEntityFrameworkOptions();

            optionsAction(options);
            
            // Add an service that implemented ICommandBus.
            AspNetCore.ServiceCollectionExtensions.AddService(services, options, typeof(DbContext), options.DbContextType);

            AddRepositoryService(services, options);

            services.AddCQRS(options);

            return services;
        }

        private static void AddRepositoryService(IServiceCollection services, CQRSEntityFrameworkOptions options)
        {
            var serviceTypeName = typeof(IRepository<>).Name;

            if (!options.RegisterImplementationActions.ContainsKey(serviceTypeName))
            {
                options.ForRepository((s) =>
                {
                    s.AddScoped(typeof(IRepository<>), typeof(DefaultRepository<>));
                });
            }
        }
    }
}
