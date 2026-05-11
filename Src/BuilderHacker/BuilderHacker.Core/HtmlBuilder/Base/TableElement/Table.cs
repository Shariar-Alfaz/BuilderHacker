using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML <table> element containing table-related child nodes.
    /// </summary>
    public sealed class Table : Element, IBaseTable
    {
        /// <summary>
        /// Initializes a new instance of the Table class with the specified child elements.
        /// </summary>
        /// <param name="children">An array of child elements to include within the table. Each element must implement the IBaseTable
        /// interface. Cannot be null.</param>
        public Table(params IBaseTable[] children) : base("table", children) { }

    }
}
