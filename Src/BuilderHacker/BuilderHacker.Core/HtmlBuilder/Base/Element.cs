using System.Text;
using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base;

public class Element : HtmlNode
{
    private readonly string _tag;

    private readonly bool _isSelfClosing;

    public Element(string tag, bool isSelfClosing = false, params IHtmlNode[] children)
    {
        _tag = tag;
        _isSelfClosing = isSelfClosing;

        if (children != null)
            Children.AddRange(children);
    }

    public Element(string tag, params IHtmlNode[] children)
    {
        _tag = tag;
        _isSelfClosing = false;

        if (children != null)
            Children.AddRange(children);
    }

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