using System;

namespace Daybreaksoft.Pattern.CQRS
{
    /// <summary>
    /// The interface of Dependency Injection.
    /// </summary>
    public interface IDependencyInjection
    {
        /// <summary>
        /// Get the instance via service type.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <returns>Return the service instance.</returns>
        object GetService(Type serviceType);

        /// <summary>
        /// Get the instance via service type.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <returns>Return the service instance.</returns>
        TService GetService<TService>();
    }
}
