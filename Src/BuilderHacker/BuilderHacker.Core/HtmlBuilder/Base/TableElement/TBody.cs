using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML <tbody> table body section element.
    /// </summary>
    public sealed class TBody : Element, IBaseTable
    {
        /// <summary>
        /// Initializes a new instance of the TBody class with the specified table row elements as its children.
        /// </summary>
        /// <param name="children">An array of table row elements to include as children of the tbody element. Cannot be null.</param>
        public TBody(params ITableRow[] children) : base("tbody", children)
        {
        }
    }
}
