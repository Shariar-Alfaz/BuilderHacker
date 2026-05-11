using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML <caption> element for a table.
    /// </summary>
    public sealed class TableCaption : Element, IBaseTable
    {
        /// <summary>
        /// Initializes a new instance of the TableCaption class with the specified child HTML nodes.
        /// </summary>
        /// <remarks>Use this constructor to create a table caption element and specify its content by
        /// providing one or more child nodes. The order of the child nodes determines their order within the
        /// caption.</remarks>
        /// <param name="children">An array of IHtmlNode elements that represent the child nodes to include within the table caption. Can be
        /// empty to create an empty caption.</param>
        public TableCaption(params IHtmlNode[] children) : base("caption", children)
        {
        }
    }
}
