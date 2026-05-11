using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML <colgroup> element that groups column definitions in a table.
    /// </summary>
    public sealed class Colgroup : Element, IBaseTable
    {
        public Colgroup(params ITableColumnDef[] children) : base("colgroup", children)
        {

        }
    }
}
