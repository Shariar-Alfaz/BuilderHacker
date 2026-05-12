using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML <c>&lt;tbody&gt;</c> element used to group the body content of a table.
    /// </summary>
    /// <remarks>
    /// The <see cref="TBody"/> element is used to contain the main data rows of an HTML table.
    /// It groups one or more <c>&lt;tr&gt;</c> (table row) elements and helps separate table structure
    /// into logical sections such as header, body, and footer.
    /// </remarks>
    public sealed class TBody : Element, IBaseTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TBody"/> class with the specified table rows.
        /// </summary>
        /// <param name="children">
        /// An array of <see cref="ITableRow"/> elements representing table rows (<c>&lt;tr&gt;</c>)
        /// that will be included inside the table body section.
        /// </param>
        /// <remarks>
        /// This constructor ensures that only valid table row elements are allowed inside the tbody section,
        /// maintaining correct HTML table structure.
        /// </remarks>
        public TBody(params ITableRow[] children)
            : base("tbody", children)
        {
        }
    }
}