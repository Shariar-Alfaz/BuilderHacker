using BuilderHacker.Abstraction.HtmlBuilder;
using System.Collections.Generic;
using System.Text;

namespace BuilderHacker.Core.HtmlBuilder.Base
{
    /// <summary>
    /// Base implementation for HTML nodes that supports attributes, child nodes, and rendering.
    /// </summary>
    public abstract class HtmlNode : IHtmlNode
    {
        private Dictionary<string, string> Attributes { get; } = new Dictionary<string, string>();
        protected List<IHtmlNode> Children { get; } = new List<IHtmlNode>();



        public IHtmlNode Attr(string key, string value)
        {
            HtmlValidator.ValidateAttribute(key, value);
            Attributes[key] = value;
            return this;
        }

        public IHtmlNode Style(string value)
        {
            HtmlValidator.ValidateStyle(value);
            Attributes["style"] = value;
            return this;
        }

        public IHtmlNode Class(string value)
        {
            HtmlValidator.ValidateClassName(value);
            Attributes["class"] = value;
            return this;
        }

        public string Render() => RenderNode();

        protected virtual string RenderNode()
        {
            var sb = new StringBuilder();
            sb.Append($"<{GetType().Name.ToLower()}{RenderAttributes()}>");

            foreach (var child in Children)
                sb.Append(child.Render());

            sb.Append($"</{GetType().Name.ToLower()}>");
            return sb.ToString();
        }

        protected string RenderAttributes()
        {
            if (Attributes.Count == 0) return "";

            var sb = new StringBuilder();

            foreach (var attr in Attributes)
                sb.Append($" {attr.Key}=\"{attr.Value}\"");

            return sb.ToString();
        }

    }
}
