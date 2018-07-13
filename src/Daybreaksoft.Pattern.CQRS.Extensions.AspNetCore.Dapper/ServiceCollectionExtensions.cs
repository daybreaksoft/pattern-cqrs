using System;
using System.Data;
using System.Data.SqlClient;
using Daybreaksoft.Pattern.CQRS.DomainModel;
using Microsoft.Extensions.DependencyInjection;

namespace Daybreaksoft.Pattern.CQRS.Extensions.AspNetCore.Dapper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCQRSWithDapper(this IServiceCollection services, Action<CQRSDapperOptions> optionsAction)
        {
            if (optionsAction == null) throw new ArgumentNullException(nameof(optionsAction));

            var options = new CQRSDapperOptions();

            optionsAction(options);

            AddDbConnectionService(services, options);

            if (!options.RegisterImplementationActions.ContainsKey(typeof(IUnitOfWork).Name))
            {
                options.ForUnitOfWork(s => s.AddScoped<IUnitOfWork, Extensions.Dapper.DefaultUnitOfWork>());
            }

            services.AddCQRS(options);

            return services;
        }

        private static void AddDbConnectionService(IServiceCollection services, CQRSDapperOptions options)
        {
            var name = typeof(IDbConnection).Name;

            if (options.RegisterImplementationActions.ContainsKey(name))
            {
                options.RegisterImplementationActions[name].Invoke(services);
            }
            else
            {
                if (string.IsNullOrEmpty(options.ConnectionString))
                {
                    throw new ArgumentNullException(nameof(options.ConnectionString));
                }

                if (options.DatabaseType == DatabaseType.SqlServer)
                {
                    services.AddScoped<IDbConnection>(sp => new SqlConnection(options.ConnectionString));
                }
                else
                {
                    throw new NotSupportedException("Don't support to use MySql.");
                }
            }
        }
    }
}
