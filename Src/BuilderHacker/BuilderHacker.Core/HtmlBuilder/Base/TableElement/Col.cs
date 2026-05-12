using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML <c>&lt;col&gt;</c> element used as a column definition inside an HTML table.
    /// </summary>
    /// <remarks>
    /// The <see cref="Col"/> element is used within a <c>&lt;colgroup&gt;</c> to apply shared styling
    /// and attributes across entire table columns rather than individual cells.
    /// 
    /// This helps improve table styling consistency and reduces repetition when defining column-based layouts.
    /// </remarks>
    public sealed class Col : Element, ITableColumnDef
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Col"/> class representing an HTML <c>&lt;col&gt;</c> element.
        /// </summary>
        /// <remarks>
        /// This element is self-closing and does not support child nodes.
        /// It is typically used inside a <c>&lt;colgroup&gt;</c> element.
        /// </remarks>
        public Col() : base("col", true)
        {
        }
    }
}