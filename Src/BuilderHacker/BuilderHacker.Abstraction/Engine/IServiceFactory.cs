using System;

namespace BuilderHacker.Abstraction.Engine
{
    /// <summary>
    /// Provides a generic abstraction for resolving services
    /// and specific implementations from a service provider.
    /// </summary>
    public interface IServiceFactory
    {

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
        /// Thrown when the requested service is not registered.
        /// </exception>
        TService Create<TService>()
            where TService : class;

        /// <summary>
        /// Resolves a specific implementation of a service.
        /// </summary>
        /// <typeparam name="TService">
        /// The service contract type.
        /// </typeparam>
        /// <typeparam name="TImplementation">
        /// The concrete implementation type.
        /// </typeparam>
        /// <returns>
        /// The resolved implementation instance.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the requested implementation is not registered.
        /// </exception>
        TImplementation Create<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;
    }
}
