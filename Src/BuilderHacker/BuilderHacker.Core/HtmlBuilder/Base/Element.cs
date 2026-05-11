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
    /// Initializes a new instance of the Element class with the specified tag name, self-closing flag, and optional
    /// child nodes.
    /// </summary>
    /// <param name="tag">The name of the HTML tag to represent. Cannot be null or empty.</param>
    /// <param name="isSelfClosing">true to create a self-closing element; otherwise, false.</param>
    /// <param name="children">An optional array of child nodes to add to the element. Can be empty.</param>
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
    /// <param name="tag">The name of the HTML tag to represent. Cannot be null.</param>
    /// <param name="children">An optional array of child nodes to add to the element. Can be empty.</param>
    public Element(string tag, params IHtmlNode[] children)
    {
        _tag = tag;
        _isSelfClosing = false;

        if (children != null)
            Children.AddRange(children);
    }

    /// <summary>
    /// Generates the HTML markup for the current node and its child nodes as a string.
    /// </summary>
    /// <remarks>If the node is self-closing, the output will use self-closing tag syntax. Otherwise, the
    /// output will include both opening and closing tags, with the rendered content of all child nodes inserted between
    /// them.</remarks>
    /// <returns>A string containing the rendered HTML markup for this node, including its attributes and any child nodes.</returns>
    protected override string RenderNode()
    {
        var sb = new StringBuilder();
        sb.Append(!_isSelfClosing ? $"<{_tag} {RenderAttributes()}>" : $"<{_tag} {RenderAttributes()} />");

        foreach (var child in Children)
            sb.Append(child.Render());

        if (!_isSelfClosing)
            sb.Append($"</{_tag}>");

        return sb.ToString();
    }
}