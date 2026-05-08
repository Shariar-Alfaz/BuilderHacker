namespace BuilderHacker.Abstraction.Engine
{
    /// <summary>
    /// Generic builder interface for constructing entities of type T.
    /// </summary>
    /// <typeparam name="T">The type of entity being built.</typeparam>
    public interface IBuilder<T>
    {
        /// <summary>
        /// Builds and returns the constructed entity.
        /// </summary>
        /// <returns>The constructed entity of type T.</returns>
        T Build();
    }
}
