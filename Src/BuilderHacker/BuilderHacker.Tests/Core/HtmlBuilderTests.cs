using BuilderHacker.Core.HtmlBuilder;
using BuilderHacker.Core.HtmlBuilder.Base.ListElement;
using HtmlAgilityPack;
using System;
using System.Linq;
using Xunit;

namespace BuilderHacker.Tests.Core
{
    /// <summary>
    /// Tests for HTML Builder - UI component factory and rendering.
    /// </summary>
    public class HtmlBuilderTests
    {
        #region Inline Elements

        [Fact]
        public void Span_WithChildren_RendersCorrectly()
        {
            var span = UI.Span(UI.TextNode("Hello"));
            var html = span.Render();

            Assert.NotNull(html);
            Assert.Contains("<span", html);
            Assert.Contains("Hello", html);
            Assert.Contains("</span>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void A_WithHrefAndTarget_RendersWithAttributes()
        {
            var link = UI.A("https://example.com", "_blank", UI.TextNode("Click here"));
            var html = link.Render();

            Assert.Contains("href=\"https://example.com\"", html);
            Assert.Contains("target=\"_blank\"", html);
            Assert.Contains("Click here", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void B_WithText_RendersAsStrongEmphasis()
        {
            var bold = UI.B(UI.TextNode("Bold text"));
            var html = bold.Render();

            Assert.Contains("<b", html);
            Assert.Contains("Bold text", html);
            Assert.Contains("</b>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void I_WithText_RendersAsItalic()
        {
            var italic = UI.I(UI.TextNode("Italic text"));
            var html = italic.Render();

            Assert.Contains("<i", html);
            Assert.Contains("Italic text", html);
            Assert.Contains("</i>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void U_WithText_RendersAsUnderline()
        {
            var underline = UI.U(UI.TextNode("Underlined text"));
            var html = underline.Render();

            Assert.Contains("<u", html);
            Assert.Contains("Underlined text", html);
            Assert.Contains("</u>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Br_RendersSelfClosingTag()
        {
            var br = UI.Br();
            var html = br.Render();

            Assert.Contains("<br", html);
            Assert.Contains("/>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Button_WithText_RendersCorrectly()
        {
            var button = UI.Button("Click me");
            var html = button.Render();

            Assert.Contains("<button", html);
            Assert.Contains("Click me", html);
            Assert.Contains("</button>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Strong_WithText_RendersCorrectly()
        {
            var strong = UI.Strong(UI.TextNode("Important"));
            var html = strong.Render();

            Assert.Contains("<strong", html);
            Assert.Contains("Important", html);
            Assert.Contains("</strong>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Sub_WithText_RendersAsSubscript()
        {
            var sub = UI.Sub(UI.TextNode("subscript"));
            var html = sub.Render();

            Assert.Contains("<sub", html);
            Assert.Contains("subscript", html);
            Assert.Contains("</sub>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Sup_WithText_RendersAsSuperscript()
        {
            var sup = UI.Sup(UI.TextNode("superscript"));
            var html = sup.Render();

            Assert.Contains("<sup", html);
            Assert.Contains("superscript", html);
            Assert.Contains("</sup>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Code_WithText_RendersCorrectly()
        {
            var code = UI.Code("var x = 5;");
            var html = code.Render();

            Assert.Contains("<code", html);
            Assert.Contains("var x = 5;", html);
            Assert.Contains("</code>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Label_WithText_RendersCorrectly()
        {
            var label = UI.Label("Username");
            var html = label.Render();

            Assert.Contains("<label", html);
            Assert.Contains("Username", html);
            Assert.Contains("</label>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Input_WithTypeAndValue_RendersWithAttributes()
        {
            var input = UI.Input("email", "user@example.com");
            var html = input.Render();

            Assert.Contains("<input", html);
            Assert.Contains("type=\"email\"", html);
            Assert.Contains("value=\"user@example.com\"", html);
            Assert.Contains("/>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Img_WithSrcAndAlt_RendersWithAttributes()
        {
            var img = UI.Img("image.jpg", "Description");
            var html = img.Render();

            Assert.Contains("<img", html);
            Assert.Contains("src=\"image.jpg\"", html);
            Assert.Contains("alt=\"Description\"", html);
            Assert.Contains("/>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void TextArea_WithText_RendersCorrectly()
        {
            var textArea = UI.TextArea("Default content");
            var html = textArea.Render();

            Assert.Contains("<textarea", html);
            Assert.Contains("Default content", html);
            Assert.Contains("</textarea>", html);
            html.ShouldBeValidHtml();
        }

        #endregion

        #region Block Elements

        [Fact]
        public void Div_WithChildren_RendersCorrectly()
        {
            var div = UI.Div(UI.TextNode("Content"));
            var html = div.Render();

            Assert.Contains("<div", html);
            Assert.Contains("Content", html);
            Assert.Contains("</div>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void P_WithText_RendersCorrectly()
        {
            var paragraph = UI.P(UI.TextNode("This is a paragraph."));
            var html = paragraph.Render();

            Assert.Contains("<p", html);
            Assert.Contains("This is a paragraph.", html);
            Assert.Contains("</p>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Section_WithChildren_RendersCorrectly()
        {
            var section = UI.Section(UI.TextNode("Section content"));
            var html = section.Render();

            Assert.Contains("<section", html);
            Assert.Contains("Section content", html);
            Assert.Contains("</section>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Address_WithText_RendersCorrectly()
        {
            var address = UI.Address(UI.TextNode("123 Main St"));
            var html = address.Render();

            Assert.Contains("<address", html);
            Assert.Contains("123 Main St", html);
            Assert.Contains("</address>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Article_WithChildren_RendersCorrectly()
        {
            var article = UI.Article(UI.P(UI.TextNode("Article content")));
            var html = article.Render();

            Assert.Contains("<article", html);
            Assert.Contains("<p", html);
            Assert.Contains("Article content", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Aside_WithChildren_RendersCorrectly()
        {
            var aside = UI.Aside(UI.TextNode("Sidebar"));
            var html = aside.Render();

            Assert.Contains("<aside", html);
            Assert.Contains("Sidebar", html);
            Assert.Contains("</aside>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Blockquote_WithCiteAndText_RendersWithAttribute()
        {
            var blockquote = UI.Blockquote("https://example.com", UI.TextNode("Quote"));
            var html = blockquote.Render();

            Assert.Contains("<blockquote", html);
            Assert.Contains("cite=\"https://example.com\"", html);
            Assert.Contains("Quote", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Canvas_WithDimensions_RendersWithAttributes()
        {
            var canvas = UI.Canvas("800", "600");
            var html = canvas.Render();

            Assert.Contains("<canvas", html);
            Assert.Contains("width=\"800\"", html);
            Assert.Contains("height=\"600\"", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Hr_RendersSelfClosingTag()
        {
            var hr = UI.Hr();
            var html = hr.Render();

            Assert.Contains("<hr", html);
            Assert.Contains("/>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Form_WithChildren_RendersCorrectly()
        {
            var form = UI.Form(UI.Input(), UI.Button("Submit"));
            var html = form.Render();

            Assert.Contains("<form", html);
            Assert.Contains("<input", html);
            Assert.Contains("<button", html);
            Assert.Contains("</form>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Nav_WithChildren_RendersCorrectly()
        {
            var nav = UI.Nav(UI.A("home.html", "", UI.TextNode("Home")));
            var html = nav.Render();

            Assert.Contains("<nav", html);
            Assert.Contains("href=\"home.html\"", html);
            Assert.Contains("Home", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Main_WithChildren_RendersCorrectly()
        {
            var main = UI.Main(UI.P(UI.TextNode("Main content")));
            var html = main.Render();

            Assert.Contains("<main", html);
            Assert.Contains("<p", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Footer_WithChildren_RendersCorrectly()
        {
            var footer = UI.Footer(UI.TextNode("© 2024"));
            var html = footer.Render();

            Assert.Contains("<footer", html);
            Assert.Contains("© 2024", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Header_WithChildren_RendersCorrectly()
        {
            var header = UI.Header(UI.TextNode("Header"));
            var html = header.Render();

            Assert.Contains("<header", html);
            Assert.Contains("Header", html);
            html.ShouldBeValidHtml();
        }

        #endregion

        #region List Elements

        [Fact]
        public void Ul_WithListItems_RendersCorrectly()
        {
            var ul = UI.Ul(
                new Li(UI.TextNode("Item 1")),
                new Li(UI.TextNode("Item 2"))
            );
            var html = ul.Render();

            Assert.Contains("<ul", html);
            Assert.Contains("<li", html);
            Assert.Contains("Item 1", html);
            Assert.Contains("Item 2", html);
            Assert.Contains("</ul>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Ol_WithListItems_RendersCorrectly()
        {
            var ol = UI.Ol(
                new Li(UI.TextNode("First")),
                new Li(UI.TextNode("Second"))
            );
            var html = ol.Render();

            Assert.Contains("<ol", html);
            Assert.Contains("<li", html);
            Assert.Contains("First", html);
            Assert.Contains("Second", html);
            Assert.Contains("</ol>", html);
            html.ShouldBeValidHtml();
        }

        #endregion

        #region Table Elements

        [Fact]
        public void FullTableCreation_RendersCompleteTableStructure()
        {
            var table = UI.Table(
                UI.TableCaption(UI.TextNode("Employee Directory")),
                UI.ColGroup(
                    UI.Col("width: 40%;"),
                    UI.Col("width: 20%;"),
                    UI.Col("width: 40%;")
                ),
                UI.THead(
                    UI.Tr(
                        UI.Th("Name"),
                        UI.Th("Age"),
                        UI.Th("Department")
                    )
                ),
                UI.TBody(
                    UI.Tr(
                        UI.Td("Alice"),
                        UI.Td("31"),
                        UI.Td("Engineering")
                    ),
                    UI.Tr(
                        UI.Td("Bob"),
                        UI.Td("28"),
                        UI.Td("Product")
                    )
                ),
                UI.TFoot(
                    UI.Tr(
                        UI.Td("Total"),
                        UI.Td("2"),
                        UI.Td("Employees")
                    )
                )
            );

            var html = table.Render();

            Assert.Contains("<table", html);
            Assert.Contains("<caption", html);
            Assert.Contains("Employee Directory", html);
            Assert.Contains("<colgroup", html);
            Assert.Contains("<col", html);
            Assert.Contains("<thead", html);
            Assert.Contains("<tbody", html);
            Assert.Contains("<tfoot", html);
            Assert.Contains("<tr", html);
            Assert.Contains("<th", html);
            Assert.Contains("<td", html);
            Assert.Contains("Alice", html);
            Assert.Contains("Bob", html);
            Assert.Contains("Engineering", html);
            Assert.Contains("Product", html);
            Assert.Contains("Total", html);
            Assert.Contains("Employees", html);
            Assert.Contains("</table>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Th_WithText_RendersCorrectly()
        {
            var th = UI.Th("Header");
            var html = th.Render();

            Assert.Contains("<th", html);
            Assert.Contains("Header", html);
            Assert.Contains("</th>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Td_WithText_RendersCorrectly()
        {
            var td = UI.Td("Data");
            var html = td.Render();

            Assert.Contains("<td", html);
            Assert.Contains("Data", html);
            Assert.Contains("</td>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void TableCaption_WithText_RendersCorrectly()
        {
            var caption = UI.TableCaption(UI.TextNode("Employee Table"));
            var html = caption.Render();

            Assert.Contains("<caption", html);
            Assert.Contains("Employee Table", html);
            html.ShouldBeValidHtml();
        }

        #endregion

        #region Heading Elements

        [Fact]
        public void Heading_H1_RendersCorrectly()
        {
            var h1 = UI.Heading(Heading.H1, UI.TextNode("Main Title"));
            var html = h1.Render();

            Assert.Contains("<h1", html);
            Assert.Contains("Main Title", html);
            Assert.Contains("</h1>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Heading_H2_RendersCorrectly()
        {
            var h2 = UI.Heading(Heading.H2, UI.TextNode("Subtitle"));
            var html = h2.Render();

            Assert.Contains("<h2", html);
            Assert.Contains("Subtitle", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Heading_H6_RendersCorrectly()
        {
            var h6 = UI.Heading(Heading.H6, UI.TextNode("Small Heading"));
            var html = h6.Render();

            Assert.Contains("<h6", html);
            Assert.Contains("Small Heading", html);
            html.ShouldBeValidHtml();
        }

        #endregion

        #region Head Elements

        [Fact]
        public void Title_WithText_RendersCorrectly()
        {
            var title = UI.Title("My Website");
            var html = title.Render();

            Assert.Contains("<title", html);
            Assert.Contains("My Website", html);
            Assert.Contains("</title>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Meta_WithNameAndContent_RendersWithAttributes()
        {
            var meta = UI.Meta("viewport", "width=device-width, initial-scale=1.0");
            var html = meta.Render();

            Assert.Contains("<meta", html);
            Assert.Contains("name=\"viewport\"", html);
            Assert.Contains("content=\"width=device-width, initial-scale=1.0\"", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Link_WithHrefAndRel_RendersWithAttributes()
        {
            var link = UI.Link("styles.css", "stylesheet");
            var html = link.Render();

            Assert.Contains("<link", html);
            Assert.Contains("href=\"styles.css\"", html);
            Assert.Contains("rel=\"stylesheet\"", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Style_WithContent_RendersCorrectly()
        {
            var style = UI.Style("body { margin: 0; }");
            var html = style.Render();

            Assert.Contains("<style", html);
            Assert.Contains("body { margin: 0; }", html);
            Assert.Contains("</style>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Script_WithContent_RendersCorrectly()
        {
            var script = UI.Script("console.log('hello');");
            var html = script.Render();

            Assert.Contains("<script", html);
            Assert.Contains("console.log('hello');", html);
            Assert.Contains("</script>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Script_WithSrcAttribute_RendersWithSrc()
        {
            var script = UI.Script("", "app.js");
            var html = script.Render();

            Assert.Contains("<script", html);
            Assert.Contains("src=\"app.js\"", html);
            html.ShouldBeValidHtml();
        }

        #endregion

        #region Custom and Document Elements

        [Fact]
        public void Custom_WithTag_RendersCorrectly()
        {
            var custom = UI.Custom("custom-element", false, UI.TextNode("Content"));
            var html = custom.Render();

            Assert.Contains("<custom-element", html);
            Assert.Contains("Content", html);
            Assert.Contains("</custom-element>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Custom_WithSelfClosing_RendersSelfClosing()
        {
            var custom = UI.Custom("my-component", true);
            var html = custom.Render();

            Assert.Contains("<my-component", html);
            Assert.Contains("/>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void HtmlDocument_WithHeadAndBody_RendersCorrectly()
        {
            var doc = UI.HtmlDocument(
                UI.Head(UI.Title("Test Page")),
                UI.Body(UI.P(UI.TextNode("Hello World")))
            );
            var html = doc.Render();

            Assert.Contains("<html", html);
            Assert.Contains("<head", html);
            Assert.Contains("<title", html);
            Assert.Contains("<body", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Body_WithChildren_RendersCorrectly()
        {
            var body = UI.Body(UI.P(UI.TextNode("Content")));
            var html = body.Render();

            Assert.Contains("<body", html);
            Assert.Contains("<p", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Head_WithChildren_RendersCorrectly()
        {
            var head = UI.Head(UI.Title("Test"));
            var html = head.Render();

            Assert.Contains("<head", html);
            Assert.Contains("<title", html);
            html.ShouldBeValidHtml();
        }

        #endregion

        #region Attributes and Styling

        [Fact]
        public void Attr_AddsCustomAttribute()
        {
            var div = UI.Div().Attr("data-id", "123");
            var html = div.Render();

            Assert.Contains("data-id=\"123\"", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Class_AddsClassAttribute()
        {
            var div = UI.Div().Class("container");
            var html = div.Render();

            Assert.Contains("class=\"container\"", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Style_AddsStyleAttribute()
        {
            var div = UI.Div().Style("color: red;");
            var html = div.Render();

            Assert.Contains("style=\"color: red;\"", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void ChainedAttributes_AllApply()
        {
            var div = UI.Div()
                .Class("box")
                .Style("background: blue;")
                .Attr("id", "main");
            var html = div.Render();

            Assert.Contains("class=\"box\"", html);
            Assert.Contains("style=\"background: blue;\"", html);
            Assert.Contains("id=\"main\"", html);
            html.ShouldBeValidHtml();
        }

        #endregion

        #region Nested Elements

        [Fact]
        public void NestedElements_RendersComplexStructure()
        {
            var structure = UI.Div(
                UI.Header(UI.Heading(Heading.H1, UI.TextNode("Welcome"))),
                UI.Main(
                    UI.Section(
                        UI.P(UI.TextNode("Introduction")),
                        UI.P(UI.TextNode("More content"))
                    )
                ),
                UI.Footer(UI.TextNode("© 2024"))
            );
            var html = structure.Render();

            Assert.Contains("<div", html);
            Assert.Contains("<header", html);
            Assert.Contains("<h1", html);
            Assert.Contains("<main", html);
            Assert.Contains("<section", html);
            Assert.Contains("<footer", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void DeepNesting_RendersWithoutErrors()
        {
            var html = UI.Div(
                UI.Div(
                    UI.Div(
                        UI.Div(
                            UI.TextNode("Deep content")
                        )
                    )
                )
            ).Render();

            Assert.NotNull(html);
            Assert.Contains("Deep content", html);
            html.ShouldBeValidHtml();
        }

        #endregion

        #region TextNode Tests

        [Fact]
        public void TextNode_WithString_RendersText()
        {
            var text = UI.TextNode("Simple text");
            var html = text.Render();

            Assert.Equal("Simple text", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void TextNode_WithSpecialCharacters_RendersCorrectly()
        {
            var text = UI.TextNode("Hello & goodbye");
            var html = text.Render();

            Assert.Equal("Hello & goodbye", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void TextNode_WithMultipleWords_RendersCorrectly()
        {
            var text = UI.TextNode("This is a long text with multiple words");
            var html = text.Render();

            Assert.Contains("multiple words", html);
            html.ShouldBeValidHtml();
        }

        #endregion

        #region Edge Cases

        [Fact]
        public void Element_WithNoChildren_RendersEmptyTag()
        {
            var div = UI.Div();
            var html = div.Render();

            Assert.Contains("<div", html);
            Assert.Contains("</div>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Element_WithNullChildren_DoesNotThrow()
        {
            var div = UI.Div(null);
            var html = div.Render();

            Assert.NotNull(html);
            Assert.Contains("<div", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Element_WithEmptyString_RendersCorrectly()
        {
            var text = UI.TextNode("");
            var html = text.Render();

            Assert.Equal("", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void MultipleAttributes_SameKey_LastValueWins()
        {
            var div = UI.Div()
                .Attr("id", "first")
                .Attr("id", "second");
            var html = div.Render();

            Assert.Contains("id=\"second\"", html);
            Assert.DoesNotContain("id=\"first\"", html);
            html.ShouldBeValidHtml();
        }

        #endregion

        #region New Inline and Media Elements

        [Fact]
        public void Em_Small_Mark_Del_Ins_S_RenderCorrectly()
        {
            var html = UI.Div(
                UI.Em(UI.TextNode("em")),
                UI.Small(UI.TextNode("small")),
                UI.Mark(UI.TextNode("mark")),
                UI.Del(UI.TextNode("del")),
                UI.Ins(UI.TextNode("ins")),
                UI.S(UI.TextNode("strike"))
            ).Render();

            Assert.Contains("<em", html);
            Assert.Contains("<small", html);
            Assert.Contains("<mark", html);
            Assert.Contains("<del", html);
            Assert.Contains("<ins", html);
            Assert.Contains("<s", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Abbr_Time_Dfn_Kbd_Samp_Var_Q_Cite_RenderCorrectly()
        {
            var html = UI.Div(
                UI.Abbr("Hyper Text Markup Language", UI.TextNode("HTML")),
                UI.Time("2026-01-01", UI.TextNode("Jan 1")),
                UI.Dfn(UI.TextNode("Term")),
                UI.Kbd(UI.TextNode("Ctrl+C")),
                UI.Samp(UI.TextNode("output")),
                UI.Var(UI.TextNode("x")),
                UI.Q("https://example.com", UI.TextNode("quote")),
                UI.Cite(UI.TextNode("source"))
            ).Render();

            Assert.Contains("<abbr", html);
            Assert.Contains("title=\"Hyper Text Markup Language\"", html);
            Assert.Contains("<time", html);
            Assert.Contains("datetime=\"2026-01-01\"", html);
            Assert.Contains("<dfn", html);
            Assert.Contains("<kbd", html);
            Assert.Contains("<samp", html);
            Assert.Contains("<var", html);
            Assert.Contains("<q", html);
            Assert.Contains("cite=\"https://example.com\"", html);
            Assert.Contains("<cite", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Wbr_And_Pre_RenderCorrectly()
        {
            var html = UI.Div(UI.Pre(UI.TextNode("line1\nline2")), UI.Wbr()).Render();

            Assert.Contains("<pre", html);
            Assert.Contains("line1", html);
            Assert.Contains("<wbr", html);
            Assert.Contains("/>", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Figure_FigCaption_Details_Summary_Dialog_Search_RenderCorrectly()
        {
            var html = UI.Div(
                UI.Figure(UI.Img("a.png", "a"), UI.FigCaption(UI.TextNode("caption"))),
                UI.Details(UI.Summary(UI.TextNode("sum")), UI.P(UI.TextNode("details"))),
                UI.Dialog(UI.TextNode("dialog")),
                UI.Search(UI.Input("search", "term"))
            ).Render();

            Assert.Contains("<figure", html);
            Assert.Contains("<figcaption", html);
            Assert.Contains("<details", html);
            Assert.Contains("<summary", html);
            Assert.Contains("<dialog", html);
            Assert.Contains("<search", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Audio_Video_Source_Track_Picture_RenderCorrectly()
        {
            var html = UI.Div(
                UI.Audio("audio.mp3", UI.Track("subtitles", "audio.vtt", "en", "English")),
                UI.Video("video.mp4", UI.Source("video.webm", "video/webm")),
                UI.Picture(UI.Source("image.webp", "image/webp"), UI.Img("image.png", "img"))
            ).Render();

            Assert.Contains("<audio", html);
            Assert.Contains("src=\"audio.mp3\"", html);
            Assert.Contains("<track", html);
            Assert.Contains("<video", html);
            Assert.Contains("src=\"video.mp4\"", html);
            Assert.Contains("<source", html);
            Assert.Contains("<picture", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void IFrame_Embed_Object_Param_RenderCorrectly()
        {
            var html = UI.Div(
                UI.IFrame("https://example.com", "Example"),
                UI.Embed("file.pdf", "application/pdf"),
                UI.Object("movie.swf", "application/x-shockwave-flash", UI.Param("quality", "high"))
            ).Render();

            Assert.Contains("<iframe", html);
            Assert.Contains("title=\"Example\"", html);
            Assert.Contains("<embed", html);
            Assert.Contains("type=\"application/pdf\"", html);
            Assert.Contains("<object", html);
            Assert.Contains("<param", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Data_Meter_Progress_RenderCorrectly()
        {
            var html = UI.Div(
                UI.Data("42", UI.TextNode("answer")),
                UI.Meter("7", "0", "10", UI.TextNode("7 of 10")),
                UI.Progress("60", "100", UI.TextNode("60%"))
            ).Render();

            Assert.Contains("<data", html);
            Assert.Contains("value=\"42\"", html);
            Assert.Contains("<meter", html);
            Assert.Contains("max=\"10\"", html);
            Assert.Contains("<progress", html);
            Assert.Contains("max=\"100\"", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Dl_Dt_Dd_RenderCorrectly()
        {
            var html = UI.Dl(
                UI.Dt(UI.TextNode("Name")),
                UI.Dd(UI.TextNode("BuilderHacker"))
            ).Render();

            Assert.Contains("<dl", html);
            Assert.Contains("<dt", html);
            Assert.Contains("<dd", html);
            Assert.Contains("BuilderHacker", html);
            html.ShouldBeValidHtml();
        }

        [Fact]
        public void Fieldset_Legend_Select_Option_OptGroup_Datalist_Output_RenderCorrectly()
        {
            var html = UI.Form(
                UI.Fieldset(
                    UI.Legend(UI.TextNode("Profile")),
                    UI.Select(
                        UI.OptGroup("Group A", UI.Option("1", UI.TextNode("One"))),
                        UI.Option("2", UI.TextNode("Two"))
                    ),
                    UI.Datalist(
                        UI.Option("alpha", UI.TextNode("alpha")),
                        UI.Option("beta", UI.TextNode("beta"))
                    ),
                    UI.Output(UI.TextNode("ok"))
                )
            ).Render();


            html.ShouldBeValidHtml();
            Assert.Contains("<fieldset", html);
            Assert.Contains("<legend", html);
            Assert.Contains("<select", html);
            Assert.Contains("<option", html);
            Assert.Contains("<optgroup", html);
            Assert.Contains("label=\"Group A\"", html);
            Assert.Contains("<datalist", html);
            Assert.Contains("<output", html);

        }
        #endregion

        #region Valid HtmlTest


        [Fact]
        public void Check_Validation()
        {
            var page = UI.HtmlDocument(
                    UI.Head(
                        UI.Title("Test")),
                                        UI.Body(
                            UI.Div(
                                UI.TextNode("Hello")
                                ).Style("color:black;").Attr("test", "true")
                            )

                    ).Render();
            var doc = new HtmlDocument();
            doc.LoadHtml(page);
            Assert.False(doc.ParseErrors.Any(), $"HTML Parse Errors: {string.Join(", ", doc.ParseErrors.Select(x => x.Reason))}");
        }

        #endregion
    }

    public static class HtmlValidationExtensions
    {
        public static void ShouldBeValidHtml(this string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var errors = string.Join(
                Environment.NewLine,
                doc.ParseErrors.Select(x => x.Reason)
            );

            Assert.False(doc.ParseErrors.Any(),
                $"HTML Parse Errors:{Environment.NewLine}{errors}");
        }
    }
}
