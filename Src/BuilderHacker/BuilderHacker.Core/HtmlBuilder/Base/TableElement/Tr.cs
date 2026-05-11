using System;
using System.Collections.Generic;
using System.Text;
using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    public sealed class Tr: Element, IBaseTable
    {
        public Tr(params IThOrTd[] children) : base("tr", children)
        {
        }
    }
}
