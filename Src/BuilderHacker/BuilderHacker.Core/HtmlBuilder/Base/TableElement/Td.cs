using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML <td> table data cell element.
    /// </summary>
    public sealed class Td : Element, IThOrTd
    {
        public Td(params IHtmlNode[] children) : base("td", children)
        {
        }
    }
}
