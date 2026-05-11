using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base
{
    public sealed class HtmlDocument: HtmlNode
    {

        public HtmlDocument(params IHtmlNode[] children)
        {
            if(children != null)
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
