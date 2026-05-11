namespace BuilderHacker.Core.HtmlBuilder.Base
{
    /// <summary>
    /// Represents a plain text node in the HTML tree.
    /// </summary>
    internal class TextNode : HtmlNode
    {
        private readonly string _text;

        internal TextNode(string text)
        {
            _text = text;
        }

        protected override string RenderNode()
        {
            return _text;
        }
    }
}
