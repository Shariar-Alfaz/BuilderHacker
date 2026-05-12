using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.MediaElement
{
    /// <summary>
    /// Represents an HTML <c>&lt;img&gt;</c> element used to display an image in a document.
    /// </summary>
    /// <remarks>
    /// The <see cref="ImgElement"/> is a self-closing HTML element and does not support child nodes.
    /// It is commonly used within <c>&lt;picture&gt;</c>, <c>&lt;video&gt;</c>, or general content layouts
    /// to render images.
    /// </remarks>
    public sealed class ImgElement : Element, IPictureContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImgElement"/> class with the specified image source and alternative text.
        /// </summary>
        /// <param name="src">
        /// The URL of the image resource. This value is assigned to the <c>src</c> attribute and should not be null or empty.
        /// </param>
        /// <param name="alt">
        /// The alternative text for the image. This is used for accessibility and is displayed if the image fails to load.
        /// If not provided, an empty string is used.
        /// </param>
        public ImgElement(string src, string alt = "")
            : base("img", true)
        {
            Attr("src", src);
            Attr("alt", alt);
        }
    }
}
