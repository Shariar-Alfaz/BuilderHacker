using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML <c>&lt;table&gt;</c> element used to structure tabular data.
    /// </summary>
    /// <remarks>
    /// The <see cref="Table"/> element serves as the root container for all table-related components,
    /// including rows, headers, footers, column groups, and captions.
    /// 
    /// It enforces a structured composition model using <see cref="IBaseTable"/> to ensure only valid
    /// table-related elements are added as children.
    /// </remarks>
    public sealed class Table : Element, IBaseTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class with the specified table children.
        /// </summary>
        /// <param name="children">
        /// An array of <see cref="IBaseTable"/> elements representing valid table components such as:
        /// <c>&lt;thead&gt;</c>, <c>&lt;tbody&gt;</c>, <c>&lt;tfoot&gt;</c>, <c>&lt;tr&gt;</c>, and <c>&lt;colgroup&gt;</c>.
        /// </param>
        /// <remarks>
        /// This constructor ensures type-safe construction of HTML tables by restricting children
        /// to valid table structure elements only.
        /// </remarks>
        public Table(params IBaseTable[] children)
            : base("table", children)
        {
        }
    }
}