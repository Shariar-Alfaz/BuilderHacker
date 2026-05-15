using System;
using System.Collections.Generic;

namespace BuilderHacker.Core.HtmlBuilder
{
    /// <summary>
    /// Represents supported HTML heading levels.
    /// </summary>
    public enum Heading
    {
        /// <summary>
        /// Heading level 1, typically used for the main title of a document or section. It is the most important heading and is often displayed in the largest font size.
        /// </summary>
        H1,
        /// <summary>
        /// Heading level 2, used for subheadings under the main title. It is less important than H1 but still significant in the document structure. It is usually displayed in a smaller font size than H1.
        /// </summary>
        H2,
        /// <summary>
        /// Heading level 3, used for sub-subheadings. It is less important than H2 but still significant in the document structure.
        /// </summary>
        H3,
        /// <summary>
        /// Heading level 4, used for further subdivisions of content. It is less important than H3 and is typically displayed in a smaller font size.
        /// </summary>
        H4,
        /// <summary>
        /// Heading level 5, used for even finer subdivisions of content. It is less important than H4 and is typically displayed in a smaller font size.
        /// </summary>
        H5,
        /// <summary>
        /// Heading level 6, used for the least important headings. It is typically displayed in a smaller font size than H5.
        /// </summary>
        H6
    }

    /// <summary>
    /// Provides extension helpers for heading-related conversions.
    /// </summary>
    public static class HeadingExtensions
    {
        /// <summary>
        /// Converts a Heading enum value to its corresponding HTML tag string (e.g., H1 -> "h1"). This is useful for generating HTML elements based on the heading level.
        /// </summary>
        /// <param name="heading"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string ToTag(this Heading heading)
        {
            Dictionary<Heading, string> headingTags = new Dictionary<Heading, string>
            {
                { Heading.H1, "h1" },
                { Heading.H2, "h2" },
                { Heading.H3, "h3" },
                { Heading.H4, "h4" },
                { Heading.H5, "h5" },
                { Heading.H6, "h6" }
            };

            if (headingTags.TryGetValue(heading, out var tag))
            {
                return tag;
            }

            throw new ArgumentOutOfRangeException(nameof(heading), heading, null);
        }
    }
}
