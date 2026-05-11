using BuilderHacker.Abstraction.HtmlBuilder;
using System.Text;

namespace BuilderHacker.Core.HtmlBuilder.Base
{
    /// <summary>
    /// Represents a full HTML document node that renders a doctype and root html element.
    /// </summary>
    public sealed class HtmlDocument : HtmlNode
    {

        public HtmlDocument(params IHtmlNode[] children)
        {
            if (children != null)
                Children.AddRange(children);
        }

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
