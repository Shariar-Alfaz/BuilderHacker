using System.Text;
using System.Xml.Linq;
using BuilderHacker.Abstraction.HtmlBuilder;

public class Element : HtmlNode
{
    private readonly string _tag;

    public Element(string tag, params HtmlNode[] children)
    {
        _tag = tag;

        if (children != null)
            Children.AddRange(children);
    }

    protected override string RenderNode()
    {
        var sb = new StringBuilder();

        sb.Append($"<{_tag}{RenderAttributes()}>");

        foreach (var child in Children)
            sb.Append(child.Render());

        sb.Append($"</{_tag}>");

        return sb.ToString();
    }
}