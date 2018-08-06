using Daybreaksoft.Pattern.CQRS.Extensions.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCQRSWithEntityFramework(this IServiceCollection services, Action<CqrsEntityFrameworkOptions> optionsAction)
        {
            if (optionsAction == null) throw new ArgumentNullException(nameof(optionsAction));

            var options = new CqrsEntityFrameworkOptions();

            optionsAction(options);

            // Add an service that implemented IUnitOfWork.
            AspNetCore.ServiceCollectionExtensions.AddSignleService(services, options, typeof(IUnitOfWork), typeof(DefaultUnitOfWork));

            // Add an service that implemented DbContext.
            AspNetCore.ServiceCollectionExtensions.AddSignleService(services, options, typeof(DbContext), options.DbContextType);

            // Add an service that implemented IRepository.
            AspNetCore.ServiceCollectionExtensions.AddSignleService(services, options, typeof(IRepository<>), typeof(DefaultRepository<>));

            services.AddCQRS(options);

            return services;
        }
    }
}
