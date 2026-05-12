using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML &lt;thead&gt; table header section element.
    /// </summary>
    public sealed class Thead : Element, IBaseTable
    {
        /// <summary>
        /// Initializes a new instance of the Thead class with the specified table row elements.
        /// </summary>
        /// <param name="children">
        /// An array of table row elements (&lt;tr&gt;) that define the header section of the table.
        /// </param>
        public Thead(params ITableRow[] children)
            : base("thead", children)
        {
        }
    }
}