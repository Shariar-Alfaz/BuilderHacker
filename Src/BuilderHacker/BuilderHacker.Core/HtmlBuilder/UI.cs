using System;
using System.Collections.Generic;
using System.Text;
using BuilderHacker.Abstraction.HtmlBuilder;
using BuilderHacker.Core.HtmlBuilder.Base;
using BuilderHacker.Core.HtmlBuilder.Base.ListElement;
using BuilderHacker.Core.HtmlBuilder.Base.TableElement;

namespace BuilderHacker.Core.HtmlBuilder
{
    public static class UI
    {
        #region Inline

        public static IHtmlNode Span(params IHtmlNode[] children) => new Element("span", children);

        public static IHtmlNode A(string href = "", string target = "", params IHtmlNode[] children)
            => new Element("a", children)
                .Attr("href", href)
                .Attr("target", target);

        public static IHtmlNode B(params IHtmlNode[] children) => new Element("b", children);

        public static IHtmlNode I(params IHtmlNode[] children) => new Element("i", children);
        public static IHtmlNode U(params IHtmlNode[] children) => new Element("u", children);

        public static IHtmlNode Br() => new Element("br", true);

        public static IHtmlNode Button(string text) => new Element("button", new TextNode(text));
        public static IHtmlNode Strong(params IHtmlNode[] children) => new Element("strong", children);

        public static IHtmlNode Sub(params IHtmlNode[] children) => new Element("sub", children);

        public static IHtmlNode Sup(params IHtmlNode[] children) => new Element("sup", children);
        public static IHtmlNode Code(string code) => new Element("code", new TextNode(code));

        public static IHtmlNode Label(string label) => new Element("label", new TextNode(label));



        public static IHtmlNode Input(string type = "text", string value = "")
            => new Element("input", true)
                .Attr("type", type)
                .Attr("value", value);

        public static IHtmlNode Img(string src, string alt = "")
            => new Element("img", true)
                .Attr("src", src)
                .Attr("alt", alt);

        public static IHtmlNode TextArea(string text = "")
            => new Element("textarea", new TextNode(text));

        #endregion

        #region Block

        public static IHtmlNode Div(params IHtmlNode[] children) => new Element("div", children);

        #region Table

        public static IHtmlNode Table(params IBaseTable[] children)
            => new Table(children);

        public static IHtmlNode THead(params Tr[] children)
            => new Thead(children);


        public static IHtmlNode TableCaption(params IHtmlNode[] children) => new TableCaption(children);

        public static IHtmlNode Th(string text)
            => new Th(new TextNode(text));

        public static HtmlNode Th(params IHtmlNode[] children)
            => new Th(children);

        public static IHtmlNode Td(string text)
            => new Td(new TextNode(text));

        public static IHtmlNode Td(params IHtmlNode[] children)
            => new Td(children);

        public static IHtmlNode Tr(params IThOrTd[] children)
             => new Tr(children);

        public static IHtmlNode TBody(params Tr[] children)
             => new TBody(children);

        public static IHtmlNode TFoot(params Tr[] children)
             => new TFoot(children);
        public static IHtmlNode ColGroup(params ITableColumnDef[] children)
             => new Colgroup(children);

        public static IHtmlNode Col(string style = "")
             => new Col().Style(style);
        #endregion

        public static IHtmlNode Section(params IHtmlNode[] children)
            => new Element("section", children);

        public static IHtmlNode P(params IHtmlNode[] children)
            => new Element("p", children);

        public static IHtmlNode Address(params IHtmlNode[] children)
            => new Element("address", children);

        public static IHtmlNode Article(params IHtmlNode[] children)
            => new Element("article", children);

        public static IHtmlNode Aside(params IHtmlNode[] children)
            => new Element("aside", children);

        public static IHtmlNode Blockquote(string cite = "", params IHtmlNode[] children)
            => new Element("blockquote", children).Attr("cite", cite);

        public static IHtmlNode Canvas(string width = "", string height = "", params IHtmlNode[] children)
            => new Element("canvas", children)
                .Attr("width", width)
                .Attr("height", height);

        public static IHtmlNode Hr()
            => new Element("hr", true);

        public static IHtmlNode Heading(Heading heading, params IHtmlNode[] children)
            => new Element(heading.ToTag(), children);

        public static IHtmlNode Form(params IHtmlNode[] children)
            => new Element("form", children);

        public static IHtmlNode Ul(params IListElement[] children)
            => new Ul(children);

        public static IHtmlNode Ol(params IListElement[] children)
            => new Ol(children);

        public static IHtmlNode Li(params IHtmlNode[] children)
            => new Li(children);

        public static IHtmlNode Nav(params IHtmlNode[] children)
            => new Element("nav", children);

        public static IHtmlNode Main(params IHtmlNode[] children)
            => new Element("main", children);

        public static IHtmlNode Footer(params IHtmlNode[] children)
            => new Element("footer", children);

        public static IHtmlNode Header(params IHtmlNode[] children)
            => new Element("header", children);

        #endregion
        public static IHtmlNode Custom(
                string tag,
                bool isSelfClosing = false,
                params IHtmlNode[] children
            )
                => new Element(tag, isSelfClosing, children);

        public static IHtmlNode HtmlDocument(params IHtmlNode[] children)
            => new HtmlDocument(children);

        public static IHtmlNode Body(params IHtmlNode[] children)
            => new Element("body", children);

        public static IHtmlNode Head(params IHtmlNode[] children)
            => new Element("head", children);

        public static IHtmlNode Title(string text)
            => new Element("title", new TextNode(text));

        public static IHtmlNode Meta(string name, string content)
            => new Element("meta", true).Attr("name", name).Attr("content", content);

        public static IHtmlNode Link(string href, string rel = "stylesheet")
            => new Element("link", true).Attr("href", href).Attr("rel", rel);

        public static IHtmlNode Style(string style) => new Element("style", new TextNode(style));

        public static IHtmlNode Script(string script, string src = "") 
            => new Element("script", new TextNode(script)).Attr("src", src);

        public static IHtmlNode TextNode(string  text) => new TextNode(text);

    }

}