namespace BuilderHacker.Abstraction.HtmlBuilder
{
    /// <summary>
    /// Represents the content of an HTML &lt;picture&gt; element used in HTML generation or manipulation.
    /// </summary>
    /// <remarks>
    /// Implementations of this interface define or interact with the content inside a &lt;picture&gt; element,
    /// typically for dynamic HTML construction or parsing scenarios.
    /// This interface extends <see cref="IHtmlNode"/>, allowing integration with other HTML node types.
    /// </remarks>
    public interface IPictureContent : IHtmlNode
    {
    }
}