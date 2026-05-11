using System;
using System.Collections.Generic;
using System.Text;
using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.ListElement
{
    public class Ol: Element
    {
        public Ol(params IListElement[] childen):base("ol", childen)
        {
            
        }
    }
}
