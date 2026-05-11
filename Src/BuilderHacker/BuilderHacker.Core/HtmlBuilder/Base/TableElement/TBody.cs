using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML <tbody> table body section element.
    /// </summary>
    public sealed class TBody : Element, IBaseTable
    {
        public TBody(params ITableRow[] children) : base("tbody", children)
        {
        }
    }
}
