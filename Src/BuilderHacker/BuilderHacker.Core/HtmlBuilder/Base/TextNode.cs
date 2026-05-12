namespace BuilderHacker.Core.HtmlBuilder.Base
{
    /// <summary>
    /// Represents a plain text HTML node that renders raw text content without any HTML tags.
    /// </summary>
    /// <remarks>
    /// <see cref="TextNode"/> is used as a leaf node in the HTML tree structure.
    /// It does not support attributes or child nodes and is rendered directly as text
    /// inside parent HTML elements.
    /// </remarks>
    internal class TextNode : HtmlNode
    {
        /// <summary>
        /// The raw text content of this node.
        /// </summary>
        private readonly string _text;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextNode"/> class with the specified text content.
        /// </summary>
        /// <param name="text">
        /// The text content to render. This can be any string value, including empty or whitespace text.
        /// </param>
        internal TextNode(string text)
        {
            _text = text;
        }

        /// <summary>
        /// Renders the text node as raw HTML text content.
        /// </summary>
        /// <remarks>
        /// Unlike standard HTML elements, this node does not wrap the content in tags.
        /// It is returned exactly as provided in the constructor.
        /// </remarks>
        /// <returns>
        /// The raw text content of this node.
        /// </returns>
        protected override string RenderNode()
        {
            return _text;
        }
    }
}
