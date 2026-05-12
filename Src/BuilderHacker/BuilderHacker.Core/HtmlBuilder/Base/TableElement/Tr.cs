using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML &lt;tr&gt; table row element containing header or data cells.
    /// </summary>
    public sealed class Tr : Element, ITableRow
    {
        /// <summary>
        /// Initializes a new instance of the Tr class with the specified table cells.
        /// </summary>
        /// <param name="children">
        /// An array of table cell elements (&lt;th&gt; or &lt;td&gt;) that make up the row.
        /// </param>
        public Tr(params IThOrTd[] children)
            : base("tr", children)
        {
        }
    }
}