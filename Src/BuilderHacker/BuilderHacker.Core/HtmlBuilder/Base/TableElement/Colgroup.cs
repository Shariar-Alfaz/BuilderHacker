using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML <c>&lt;colgroup&gt;</c> element used to group one or more column definitions in a table.
    /// </summary>
    /// <remarks>
    /// The <see cref="Colgroup"/> element is used to apply shared properties to multiple table columns.
    /// It helps improve table styling, structure, and maintainability by allowing column-level configuration
    /// instead of defining styles on individual cells.
    /// </remarks>
    public sealed class Colgroup : Element, IBaseTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Colgroup"/> class with the specified column definitions.
        /// </summary>
        /// <param name="children">
        /// An array of <see cref="ITableColumnDef"/> elements representing column definitions (<c>&lt;col&gt;</c> elements)
        /// to be included in the column group.
        /// </param>
        /// <remarks>
        /// This constructor is used to define structured column layouts in HTML tables.
        /// The <c>&lt;colgroup&gt;</c> element must contain one or more <c>&lt;col&gt;</c> elements.
        /// </remarks>
        public Colgroup(params ITableColumnDef[] children)
            : base("colgroup", children)
        {
        }
    }
}