using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Daybreaksoft.Pattern.CQRS.Extensions.AspNetCore
{
    /// <summary>
    /// Default to use IServiceProvider of AspNetCore as IDependencyInjection
    /// </summary>
    public class DefaultDependencyInjection : IDependencyInjection
    {
        protected readonly IServiceProvider ServiceProvider;

        /// <summary>
        /// Get service of type T from the System.IServiceProvider.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">There is no service of type T.</exception>
        public DefaultDependencyInjection(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public object GetService(Type serviceType)
        {
            return ServiceProvider.GetRequiredService(serviceType);
        }

        /// <summary>
        /// Get service of type T from the System.IServiceProvider.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">There is no service of type T.</exception>
        public TService GetService<TService>()
        {
            return ServiceProvider.GetRequiredService<TService>();
        }
    }
}
