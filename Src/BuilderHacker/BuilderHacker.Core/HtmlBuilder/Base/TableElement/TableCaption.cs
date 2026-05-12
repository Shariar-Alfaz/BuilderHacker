using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    /// <summary>
    /// Represents an HTML <c>&lt;caption&gt;</c> element used to define a title or description for a table.
    /// </summary>
    /// <remarks>
    /// The <see cref="TableCaption"/> element provides a semantic label for an HTML table.
    /// It is typically placed as the first child of a <c>&lt;table&gt;</c> element.
    /// 
    /// This improves accessibility and helps users and assistive technologies understand
    /// the purpose of the table.
    /// </remarks>
    public sealed class TableCaption : Element, IBaseTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableCaption"/> class with the specified child nodes.
        /// </summary>
        /// <param name="children">
        /// An array of <see cref="IHtmlNode"/> elements that represent the content of the table caption.
        /// This can include text nodes or other inline HTML elements.
        /// </param>
        /// <remarks>
        /// The order of the provided child nodes determines their rendering order inside the caption.
        /// </remarks>
        public TableCaption(params IHtmlNode[] children)
            : base("caption", children)
        {
        }
    }
}