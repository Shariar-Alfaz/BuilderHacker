using System;
using System.Collections.Generic;
using System.Text;
using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.ListElement
{
    public class Li: Element, IListElement
    {
        public Li(params IHtmlNode[] children) : base("li", children)
        {
        }
    }
}
