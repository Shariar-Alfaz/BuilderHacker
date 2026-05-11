using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.MediaElement
{
    /// <summary>
    /// Represents an HTML <source> element, typically used to specify multiple media resources for media elements such
    /// as <audio> and <video>.
    /// </summary>
    /// <remarks>Use this class to define alternative media sources for media elements. The 'src' attribute
    /// specifies the URL of the media file, and the 'type' attribute specifies the media type (MIME type) of the
    /// resource. This enables browsers to select the most appropriate source based on their capabilities.</remarks>
    public sealed class SourceElement : Element, ISourceContent
    {
        /// <summary>
        /// Initializes a new instance of the SourceElement class with the specified source URL and media type.
        /// </summary>
        /// <param name="src">The URL of the media resource to be used as the source. If not specified, the source attribute will be
        /// empty.</param>
        /// <param name="type">The MIME type of the media resource. If not specified, the type attribute will be empty.</param>
        public SourceElement(string src = "", string type = "") : base("source", true)
        {
            Attr("src", src);
            Attr("type", type);
        }
    }
}
