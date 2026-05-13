using BuilderHacker.Abstraction.HtmlBuilder;
using System.Text;

namespace BuilderHacker.Core.HtmlBuilder.Base;

/// <summary>
/// Represents a generic HTML element node with a configurable tag name, optional self-closing behavior,
/// attributes, and child nodes.
/// </summary>
public class Element : HtmlNode
{
    private readonly string _tag;
    private readonly bool _isSelfClosing;

    /// <summary>
    /// Gets the HTML tag name for this element.
    /// </summary>
    protected override string TagName => _tag;

    /// <summary>
    /// Initializes a new instance of the Element class with the specified tag name, self-closing flag, and optional child nodes.
    /// </summary>
    /// <param name="tag">The name of the HTML tag to represent (e.g., div, span). Cannot be null or empty.</param>
    /// <param name="isSelfClosing">true to create a self-closing element; otherwise false.</param>
    /// <param name="children">Optional child nodes to add to the element.</param>
    public Element(string tag, bool isSelfClosing = false, params IHtmlNode[] children)
    {
        _tag = tag;
        _isSelfClosing = isSelfClosing;

        if (children != null)
            Children.AddRange(children);
    }

    /// <summary>
    /// Initializes a new instance of the Element class with the specified tag name and optional child nodes.
    /// </summary>
    /// <param name="tag">The name of the HTML tag to represent (e.g., div, span).</param>
    /// <param name="children">Optional child nodes to add to the element.</param>
    public Element(string tag, params IHtmlNode[] children)
    {
        _tag = tag;
        _isSelfClosing = false;

        if (children != null)
            Children.AddRange(children);
    }

    /// <summary>
    /// Generates the HTML markup for the current node and its child nodes.
    /// </summary>
    /// <remarks>
    /// Self-closing elements are rendered using <tag /> syntax.
    /// Normal elements include opening and closing tags with child content in between.
    /// </remarks>
    /// <returns>A string containing the rendered HTML markup.</returns>
    protected override string RenderNode()
    {
        var sb = new StringBuilder();
        var tagName = TagName;

        var attrs = RenderAttributes();

        if (_isSelfClosing)
        {
            sb.Append('<').Append(tagName).Append(attrs).Append(" />");
            return sb.ToString();
        }

        sb.Append('<').Append(tagName).Append(attrs).Append('>');

        foreach (var child in Children)
            sb.Append(child.Render());

        sb.Append("</").Append(tagName).Append('>');

        return sb.ToString();
    }
}
