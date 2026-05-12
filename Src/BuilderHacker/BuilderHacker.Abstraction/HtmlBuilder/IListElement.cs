namespace BuilderHacker.Abstraction.HtmlBuilder
{
    /// <summary>
    /// Represents a list element node in an HTML document.
    /// </summary>
    /// <remarks>
    /// This interface is typically implemented by types that model HTML list elements such as
    /// &lt;ul&gt;, &lt;ol&gt;, or &lt;li&gt; nodes.
    /// It extends <see cref="IHtmlNode"/> to provide a common contract for working with list-related
    /// HTML elements in a document object model.
    /// </remarks>
    public interface IListElement : IHtmlNode
    {
    }
}