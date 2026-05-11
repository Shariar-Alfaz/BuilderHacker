using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.ListElement
{
    /// <summary>
    /// Represents an HTML <li> (list item) element that can contain child nodes.
    /// </summary>
    /// <remarks>Use the Li class to create list items within ordered or unordered HTML lists. Child nodes can
    /// include text, other elements, or nested lists, allowing for flexible list structures.</remarks>
    public class Li : Element, IListElement
    {
        /// <summary>
        /// Initializes a new instance of the Li class with the specified child HTML nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to be added as the contents of the list item. Can be empty to create an empty list
        /// item. Cannot be null.</param>
        public Li(params IHtmlNode[] children) : base("li", children)
        {
        }
    }
}
