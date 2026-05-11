using System;
using System.Collections.Generic;
using System.Text;
using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    public sealed class Col : Element, ITableColumnDef
    {
        public Col() : base("col", true)
        {
        }
    }
}
