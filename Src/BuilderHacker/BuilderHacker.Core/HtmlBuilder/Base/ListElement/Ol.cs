using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.ListElement
{
    /// <summary>
    /// Represents an HTML ordered list (ol) element that contains a collection of list items.
    /// </summary>
    /// <remarks>Use this class to create an ordered list in an HTML document by specifying one or more list
    /// item elements as children. The order of the child elements determines the sequence of items in the rendered
    /// list.</remarks>
    public class Ol : Element
    {
        /// <summary>
        /// Initializes a new instance of the Ol class with the specified child elements.
        /// </summary>
        /// <param name="childen">An array of elements to be included as children of the ordered list. Cannot be null.</param>
        public Ol(params IListElement[] childen) : base("ol", childen)
        {

        }
    }
}
