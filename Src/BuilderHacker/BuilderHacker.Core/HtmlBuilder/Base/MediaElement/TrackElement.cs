using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.MediaElement
{
    /// <summary>
    /// Represents an HTML <track> element used to specify timed text tracks for media elements such as <audio> and
    /// <video>.
    /// </summary>
    /// <remarks>The TrackElement class enables the addition of subtitles, captions, or other timed text
    /// tracks to media content in a web-based user interface. It corresponds to the standard HTML <track> element and
    /// supports attributes such as kind, src, srclang, and label to define the type and source of the track. This class
    /// is typically used in conjunction with media elements to enhance accessibility and provide alternative content
    /// for users.</remarks>
    public sealed class TrackElement : Element, ITrackContent
    {
        /// <summary>
        /// Initializes a new instance of the TrackElement class with the specified track attributes for use in media
        /// elements.
        /// </summary>
        /// <param name="kind">The type of text track, such as 'subtitles', 'captions', 'descriptions', 'chapters', or 'metadata'.</param>
        /// <param name="src">The URL of the track file to be used as the source for the text track.</param>
        /// <param name="srclang">The language of the track text data, specified as a valid BCP 47 language tag.</param>
        /// <param name="label">A user-readable title for the track, used by the browser when listing available text tracks.</param>
        public TrackElement(string kind = "", string src = "", string srclang = "", string label = "") : base("track", true)
        {
            Attr("kind", kind);
            Attr("src", src);
            Attr("srclang", srclang);
            Attr("label", label);
        }
    }
}
