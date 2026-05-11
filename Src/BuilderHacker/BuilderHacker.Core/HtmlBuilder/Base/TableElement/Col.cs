using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents a column definition element within an HTML table, corresponding to the <col> tag.
    /// </summary>
    /// <remarks>Use the Col class to specify common attributes for one or more columns in an HTML table. This
    /// element is typically used to apply styles or attributes to entire columns rather than individual
    /// cells.</remarks>
    public sealed class Col : Element, ITableColumnDef
    {
        /// <summary>
        /// Initializes a new instance of the Col class representing an HTML <col> element.
        /// </summary>
        public Col() : base("col", true)
        {
        }
    }
}
