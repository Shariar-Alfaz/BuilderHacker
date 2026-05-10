namespace BuilderHacker.Abstraction.Engine
{
    /// <summary>
    /// Factory interface for resolving builders for specific entity types.
    /// </summary>
    public interface IBuilderHackerFactory
    {
        /// <summary>
        /// Creates a new builder instance for type T.
        /// </summary>
        /// <typeparam name="T">The type of entity to build.</typeparam>
        /// <returns>An instance of IBuilder<T>.</returns>
        IBuilder<T> CreateBuilder<T>();

        /// <summary>
        /// Creates a new builder instance of a specific type.
        /// </summary>
        /// <typeparam name="T">The type of entity to build.</typeparam>
        /// <typeparam name="TBuilder">The specific builder type.</typeparam>
        /// <returns>An instance of TBuilder.</returns>
        TBuilder CreateBuilder<T, TBuilder>() where TBuilder : IBuilder<T>;
    }
}
