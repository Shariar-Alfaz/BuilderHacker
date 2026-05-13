using BuilderHacker.Abstraction.HtmlBuilder;
using System.Collections.Generic;
using System.Text;

namespace BuilderHacker.Core.HtmlBuilder.Base
{
    /// <summary>
    /// Base implementation for HTML nodes that provides support for attributes,
    /// child node composition, styling, and HTML rendering.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="HtmlNode"/> serves as the core foundation for all HTML node types
    /// in the BuilderHacker HTML builder system.
    /// </para>
    ///
    /// <para>
    /// This class manages:
    /// </para>
    ///
    /// <list type="bullet">
    /// <item>
    /// <description>HTML attribute storage and rendering.</description>
    /// </item>
    /// <item>
    /// <description>Child node composition and traversal.</description>
    /// </item>
    /// <item>
    /// <description>CSS class and inline style assignment.</description>
    /// </item>
    /// <item>
    /// <description>Validation through <see cref="HtmlValidator"/>.</description>
    /// </item>
    /// <item>
    /// <description>HTML string generation through the rendering pipeline.</description>
    /// </item>
    /// </list>
    ///
    /// <para>
    /// Derived classes can override <see cref="RenderNode"/> to customize
    /// rendering behavior for specific HTML elements.
    /// </para>
    /// </remarks>
    public abstract class HtmlNode : IHtmlNode
    {

        private string _tagName;

        /// <summary>
        /// Gets the collection of HTML attributes assigned to the node.
        /// </summary>
        /// <remarks>
        /// Attribute values are rendered in the final HTML output as key-value pairs.
        /// </remarks>
        private Dictionary<string, string> Attributes { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets the collection of child HTML nodes contained within the current node.
        /// </summary>
        /// <remarks>
        /// Child nodes are rendered sequentially in the order they are added.
        /// </remarks>
        protected List<IHtmlNode> Children { get; } = new List<IHtmlNode>();


        /// <summary>
        /// Assigns or replaces an HTML attribute on the current node.
        /// </summary>
        /// <param name="key">
        /// The HTML attribute name.
        /// </param>
        /// <param name="value">
        /// The HTML attribute value.
        /// </param>
        /// <returns>
        /// The current <see cref="IHtmlNode"/> instance for fluent chaining.
        /// </returns>
        /// <remarks>
        /// Attribute names and values are validated through
        /// <see cref="HtmlValidator.ValidateAttribute(string, string)"/>.
        /// </remarks>
        public IHtmlNode Attr(string key, string value)
        {
            HtmlValidator.ValidateAttribute(key, value);
            Attributes[key] = value;
            return this;
        }

        /// <summary>
        /// Gets the HTML tag name used by the default renderer.
        /// </summary>
        protected virtual string TagName => _tagName ??= GetType().Name.ToLowerInvariant();


        /// <summary>
        /// Assigns an inline CSS style declaration to the current node.
        /// </summary>
        /// <param name="value">
        /// The CSS style declaration string.
        /// </param>
        /// <returns>
        /// The current <see cref="IHtmlNode"/> instance for fluent chaining.
        /// </returns>
        /// <remarks>
        /// The provided CSS is validated before assignment.
        /// The value is stored in the <c>style</c> attribute.
        /// </remarks>
        public IHtmlNode Style(string value)
        {
            HtmlValidator.ValidateStyle(value);
            Attributes["style"] = value;
            return this;
        }


        /// <summary>
        /// Assigns one or more CSS class names to the current node.
        /// </summary>
        /// <param name="value">
        /// A space-separated list of CSS class names.
        /// </param>
        /// <returns>
        /// The current <see cref="IHtmlNode"/> instance for fluent chaining.
        /// </returns>
        /// <remarks>
        /// Class names are validated before assignment.
        /// The value is stored in the <c>class</c> attribute.
        /// </remarks>
        public IHtmlNode Class(string value)
        {
            HtmlValidator.ValidateClassName(value);
            Attributes["class"] = value;
            return this;
        }

        /// <summary>
        /// Renders the current node and all child nodes into an HTML string.
        /// </summary>
        /// <returns>
        /// A string containing the rendered HTML markup.
        /// </returns>
        public string Render() => RenderNode();


        /// <summary>
        /// Renders the current HTML node including its attributes and child nodes.
        /// </summary>
        /// <remarks>
        /// The default implementation renders:
        ///
        /// <code>
        /// &lt;tag attributes&gt;children&lt;/tag&gt;
        /// </code>
        ///
        /// Derived classes may override this method to implement custom rendering
        /// behavior such as self-closing tags or specialized document rendering.
        /// </remarks>
        /// <returns>
        /// A string containing the rendered HTML element.
        /// </returns>
        protected virtual string RenderNode()
        {
            var sb = new StringBuilder();
            sb.Append('<').Append(TagName).Append(RenderAttributes()).Append('>');

            foreach (var child in Children)
                sb.Append(child.Render());

            sb.Append("</").Append(TagName).Append('>');
            return sb.ToString();
        }

        /// <summary>
        /// Renders all assigned HTML attributes into a formatted attribute string.
        /// </summary>
        /// <returns>
        /// A string containing all rendered HTML attributes,
        /// or an empty string if no attributes exist.
        /// </returns>
        /// <remarks>
        /// Attributes are rendered in the format:
        ///
        /// <code>
        /// key="value"
        /// </code>
        ///
        /// with leading spaces automatically included.
        /// </remarks>
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
