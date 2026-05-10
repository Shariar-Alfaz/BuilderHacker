using System.Collections.Generic;
using System.Text;

namespace BuilderHacker.Abstraction.HtmlBuilder
{
    public abstract class HtmlNode
    {
        public Dictionary<string, string> Attributes { get; } = new Dictionary<string, string>();
        public List<HtmlNode> Children { get; } = new List<HtmlNode>();
      


        public HtmlNode Attr(string key, string value)
        {
            HtmlValidator.ValidateAttribute(key, value);
            Attributes[key] = value;
            return this;
        }

        public HtmlNode Style(string value)
        {
            HtmlValidator.ValidateStyle(value);
            Attributes["style"] = value;
            return this;
        }

        public HtmlNode Class(string value)
        {
            HtmlValidator.ValidateClassName(value);
            Attributes["class"] = value;
            return this;
        }

        public string Render() => RenderNode();

        protected abstract string RenderNode();

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
