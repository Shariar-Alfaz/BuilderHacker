using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.ListElement
{
    /// <summary>
    /// Represents an HTML <ul> (unordered list) element that contains a collection of list items.
    /// </summary>
    /// <remarks>Use the Ul class to create unordered lists in HTML markup by specifying one or more list item
    /// elements as children. This class is typically used when generating HTML dynamically or constructing document
    /// object models programmatically.</remarks>
    public class Ul : Element
    {
        public Ul(params IListElement[] childen) : base("ul", childen)
        {

        }
    }
}
