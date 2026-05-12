using BuilderHacker.Abstraction.Engine;
using System;

namespace BuilderHacker.Core.Builder
{
    /// <summary>
    /// Default implementation of <see cref="IBuilderHackerFactory"/> that resolves builder instances
    /// using an <see cref="IServiceProvider"/>.
    /// </summary>
    /// <remarks>
    /// This factory acts as a central resolver for retrieving registered builder instances.
    /// It supports resolving both generic builders and strongly-typed builder implementations.
    /// </remarks>
    public class DefaultBuilderHackerFactory : IBuilderHackerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultBuilderHackerFactory"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider used to resolve registered builder services.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="serviceProvider"/> is null.</exception>
        public DefaultBuilderHackerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        /// <summary>
        /// Creates a builder instance for the specified type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type for which the builder is requested.</typeparam>
        /// <returns>An instance of <see cref="IBuilder{T}"/> for the specified type.</returns>
        /// <exception cref="InvalidOperationException">Thrown when no builder is registered for the specified type.</exception>
        public IBuilder<T> CreateBuilder<T>()
        {
            var builder = _serviceProvider.GetService(typeof(IBuilder<T>)) as IBuilder<T>;
            if (builder == null)
            {
                throw new InvalidOperationException(string.Format("No builder registered for type {0}.", typeof(T).FullName));
            }
            return builder;
        }

        /// <summary>
        /// Creates a strongly typed builder instance for the specified model type.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <typeparam name="TBuilder">The builder type.</typeparam>
        /// <returns>An instance of <typeparamref name="TBuilder"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown when no compatible builder is registered.</exception>
        /// <exception cref="InvalidCastException">Thrown when the resolved builder cannot be cast to <typeparamref name="TBuilder"/>.</exception>
        public TBuilder CreateBuilder<T, TBuilder>() where TBuilder : IBuilder<T>
        {
            var builder = _serviceProvider.GetService(typeof(TBuilder));

            // Fallback to trying to resolve IBuilder<T> and casting
            if (builder == null)
            {
                builder = _serviceProvider.GetService(typeof(IBuilder<T>));
            }

            if (builder == null)
            {
                throw new InvalidOperationException(string.Format("No builder registered for type {0} or {1}.", typeof(TBuilder).FullName, typeof(IBuilder<T>).FullName));
            }

            if (!(builder is TBuilder))
            {
                throw new InvalidCastException(string.Format("Registered builder is not of type {0}.", typeof(TBuilder).FullName));
            }

            return (TBuilder)builder;
        }
    }
}