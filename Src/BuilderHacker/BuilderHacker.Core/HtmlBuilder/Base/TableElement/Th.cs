using System;
using System.Collections.Generic;
using System.Text;
using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    public sealed class Th: Element, IThOrTd
    {
        public Th(params IHtmlNode[] children) : base("th", children)
        {
            
        }
    }
}
