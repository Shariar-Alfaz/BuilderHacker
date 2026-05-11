using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML <th> table header cell element.
    /// </summary>
    public sealed class Th : Element, IThOrTd
    {
        public Th(params IHtmlNode[] children) : base("th", children)
        {

        }
    }
}
