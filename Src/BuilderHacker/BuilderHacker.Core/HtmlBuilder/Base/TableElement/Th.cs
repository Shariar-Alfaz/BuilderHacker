using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML &lt;th&gt; table header cell element.
    /// </summary>
    public sealed class Th : Element, IThOrTd
    {
        /// <summary>
        /// Initializes a new instance of the Th class with the specified child HTML nodes.
        /// </summary>
        /// <param name="children">
        /// An array of child nodes to be added as the content of the table header cell (&lt;th&gt;).
        /// Can include text, links, or other HTML elements.
        /// </param>
        public Th(params IHtmlNode[] children)
            : base("th", children)
        {
        }
    }
}