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


            // Add DefaultRepository<> as IRepository<> if don't have custom DI action
            //if (options.RegisterRepositoryImplementationAction == null)
            //{
            //    services.AddScoped(typeof(IRepository<>), typeof(DefaultRepository<>));
            //}

            services.AddCQRS(options);

            return services;
        }
    }
}
