namespace BuilderHacker.Abstraction.HtmlBuilder
{
    /// <summary>
    /// Represents the content of a <picture> HTML element for use in HTML generation or manipulation.
    /// </summary>
    /// <remarks>Implementations of this interface provide the ability to define or interact with the content
    /// within a <picture> element, typically for scenarios involving dynamic HTML construction or parsing. This
    /// interface extends IHtmlNode, allowing integration with other HTML node types.</remarks>
    public interface IPictureContent : IHtmlNode
    {
    }
}
