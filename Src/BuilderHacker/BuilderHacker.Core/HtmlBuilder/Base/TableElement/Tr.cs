using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML <tr> table row element containing header/data cells.
    /// </summary>
    public sealed class Tr : Element, ITableRow
    {
        public Tr(params IThOrTd[] children) : base("tr", children)
        {
        }
    }
}
