namespace BuilderHacker.Abstraction.HtmlBuilder
{
    /// <summary>
    /// Defines methods for building and rendering an HTML node with attributes, styles, and CSS classes.
    /// </summary>
    /// <remarks>Implementations of this interface allow for fluent construction of HTML elements by chaining
    /// attribute, style, and class assignments, and rendering the final HTML markup as a string.</remarks>
    public interface IHtmlNode
    {
        /// <summary>
        /// Sets an attribute on the current HTML node with the specified key and value.
        /// </summary>
        /// <remarks>If an attribute with the specified key already exists, its value is updated. If the
        /// value is null, the attribute is removed from the node.</remarks>
        /// <param name="key">The name of the attribute to set. Cannot be null or empty.</param>
        /// <param name="value">The value to assign to the attribute. If null, the attribute will be removed.</param>
        /// <returns>The current HTML node instance with the updated attribute.</returns>
        IHtmlNode Attr(string key, string value);

        /// <summary>
        /// Sets the value of the style attribute for the HTML element represented by this node.
        /// </summary>
        /// <param name="value">The CSS style string to assign to the element's style attribute. This value should be a valid CSS
        /// declaration block, such as "color: red; font-size: 12px;". If null or empty, the style attribute will be
        /// removed.</param>
        /// <returns>The current <see cref="IHtmlNode"/> instance with the updated style attribute.</returns>
        IHtmlNode Style(string value);

        /// <summary>
        /// Adds a CSS class to the HTML node.
        /// </summary>
        /// <remarks>If the class already exists on the node, it will not be added again.</remarks>
        /// <param name="value">The CSS class name to add. Cannot be null or empty.</param>
        /// <returns>The current <see cref="IHtmlNode"/> instance with the specified class added.</returns>
        IHtmlNode Class(string value);

        /// <summary>
        /// Generates and returns a string representation of the current object or data for display or output purposes.
        /// </summary>
        /// <returns>A string containing the rendered output. The format and content of the string depend on the implementation.</returns>
        string Render();
    }
}
