using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML <td> table data cell element.
    /// </summary>
    public sealed class Td : Element, IThOrTd
    {
        /// <summary>
        /// Initializes a new instance of the Td class with the specified child HTML nodes.
        /// </summary>
        /// <remarks>Use this constructor to create a table cell element (<td>) with one or more child
        /// nodes, such as text or other HTML elements.</remarks>
        /// <param name="children">An array of child nodes to be added as the contents of the table cell. Can be empty to create an empty cell.</param>
        public Td(params IHtmlNode[] children) : base("td", children)
        {
        }
    }
}
