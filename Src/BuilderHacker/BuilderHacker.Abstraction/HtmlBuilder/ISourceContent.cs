namespace BuilderHacker.Abstraction.HtmlBuilder
{
    /// <summary>
    /// Represents media source content that can be used within HTML media elements.
    /// </summary>
    /// <remarks>
    /// This interface combines support for video, audio, and picture media content, allowing implementations
    /// to be used polymorphically across different HTML media elements.
    ///
    /// It is typically used for elements such as &lt;source&gt; that can belong to:
    /// - &lt;audio&gt;
    /// - &lt;video&gt;
    /// - &lt;picture&gt;
    /// </remarks>
    public interface ISourceContent : IVideoContent, IAudioContent, IPictureContent
    {
    }
}