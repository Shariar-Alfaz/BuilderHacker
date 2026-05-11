using System;
using System.Collections.Generic;
using System.Text;
using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base.ListElement
{
    public class Ul: Element
    {
        public Ul(params IListElement[] childen):base("ul", childen)
        {
            
        }
    }
}
