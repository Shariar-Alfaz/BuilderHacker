using System;
using System.Collections.Generic;
using System.Text;
using BuilderHacker.Abstraction.HtmlBuilder;

namespace BuilderHacker.Core.HtmlBuilder.Base
{
    public class TextNode:HtmlNode
    {
        private readonly string _text;

        public TextNode(string text)
        {
            _text = text;
        }

        protected override string RenderNode()
        {
            return _text;
        }
    }
}
