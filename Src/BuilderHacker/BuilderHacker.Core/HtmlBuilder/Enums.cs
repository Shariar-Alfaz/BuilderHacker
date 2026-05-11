using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderHacker.Core.HtmlBuilder
{
    public enum Heading
    {
        H1,
        H2,
        H3,
        H4,
        H5,
        H6
    }

    public static class HeadingExtensions
    {
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
