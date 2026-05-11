using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML <colgroup> element that groups column definitions in a table.
    /// </summary>
    public sealed class Colgroup : Element, IBaseTable
    {
        /// <summary>
        /// Initializes a new instance of the Colgroup class with the specified table column definitions as child
        /// elements.
        /// </summary>
        /// <remarks>Use this constructor to create a colgroup element containing one or more column
        /// definitions for an HTML table. The colgroup element is used to group columns within a table for formatting
        /// purposes.</remarks>
        /// <param name="children">An array of table column definitions to include as children of the colgroup element. Cannot be null.</param>
        public Colgroup(params ITableColumnDef[] children) : base("colgroup", children)
        {

        }
    }
}
