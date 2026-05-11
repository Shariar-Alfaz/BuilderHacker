using System;
using System.Collections.Generic;
using System.Text;
using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    public sealed class TableCaption: Element, IBaseTable
    {
        public TableCaption(params IHtmlNode[] children) : base("caption", children)
        {
        }
    }
}
