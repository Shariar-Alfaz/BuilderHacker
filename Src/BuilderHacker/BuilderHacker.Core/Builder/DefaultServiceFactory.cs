using BuilderHacker.Abstraction.Engine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuilderHacker.Core.Builder
{
    /// <summary>
    /// Default implementation of <see cref="IServiceFactory"/>.
    /// Resolves services from the registered <see cref="IServiceProvider"/>.
    /// </summary>
    /// <remarks>
    /// This factory provides a centralized way to resolve registered services and their
    /// concrete implementations using dependency injection.
    /// </remarks>
    public class DefaultServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultServiceFactory"/> class.
        /// </summary>
        /// <param name="serviceProvider">
        /// The service provider used to resolve registered services.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="serviceProvider"/> is null.
        /// </exception>
        public DefaultServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        /// <summary>
        /// Resolves a service of type <typeparamref name="TService"/>.
        /// </summary>
        /// <typeparam name="TService">
        /// The service type to resolve.
        /// </typeparam>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the requested service is not registered in the service provider.
        /// </exception>
        public TService Create<TService>()
            where TService : class
        {
            try
            {
                return GetService<TService>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Resolves a specific implementation of a service interface.
        /// </summary>
        /// <typeparam name="TService">
        /// The service contract type.
        /// </typeparam>
        /// <typeparam name="TImplementation">
        /// The concrete implementation type that implements <typeparamref name="TService"/>.
        /// </typeparam>
        /// <returns>
        /// The resolved implementation instance.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when no matching implementation is registered for the requested service type.
        /// </exception>
        public TImplementation Create<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            try
            {
                var services = _serviceProvider.GetService(typeof(IEnumerable<TService>)) as IEnumerable<TService>;

                if (services == null)
                {
                    throw new InvalidOperationException($"No services registered for {typeof(TService).FullName}");
                }

                var implementation = services.OfType<TImplementation>().FirstOrDefault();

                if (implementation == null)
                {
                    throw new InvalidOperationException(
                        $"No implementation of type {typeof(TImplementation).FullName} registered for {typeof(TService).FullName}");
                }

                return implementation;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Resolves a service instance from the underlying service provider.
        /// </summary>
        /// <typeparam name="TInnerService">
        /// The type of service to resolve.
        /// </typeparam>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the requested service is not registered in the service provider.
        /// </exception>
        private TInnerService GetService<TInnerService>()
        {
            var service = _serviceProvider.GetService(typeof(TInnerService));
            if (service == null)
            {
                throw new InvalidOperationException(
                    $"No service registered for {typeof(TInnerService).FullName}");
            }
            return (TInnerService)service;
        }
    }
}