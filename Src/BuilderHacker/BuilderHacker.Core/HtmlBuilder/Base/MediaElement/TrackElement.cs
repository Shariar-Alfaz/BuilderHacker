using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.MediaElement
{
    /// <summary>
    /// Represents an HTML <c>&lt;track&gt;</c> element used to define timed text tracks
    /// for media elements such as <c>&lt;audio&gt;</c> and <c>&lt;video&gt;</c>.
    /// </summary>
    /// <remarks>
    /// The <see cref="TrackElement"/> provides support for accessibility features such as:
    /// <list type="bullet">
    /// <item><description>Subtitles</description></item>
    /// <item><description>Captions</description></item>
    /// <item><description>Descriptions</description></item>
    /// <item><description>Chapters</description></item>
    /// <item><description>Metadata tracks</description></item>
    /// </list>
    ///
    /// It is a self-closing HTML element and is typically used inside <c>&lt;audio&gt;</c> or
    /// <c>&lt;video&gt;</c> elements to enhance accessibility and user experience.
    /// </remarks>
    public sealed class TrackElement : Element, ITrackContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackElement"/> class with the specified track configuration.
        /// </summary>
        /// <param name="kind">
        /// The type of text track (e.g., <c>subtitles</c>, <c>captions</c>, <c>descriptions</c>, <c>chapters</c>, or <c>metadata</c>).
        /// </param>
        /// <param name="src">
        /// The URL of the track file (typically a .vtt file) that contains the timed text data.
        /// </param>
        /// <param name="srclang">
        /// The language of the track, specified as a BCP 47 language tag (e.g., <c>en</c>, <c>bn</c>).
        /// </param>
        /// <param name="label">
        /// A human-readable label for the track, shown to users in the media controls UI.
        /// </param>
        public TrackElement(
            string kind = "",
            string src = "",
            string srclang = "",
            string label = ""
        ) : base("track", true)
        {
            Attr("kind", kind);
            Attr("src", src);
            Attr("srclang", srclang);
            Attr("label", label);
        }
    }
}