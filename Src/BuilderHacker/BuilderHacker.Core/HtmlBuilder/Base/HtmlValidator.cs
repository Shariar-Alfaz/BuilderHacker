using System;
using System.Text.RegularExpressions;

namespace BuilderHacker.Core.HtmlBuilder.Base
{
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
