using System;
using System.Collections.Generic;
using System.Text;
using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    public sealed class Colgroup: Element, IBaseTable
    {
        public Colgroup(params ITableColumnDef[] children) : base("colgroup", children)
        {
            
        }
    }
}
