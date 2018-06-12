using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Daybreaksoft.Pattern.CQRS.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Daybreaksoft.Pattern.CQRS.Extensions.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCQRSWithEntityFramework(this IServiceCollection services, Action<CQRSEntityFrameworkOptionBuilder> builderAction)
        {
            if (builderAction == null) throw new ArgumentNullException(nameof(builderAction));

            var builder = new CQRSEntityFrameworkOptionBuilder();

            builderAction(builder);

            // Add TDbContext as IDbContext if don't have custom DI action
            if (builder.AddDbContextAction == null)
            {
                services.TryAddScoped(typeof(IDbContext), builder.DbContextType);
            }
            else
            {
                builder.AddDbContextAction(services);
            }

            // Add DefaultRepository<> as IRepository<> if don't have custom DI action
            if (builder.AddRepositoryAction == null)
            {
                services.TryAddScoped(typeof(IRepository<>), typeof(DefaultRepository<>));
            }

            services.AddCQRS(builder);

            return services;
        }
    }
}
