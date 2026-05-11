namespace BuilderHacker.Abstraction.HtmlBuilder;

public interface IHtmlNode
{
    IHtmlNode Attr(string key, string value);
    IHtmlNode Style(string value);
    IHtmlNode Class(string value);
    string Render();
}