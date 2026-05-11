using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML <caption> element for a table.
    /// </summary>
    public sealed class TableCaption : Element, IBaseTable
    {
        public TableCaption(params IHtmlNode[] children) : base("caption", children)
        {
        }
    }
}
