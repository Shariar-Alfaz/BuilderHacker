using System;
using System.Text.RegularExpressions;

namespace BuilderHacker.Core.HtmlBuilder.Base
{
    /// <summary>
    /// Provides validation helpers for HTML class names, inline styles, and attribute keys
    /// used by HTML builder nodes to ensure safe and well-formed HTML generation.
    /// </summary>
    /// <remarks>
    /// This validator enforces basic structural rules for:
    /// <list type="bullet">
    /// <item><description>CSS class attribute syntax</description></item>
    /// <item><description>Inline style declaration format</description></item>
    /// <item><description>HTML attribute naming rules</description></item>
    /// </list>
    /// These validations help prevent malformed HTML output and reduce runtime rendering issues.
    /// </remarks>
    internal static class HtmlValidator
    {
        // class="btn primary"
        private static readonly Regex ClassRegex =
            new(@"^[a-zA-Z0-9_-]+(\s[a-zA-Z0-9_-]+)*$",
                RegexOptions.Compiled);

        // style="color:red; font-size:12px;"
        private static readonly Regex StyleRegex =
            new(@"^(\s*[a-zA-Z-]+\s*:\s*[^;]+;\s*)+$",
                RegexOptions.Compiled);

        // attribute key (safe HTML attribute name)
        private static readonly Regex AttributeKeyRegex =
            new(@"^[a-zA-Z_:][a-zA-Z0-9:._-]*$",
                RegexOptions.Compiled);

        #region Class Validation


        /// <summary>
        /// Validates a CSS class attribute string.
        /// </summary>
        /// <param name="className">
        /// A space-separated list of CSS class names.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the class name contains invalid characters or formatting.
        /// </exception>
        internal static void ValidateClassName(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
                return;

            if (!ClassRegex.IsMatch(className))
                throw new InvalidOperationException(
                    $"Invalid class attribute syntax: '{className}'");
        }

        #endregion

        #region Style Validation

        /// <summary>
        /// Validates an inline CSS style string.
        /// </summary>
        /// <param name="style">
        /// A CSS style declaration string (e.g., "color:red; font-size:12px;").
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the style string is not in a valid CSS declaration format.
        /// </exception>
        internal static void ValidateStyle(string style)
        {
            if (string.IsNullOrWhiteSpace(style))
                return;

            if (!StyleRegex.IsMatch(style))
                throw new InvalidOperationException(
                    $"Invalid style syntax: '{style}'");
        }

        #endregion

        #region Attribute Validation

        /// <summary>
        /// Validates an HTML attribute key-value pair.
        /// </summary>
        /// <param name="key">
        /// The HTML attribute name (e.g., "href", "id", "data-value").
        /// </param>
        /// <param name="value">
        /// The attribute value. Must not be null.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when the attribute key is null, empty, or invalid.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the attribute name does not match valid HTML naming rules.
        /// </exception>
        internal static void ValidateAttribute(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Attribute key cannot be empty");

            if (!AttributeKeyRegex.IsMatch(key))
                throw new InvalidOperationException(
                    $"Invalid attribute name: '{key}'");

            if (value == null)
                throw new ArgumentException(
                    $"Attribute value cannot be null for '{key}'");
        }

        #endregion
    }
}
