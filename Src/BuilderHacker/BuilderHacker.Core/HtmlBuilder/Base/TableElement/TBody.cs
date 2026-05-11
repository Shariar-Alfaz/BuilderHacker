using System;
using System.Collections.Generic;
using System.Text;
using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    public sealed class TBody: Element, IBaseTable
    {
        public TBody(params Tr[] children) : base("tbody", children)
        {
        }
    }
}
