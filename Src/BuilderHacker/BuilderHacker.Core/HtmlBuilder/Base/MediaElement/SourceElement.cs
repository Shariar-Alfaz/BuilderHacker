using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.MediaElement
{
    /// <summary>
    /// Represents an HTML <c>&lt;source&gt;</c> element used to define alternative media resources
    /// for media elements such as <c>&lt;audio&gt;</c>, <c>&lt;video&gt;</c>, and <c>&lt;picture&gt;</c>.
    /// </summary>
    /// <remarks>
    /// The <see cref="SourceElement"/> allows the browser to choose the most appropriate media file
    /// based on supported formats. It is a self-closing element and cannot contain child nodes.
    /// </remarks>
    public sealed class SourceElement : Element, ISourceContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SourceElement"/> class with the specified media source and MIME type.
        /// </summary>
        /// <param name="src">
        /// The URL of the media resource. This value is assigned to the <c>src</c> attribute.
        /// </param>
        /// <param name="type">
        /// The MIME type of the media resource (for example: <c>video/mp4</c>, <c>audio/ogg</c>).
        /// This value is assigned to the <c>type</c> attribute.
        /// </param>
        public SourceElement(string src = "", string type = "")
            : base("source", true)
        {
            Attr("src", src);
            Attr("type", type);
        }
    }
}