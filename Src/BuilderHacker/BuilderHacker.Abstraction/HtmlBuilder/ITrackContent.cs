namespace BuilderHacker.Abstraction.HtmlBuilder
{
    /// <summary>
    /// Represents media content that includes both video and audio tracks.
    /// </summary>
    /// <remarks>This interface combines the functionality of both video and audio content, allowing
    /// implementations to provide access to media that contains synchronized audio and video streams. It is typically
    /// used in scenarios where operations or processing need to be performed on media files or streams that include
    /// both types of content.</remarks>
    public interface ITrackContent : IVideoContent, IAudioContent
    {
    }
}
