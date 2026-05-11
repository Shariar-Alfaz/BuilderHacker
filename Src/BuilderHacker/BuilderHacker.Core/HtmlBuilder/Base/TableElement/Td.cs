using System;
using System.Collections.Generic;
using System.Text;
using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    public sealed class Td: Element, IThOrTd
    {
        public Td(params IHtmlNode[] children) : base("td", children)
        {
        }
    }
}
