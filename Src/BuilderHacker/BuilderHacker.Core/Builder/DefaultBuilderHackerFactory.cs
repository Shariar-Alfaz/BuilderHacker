using BuilderHacker.Abstraction.Engine;
using System;

namespace BuilderHacker.Core.Builder
{
    /// <summary>
    /// Default implementation of IBuilderHackerFactory that uses a service provider to resolve builders.
    /// </summary>
    public class DefaultBuilderHackerFactory : IBuilderHackerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DefaultBuilderHackerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IBuilder<T> CreateBuilder<T>()
        {
            var builder = _serviceProvider.GetService(typeof(IBuilder<T>)) as IBuilder<T>;
            if (builder == null)
            {
                throw new InvalidOperationException(string.Format("No builder registered for type {0}.", typeof(T).FullName));
            }
            return builder;
        }

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
