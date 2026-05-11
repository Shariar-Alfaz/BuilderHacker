using System;
using System.Collections.Generic;
using System.Text;
using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base
{
    internal class TextNode:HtmlNode
    {
        private readonly string _text;

        internal TextNode(string text)
        {
            _text = text;
        }

        protected override string RenderNode()
        {
            return _text;
        }
    }
}
