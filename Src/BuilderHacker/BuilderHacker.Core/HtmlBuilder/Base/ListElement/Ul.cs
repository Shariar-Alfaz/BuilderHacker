using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.ListElement
{
    /// <summary>
    /// Represents an HTML <c>&lt;ul&gt;</c> (unordered list) element that contains a collection of list items.
    /// </summary>
    /// <remarks>
    /// The <see cref="Ul"/> class is used to programmatically build unordered HTML lists.
    /// It accepts only elements that implement <see cref="IListElement"/>, ensuring type-safe
    /// composition of list structures such as <c>&lt;li&gt;</c> elements.
    /// </remarks>
    public class Ul : Element
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ul"/> class with the specified list item children.
        /// </summary>
        /// <param name="children">
        /// An array of <see cref="IListElement"/> items that will be rendered as children of the unordered list.
        /// Typically, these are <c>&lt;li&gt;</c> elements.
        /// </param>
        public Ul(params IListElement[] children) : base("ul", children)
        {

        }
    }
}
