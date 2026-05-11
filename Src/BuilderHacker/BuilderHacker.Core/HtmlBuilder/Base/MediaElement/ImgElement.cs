using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.MediaElement
{
    /// <summary>
    /// Represents an HTML <img> element that displays an image within a document.
    /// </summary>
    /// <remarks>Use this class to create and configure image elements by specifying the image source and
    /// alternative text. The <img> element is self-closing and does not contain child elements.</remarks>
    public sealed class ImgElement : Element, IPictureContent
    {
        /// <summary>
        /// Initializes a new instance of the ImgElement class with the specified image source and alternate text.
        /// </summary>
        /// <param name="src">The URL of the image to display in the element. This value is assigned to the 'src' attribute and cannot be
        /// null or empty.</param>
        /// <param name="alt">The alternate text for the image, used for accessibility and displayed if the image cannot be loaded. This
        /// value is assigned to the 'alt' attribute. If not specified, an empty string is used.</param>
        public ImgElement(string src, string alt = "") : base("img", true)
        {
            Attr("src", src);
            Attr("alt", alt);
        }
    }
}
