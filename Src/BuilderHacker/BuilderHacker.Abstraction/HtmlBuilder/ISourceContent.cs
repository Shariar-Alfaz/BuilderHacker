namespace BuilderHacker.Abstraction.HtmlBuilder
{
    /// <summary>
    /// Represents content that can include video, audio, or picture data.
    /// </summary>
    /// <remarks>This interface combines the capabilities of video, audio, and picture content, allowing
    /// implementations to provide one or more types of media. It is intended for use in scenarios where content may be
    /// of multiple media types and should be handled polymorphically.</remarks>
    public interface ISourceContent : IVideoContent, IAudioContent, IPictureContent
    {
    }
}
