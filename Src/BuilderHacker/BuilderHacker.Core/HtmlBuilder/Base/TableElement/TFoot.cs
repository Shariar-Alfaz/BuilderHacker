using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML <tfoot> table footer section element.
    /// </summary>
    public sealed class TFoot : Element, IBaseTable
    {
        public TFoot(params ITableRow[] children) : base("tfoot", children)
        {
        }
    }
}
