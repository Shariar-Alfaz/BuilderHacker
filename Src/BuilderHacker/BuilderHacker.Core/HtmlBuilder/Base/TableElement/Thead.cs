using System;
using System.Collections.Generic;
using System.Text;
using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    public sealed class Thead: Element, IBaseTable
    {
        public Thead(params Tr[] children) : base("thead", children)
        { }
    }
}
