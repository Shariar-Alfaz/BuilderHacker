using System;
using System.Collections.Generic;
using System.Text;
using BuilderHacker.Abstraction.HtmlBuilder;
using BuilderHacker.Core.HtmlBuilder.Base;

namespace BuilderHacker.Core.HtmlBuilder
{
    public static class UI
    {
        #region Inline

        public static Element Span(params HtmlNode[] children) => new Element("span", children);

        public static Element A(params HtmlNode[] children) => new Element("a", children);

        public static Element B(params HtmlNode[] children) => new Element("b", children);

        public static Element I(params HtmlNode[] children) => new Element("i", children);

        public static Element U(params HtmlNode[] children) => new Element("u", children);

        public static Element Br() => new Element("br");

        public static Element Button(string text) => new Element("button", new TextNode(text));

        public static Element Strong(params HtmlNode[] children) => new Element("strong", children);

        public static Element Sub(params HtmlNode[] children) => new Element("sub", children);

        public static Element Sup(params HtmlNode[] children) => new Element("sup", children);

        public static Element Code(string code) => new Element("code", new TextNode(code));

        public static Element TextNode(string text) => new Element("span", new TextNode(text));

        #endregion

        #region Block

        public static Element Div(params HtmlNode[] children) => new Element("div", children);

        #endregion

        public static Element Custom(string tag, params HtmlNode[] children) => new Element(tag, children);

    }

}
