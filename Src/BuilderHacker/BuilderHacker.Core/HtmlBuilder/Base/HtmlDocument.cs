using BuilderHacker.Abstraction.HtmlBuilder;
using System.Text;

namespace BuilderHacker.Core.HtmlBuilder.Base
{
    /// <summary>
    /// Represents a full HTML document node that renders a doctype and root html element.
    /// </summary>
    public sealed class HtmlDocument : HtmlNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlDocument"/> class with the specified top-level HTML nodes.
        /// </summary>
        /// <remarks>
        /// The provided child nodes typically include <c>&lt;head&gt;</c> and <c>&lt;body&gt;</c> elements,
        /// which together form a complete HTML document structure.
        /// </remarks>
        /// <param name="children">
        /// An array of top-level HTML nodes to include in the document. Commonly contains
        /// document structure elements such as <c>&lt;head&gt;</c> and <c>&lt;body&gt;</c>.
        /// </param>
        public HtmlDocument(params IHtmlNode[] children)
        {
            if (children != null)
                Children.AddRange(children);
        }

        /// <summary>
        /// Renders the complete HTML document including the document type declaration,
        /// root <c>&lt;html&gt;</c> element, and all child nodes.
        /// </summary>
        /// <remarks>
        /// The generated output includes:
        /// <list type="bullet">
        /// <item>
        /// <description>The HTML5 <c>&lt;!DOCTYPE html&gt;</c> declaration.</description>
        /// </item>
        /// <item>
        /// <description>The root <c>&lt;html&gt;</c> element.</description>
        /// </item>
        /// <item>
        /// <description>All child nodes rendered in the order they were added.</description>
        /// </item>
        /// </list>
        /// </remarks>
        /// <returns>
        /// A formatted HTML document string representing the rendered document tree.
        /// </returns>
        protected override string RenderNode()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            foreach (var child in Children)
                sb.AppendLine(child.Render());
            sb.AppendLine("</html>");
            return sb.ToString();
        }
    }
}
