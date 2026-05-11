using System;
using System.Collections.Generic;
using System.Text;
using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.TableElement
{
    public sealed class TFoot: Element, IBaseTable
    {
        public TFoot(params Tr[] children) : base("tfoot", children)
        {
        }
    }
}
