using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML &lt;tfoot&gt; table footer section element.
    /// </summary>
    public sealed class TFoot : Element, IBaseTable
    {
        /// <summary>
        /// Initializes a new instance of the TFoot class with the specified table row elements.
        /// </summary>
        /// <param name="children">
        /// An array of table row elements (&lt;tr&gt;) that define the footer content of the table.
        /// </param>
        public TFoot(params ITableRow[] children)
            : base("tfoot", children)
        {
        }
    }
}