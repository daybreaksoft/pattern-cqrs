using Daybreaksoft.Pattern.CQRS.Extensions.AspNetCore;
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
            AspNetCore.ServiceCollectionExtensions.AddSignleService(services, options, typeof(DbContext), options.DbContextType);

            if (!options.RegisterImplementationActions.ContainsKey(typeof(IUnitOfWork).Name))
            {
                options.ForUnitOfWork(s => s.AddScoped<IUnitOfWork, DefaultUnitOfWork>());
            }

            services.AddCQRS(options);

            return services;
        }
    }
}
