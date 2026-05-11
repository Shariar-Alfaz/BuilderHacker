using System;
using System.Collections.Generic;
using System.Text;
using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    public sealed class Table: Element, IBaseTable
    {
        public Table(params IBaseTable[] children) : base("table", children) { }
        
    }
}
