using BuilderHacker.Abstraction.HtmlBuilder;
using BuilderHacker.Core.HtmlBuilder.Base;
using BuilderHacker.Core.HtmlBuilder.Base.ListElement;
using BuilderHacker.Core.HtmlBuilder.Base.MediaElement;
using BuilderHacker.Core.HtmlBuilder.Base.TableElement;

namespace BuilderHacker.Core.HtmlBuilder
{
    /// <summary>
    /// Provides static helper methods for creating HTML element nodes and structures in a programmatic, type-safe
    /// manner.
    /// </summary>
    /// <remarks>The UI class offers a comprehensive set of methods for generating HTML elements, including
    /// inline, block, table, and form elements, as well as document structure components. These methods simplify the
    /// construction of HTML trees by allowing developers to compose elements using strongly-typed nodes and attributes.
    /// This approach helps prevent common markup errors and enables dynamic HTML generation in .NET applications. All
    /// methods return objects implementing IHtmlNode or related interfaces, which can be further composed or rendered
    /// as needed.</remarks>
    public static class UI
    {
        #region Inline

        /// <summary>
        /// Creates a new HTML &lt;span&gt; element containing the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the &lt;span&gt; element. Can be empty to create an empty &lt;span&gt;.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;span&gt; element with the specified children.</returns>
        public static IHtmlNode Span(params IHtmlNode[] children) => new Element("span", children);

        /// <summary>
        /// Creates a new HTML anchor ('a') element with the specified href, target, and child nodes.
        /// </summary>
        /// <param name="href">The URL to assign to the anchor's href attribute. If empty, the href attribute is omitted.</param>
        /// <param name="target">The value for the anchor's target attribute, specifying where to open the linked document. If empty, the
        /// target attribute is omitted.</param>
        /// <param name="children">An array of child nodes to include within the anchor element. Can be empty to create an anchor with no
        /// content.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed anchor element with the specified attributes and
        /// children.</returns>
        public static IHtmlNode A(string href = "", string target = "", params IHtmlNode[] children)
            => new Element("a", children)
                .Attr("href", href)
                .Attr("target", target);

        /// <summary>
        /// Creates a new HTML &lt;b&gt; element with the specified child nodes.
        /// </summary>
        /// <remarks>Use this method to programmatically generate bold text content in an HTML document by
        /// wrapping child nodes within a &lt;b&gt; element.</remarks>
        /// <param name="children">An array of <see cref="IHtmlNode"/> objects to be added as children of the &lt;b&gt; element. Can be empty to
        /// create an empty &lt;b&gt; element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;b&gt; element containing the specified child nodes.</returns>
        public static IHtmlNode B(params IHtmlNode[] children) => new Element("b", children);

        /// <summary>
        /// Creates a new HTML &lt;i&gt; element with the specified child nodes.
        /// </summary>
        /// <remarks>The &lt;i&gt; element is typically used to render text in an alternate voice or style, such
        /// as italics. This method provides a convenient way to programmatically construct such elements with optional
        /// child content.</remarks>
        /// <param name="children">An array of child nodes to be added as the content of the &lt;i&gt; element. Can be empty to create an empty
        /// element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;i&gt; element containing the specified child nodes.</returns>
        public static IHtmlNode I(params IHtmlNode[] children) => new Element("i", children);

        /// <summary>
        /// Creates a new HTML &lt;u&gt; element with the specified child nodes.
        /// </summary>
        /// <remarks>The &lt;u&gt; element represents text that should be stylistically different from normal
        /// text, typically rendered with an underline. This method is useful for programmatically building HTML
        /// content.</remarks>
        /// <param name="children">An array of child nodes to be added as the content of the &lt;u&gt; element. Can be empty to create an empty
        /// element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;u&gt; element containing the specified children.</returns>
        public static IHtmlNode U(params IHtmlNode[] children) => new Element("u", children);

        /// <summary>
        /// Creates a new HTML &lt;em&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to be added as the content of the &lt;em&gt; element. Can be empty.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;em&gt; element containing the specified children.</returns>
        public static IHtmlNode Em(params IHtmlNode[] children) => new Element("em", children);

        /// <summary>
        /// Creates a new HTML &lt;small&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to be added as the content of the &lt;small&gt; element. Can be empty.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;small&gt; HTML element containing the specified
        /// children.</returns>
        public static IHtmlNode Small(params IHtmlNode[] children) => new Element("small", children);

        /// <summary>
        /// Creates a &lt;mark&gt; HTML element that highlights text, containing the specified child nodes.
        /// </summary>
        /// <remarks>Use the &lt;mark&gt; element to indicate text that should be highlighted or marked for
        /// reference. This method simplifies the creation of semantic HTML for text emphasis.</remarks>
        /// <param name="children">An array of child nodes to be included within the &lt;mark&gt; element. Can be empty to create an empty element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;mark&gt; element with the specified children.</returns>
        public static IHtmlNode Mark(params IHtmlNode[] children) => new Element("mark", children);

        /// <summary>
        /// Creates a new HTML &lt;del&gt; element with the specified child nodes.
        /// </summary>
        /// <remarks>The &lt;del&gt; element is used to represent content that has been deleted from a document.
        /// This method provides a convenient way to programmatically construct such elements with any number of child
        /// nodes.</remarks>
        /// <param name="children">An array of child nodes to be added as the content of the &lt;del&gt; element. Can be empty to create an empty
        /// element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;del&gt; element containing the specified children.</returns>
        public static IHtmlNode Del(params IHtmlNode[] children) => new Element("del", children);

        /// <summary>
        /// Creates a new HTML &lt;ins&gt; element with the specified child nodes.
        /// </summary>
        /// <remarks>The &lt;ins&gt; element represents inserted text in an HTML document. This method is
        /// typically used to programmatically build HTML structures.</remarks>
        /// <param name="children">An array of child nodes to be added as the content of the &lt;ins&gt; element. Can be empty.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;ins&gt; element containing the specified children.</returns>
        public static IHtmlNode Ins(params IHtmlNode[] children) => new Element("ins", children);

        /// <summary>
        /// Creates an HTML &lt;s&gt; element that renders its child nodes with strikethrough formatting.
        /// </summary>
        /// <remarks>The &lt;s&gt; element is typically used to indicate text that is no longer accurate or
        /// relevant. Child nodes are rendered in the order provided.</remarks>
        /// <param name="children">An array of child nodes to be included within the &lt;s&gt; element. Can be empty to create an empty element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;s&gt; element containing the specified child nodes.</returns>
        public static IHtmlNode S(params IHtmlNode[] children) => new Element("s", children);

        /// <summary>
        /// Creates an HTML &lt;abbr&gt; element with the specified title attribute and child nodes.
        /// </summary>
        /// <remarks>Use this method to semantically mark up abbreviations or acronyms in HTML, improving
        /// accessibility and providing additional context for users and assistive technologies.</remarks>
        /// <param name="title">The value for the 'title' attribute, providing an expanded description of the abbreviation. If empty, the
        /// attribute is omitted.</param>
        /// <param name="children">The child nodes to include within the &lt;abbr&gt; element. Can be text or other HTML nodes.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;abbr&gt; element with the specified attributes and
        /// children.</returns>
        public static IHtmlNode Abbr(string title = "", params IHtmlNode[] children)
            => new Element("abbr", children).Attr("title", title);

        /// <summary>
        /// Creates a new HTML &lt;time&gt; element with the specified datetime attribute and child nodes.
        /// </summary>
        /// <remarks>Use this method to generate semantic HTML for representing dates and times, which can
        /// improve accessibility and machine-readability of your markup.</remarks>
        /// <param name="datetime">The value to assign to the 'datetime' attribute of the &lt;time&gt; element. This should be a valid date or time
        /// string, or an empty string if the attribute should be omitted.</param>
        /// <param name="children">An array of child nodes to be added as the content of the &lt;time&gt; element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;time&gt; element with the specified attributes and
        /// children.</returns>
        public static IHtmlNode Time(string datetime = "", params IHtmlNode[] children)
            => new Element("time", children).Attr("datetime", datetime);

        /// <summary>
        /// Creates a 'dfn' HTML element that represents the defining instance of a term, containing the specified child
        /// nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to be included within the 'dfn' element. Can be empty to create an empty 'dfn'
        /// element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed 'dfn' element with the specified children.</returns>
        public static IHtmlNode Dfn(params IHtmlNode[] children) => new Element("dfn", children);

        /// <summary>
        /// Creates a &lt;kbd&gt; HTML element that represents user input, containing the specified child nodes.
        /// </summary>
        /// <remarks>The &lt;kbd&gt; element is typically used to denote keyboard input in HTML documents. Child
        /// nodes can include text or other HTML elements as needed.</remarks>
        /// <param name="children">An array of child nodes to be included within the &lt;kbd&gt; element. Can be empty to create an empty element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;kbd&gt; element with the specified children.</returns>
        public static IHtmlNode Kbd(params IHtmlNode[] children) => new Element("kbd", children);

        /// <summary>
        /// Creates a &lt;samp&gt; HTML element that represents sample output from a program or computing system, containing
        /// the specified child nodes.
        /// </summary>
        /// <remarks>The &lt;samp&gt; element is typically used to display output from programs or commands in a
        /// way that distinguishes it from surrounding text. Child nodes can include text or other HTML elements as
        /// appropriate.</remarks>
        /// <param name="children">An array of child nodes to be included within the &lt;samp&gt; element. Can be empty to create an empty &lt;samp&gt;
        /// element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;samp&gt; element with the specified children.</returns>
        public static IHtmlNode Samp(params IHtmlNode[] children) => new Element("samp", children);

        /// <summary>
        /// Creates a new HTML &lt;var&gt; element with the specified child nodes.
        /// </summary>
        /// <remarks>The &lt;var&gt; element is typically used to represent a variable in mathematical expressions
        /// or programming code within HTML content.</remarks>
        /// <param name="children">An array of child nodes to be added as the content of the &lt;var&gt; element. Can be empty.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;var&gt; element containing the specified children.</returns>
        public static IHtmlNode Var(params IHtmlNode[] children) => new Element("var", children);

        /// <summary>
        /// Creates a new HTML &lt;q&gt; element with the specified cite attribute and child nodes.
        /// </summary>
        /// <param name="cite">The value of the cite attribute, specifying the source of the quotation. If empty, the cite attribute is
        /// omitted.</param>
        /// <param name="children">An array of child nodes to be added as the content of the &lt;q&gt; element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;q&gt; element with the specified attributes and
        /// children.</returns>
        public static IHtmlNode Q(string cite = "", params IHtmlNode[] children)
            => new Element("q", children).Attr("cite", cite);

        /// <summary>
        /// Creates a new HTML &lt;cite&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to be added as the content of the &lt;cite&gt; element. Can be empty to create an empty
        /// element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;cite&gt; element containing the specified child nodes.</returns>
        public static IHtmlNode Cite(params IHtmlNode[] children) => new Element("cite", children);

        /// <summary>
        /// Creates a new HTML &lt;br&gt; (line break) element node.
        /// </summary>
        /// <remarks>Use this method to insert a line break in generated HTML content. The returned node is
        /// self-closing and does not contain any child elements.</remarks>
        /// <returns>An <see cref="IHtmlNode"/> representing a self-closing &lt;br&gt; element.</returns>
        public static IHtmlNode Br() => new Element("br", true);

        /// <summary>
        /// Creates a new HTML &lt;wbr&gt; (word break opportunity) element node.
        /// </summary>
        /// <remarks>The &lt;wbr&gt; element is used to indicate a position within text where the browser may
        /// optionally break a line. This method is useful when generating HTML content that requires explicit word break
        /// opportunities.</remarks>
        /// <returns>An <see cref="IHtmlNode"/> representing a self-closing &lt;wbr&gt; element.</returns>
        public static IHtmlNode Wbr() => new Element("wbr", true);

        /// <summary>
        /// Creates a new HTML &lt;button&gt; element with the specified text content.
        /// </summary>
        /// <param name="text">The text to display inside the &lt;button&gt; element. Cannot be null.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the created &lt;button&gt; element containing the specified text.</returns>
        public static IHtmlNode Button(string text) => new Element("button", new TextNode(text));

        /// <summary>
        /// Creates a new HTML &lt;strong&gt; element that contains the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to be included within the &lt;strong&gt; element. Can be empty to create an empty element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;strong&gt; element with the specified children.</returns>
        public static IHtmlNode Strong(params IHtmlNode[] children) => new Element("strong", children);


        /// <summary>
        /// Creates a new HTML &lt;sub&gt; element with the specified child nodes.
        /// </summary>
        /// <remarks>Use this method to generate subscript text in HTML documents by providing the desired
        /// child nodes. The resulting element can be further composed or rendered as part of a larger HTML
        /// structure.</remarks>
        /// <param name="children">An array of child nodes to be added as the content of the &lt;sub&gt; element. Can be empty to create an empty
        /// element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;sub&gt; element containing the specified children.</returns>
        public static IHtmlNode Sub(params IHtmlNode[] children) => new Element("sub", children);


        /// <summary>
        /// Creates a new HTML &lt;sup&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to be added as the content of the &lt;sup&gt; element. Can be empty to create an empty
        /// element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;sup&gt; element containing the specified children.</returns>
        public static IHtmlNode Sup(params IHtmlNode[] children) => new Element("sup", children);

        /// <summary>
        /// Creates an HTML &lt;code&gt; element containing the specified code text.
        /// </summary>
        /// <param name="code">The code text to be enclosed within the &lt;code&gt; element. Cannot be null.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing a &lt;code&gt; element with the provided code text as its content.</returns>
        public static IHtmlNode Code(string code) => new Element("code", new TextNode(code));

        /// <summary>
        /// Creates a new &lt;pre&gt; HTML element that contains the specified child nodes.
        /// </summary>
        /// <remarks>The &lt;pre&gt; element preserves both whitespace and line breaks in its content, making it
        /// suitable for displaying preformatted text.</remarks>
        /// <param name="children">An array of child nodes to include within the &lt;pre&gt; element. Can be empty to create an empty &lt;pre&gt; element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;pre&gt; element with the specified children.</returns>
        public static IHtmlNode Pre(params IHtmlNode[] children) => new Element("pre", children);


        /// <summary>
        /// Creates a new HTML &lt;label&gt; element with the specified text content.
        /// </summary>
        /// <param name="label">The text to display within the &lt;label&gt; element. Cannot be null.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;label&gt; element containing the specified text.</returns>
        public static IHtmlNode Label(string label) => new Element("label", new TextNode(label));

        /// <summary>
        /// Creates a new HTML &lt;input&gt; element with the specified type and value attributes.
        /// </summary>
        /// <param name="type">The value to assign to the input element's 'type' attribute. Defaults to "text" if not specified.</param>
        /// <param name="value">The value to assign to the input element's 'value' attribute. Defaults to an empty string if not specified.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;input&gt; element with the specified attributes.</returns>
        public static IHtmlNode Input(string type = "text", string value = "")
            => new Element("input", true)
                .Attr("type", type)
                .Attr("value", value);

        /// <summary>
        /// Creates a new HTML &lt;img&gt; element with the specified source and alternate text.
        /// </summary>
        /// <param name="src">The URL of the image to display in the &lt;img&gt; element. This value is assigned to the 'src' attribute and
        /// cannot be null.</param>
        /// <param name="alt">The alternate text for the image, used for accessibility and displayed if the image cannot be loaded. This
        /// value is assigned to the 'alt' attribute. If not specified, an empty string is used.</param>
        /// <returns>An <see cref="IPictureContent"/> representing the constructed &lt;img&gt; element.</returns>
        public static IPictureContent Img(string src, string alt = "")
            => new ImgElement(src, alt);

        /// <summary>
        /// Creates a new HTML &lt;textarea&gt; element with the specified text content.
        /// </summary>
        /// <param name="text">The text to display within the &lt;textarea&gt; element. If null or empty, the &lt;textarea&gt; will be empty.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;textarea&gt; element containing the specified text.</returns>
        public static IHtmlNode TextArea(string text = "")
            => new Element("textarea", new TextNode(text));

        #endregion

        #region Block

        /// <summary>
        /// Creates a new HTML &lt;div&gt; element that contains the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the &lt;div&gt; element. Can be empty to create an empty &lt;div&gt;.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;div&gt; element with the specified children.</returns>
        public static IHtmlNode Div(params IHtmlNode[] children) => new Element("div", children);

        #region Table

        /// <summary>
        /// Creates a new table composed of the specified child table elements.
        /// </summary>
        /// <param name="children">An array of child table elements to include in the new table. Cannot be null.</param>
        /// <returns>A new instance of a table containing the specified child elements.</returns>
        public static IBaseTable Table(params IBaseTable[] children)
            => new Table(children);

        /// <summary>
        /// Creates a &lt;thead&gt; HTML table section element containing table row elements.
        /// </summary>
        /// <param name="children">An array of table rows to include in the header section.</param>
        /// <returns>An <see cref="IBaseTable"/> representing the constructed &lt;thead&gt; element.</returns>
        public static IBaseTable THead(params ITableRow[] children)
            => new Thead(children);

        /// <summary>
        /// Creates a table caption element containing the specified child nodes.
        /// </summary>
        /// <param name="children">An array of <see cref="IHtmlNode"/> objects that represent the child elements to include within the table
        /// caption.</param>
        /// <returns>An <see cref="IBaseTable"/> instance representing a table caption element with the provided child nodes.</returns>
        public static IBaseTable TableCaption(params IHtmlNode[] children) => new TableCaption(children);

        /// <summary>
        /// Creates a table header cell element containing the specified text.
        /// </summary>
        /// <param name="text">The text content to display within the table header cell.</param>
        /// <returns>An object representing a table header cell initialized with the specified text.</returns>
        public static IThOrTd Th(string text)
            => new Th(new TextNode(text));

        /// <summary>
        /// Creates a new HTML &lt;th&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to be added as the content of the &lt;th&gt; element. Can include text, elements, or other
        /// HTML nodes.</param>
        /// <returns>An object representing a &lt;th&gt; element containing the specified child nodes.</returns>
        public static IThOrTd Th(params IHtmlNode[] children)
            => new Th(children);

        /// <summary>
        /// Creates a new table cell element with the specified text content.
        /// </summary>
        /// <param name="text">The text to be used as the content of the table cell. Cannot be null.</param>
        /// <returns>An object representing a table cell containing the specified text.</returns>
        public static IThOrTd Td(string text)
            => new Td(new TextNode(text));

        /// <summary>
        /// Creates a new HTML &lt;td&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to be added as the contents of the &lt;td&gt; element. Can include text nodes, elements,
        /// or other HTML nodes. May be empty to create an empty cell.</param>
        /// <returns>An object representing a &lt;td&gt; HTML element containing the specified child nodes.</returns>
        public static IThOrTd Td(params IHtmlNode[] children)
            => new Td(children);

        /// <summary>
        /// Creates a new table row element containing the specified header or data cell elements.
        /// </summary>
        /// <param name="children">An array of table cell nodes that implement <see cref="IThOrTd"/>.</param>
        /// <returns>An <see cref="ITableRow"/> representing the constructed table row.</returns>
        public static ITableRow Tr(params IThOrTd[] children)
             => new Tr(children);


        /// <summary>
        /// Creates a new table body element containing table row elements.
        /// </summary>
        /// <param name="children">An array of table rows to include in the body section.</param>
        /// <returns>An <see cref="IBaseTable"/> representing the constructed &lt;tbody&gt; element.</returns>
        public static IBaseTable TBody(params ITableRow[] children)
             => new TBody(children);

        /// <summary>
        /// Creates a table footer element (&lt;tfoot&gt;) containing table row elements.
        /// </summary>
        /// <param name="children">An array of table rows to include in the footer section.</param>
        /// <returns>An <see cref="IBaseTable"/> representing the constructed &lt;tfoot&gt; element.</returns>
        public static IBaseTable TFoot(params ITableRow[] children)
             => new TFoot(children);

        /// <summary>
        /// Creates a table column group element containing the specified column definitions.
        /// </summary>
        /// <remarks>Use this method to define a &lt;colgroup&gt; element for a table, grouping multiple columns
        /// together for styling or layout purposes.</remarks>
        /// <param name="children">An array of column definitions to include in the column group. Cannot be null.</param>
        /// <returns>A &lt;colgroup&gt; element that contains the specified column definitions.</returns>
        public static IBaseTable ColGroup(params ITableColumnDef[] children)
             => new Colgroup(children);

        /// <summary>
        /// Creates a new table column definition with the specified style applied.
        /// </summary>
        /// <param name="style">The CSS style to apply to the column. Specify an empty string to use the default style.</param>
        /// <returns>An object that defines the table column with the specified style.</returns>
        public static ITableColumnDef Col(string style = "")
             => new Col().Style(style) as ITableColumnDef;
        #endregion

        /// <summary>
        /// Creates a new HTML &lt;section&gt; element with the specified child nodes.
        /// </summary>
        /// <remarks>Use this method to programmatically build a &lt;section&gt; element as part of an HTML
        /// document structure. The order of the child nodes is preserved in the resulting element.</remarks>
        /// <param name="children">An array of child nodes to include within the &lt;section&gt; element. Can be empty to create an empty section.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;section&gt; element containing the specified children.</returns>
        public static IHtmlNode Section(params IHtmlNode[] children)
            => new Element("section", children);

        /// <summary>
        /// Creates a new HTML &lt;p&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the &lt;p&gt; element. Can be empty to create an empty paragraph.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;p&gt; element containing the specified children.</returns>
        public static IHtmlNode P(params IHtmlNode[] children)
            => new Element("p", children);

        /// <summary>
        /// Creates a new HTML &lt;address&gt; element with the specified child nodes.
        /// </summary>
        /// <remarks>The &lt;address&gt; element is typically used to provide contact information for the
        /// enclosing section or document.</remarks>
        /// <param name="children">An array of child nodes to be included within the &lt;address&gt; element. Can be empty to create an empty
        /// element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;address&gt; element containing the specified children.</returns>
        public static IHtmlNode Address(params IHtmlNode[] children)
            => new Element("address", children);

        /// <summary>
        /// Creates a new HTML &lt;article&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the &lt;article&gt; element. Can be empty to create an empty article.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;article&gt; element containing the specified children.</returns>
        public static IHtmlNode Article(params IHtmlNode[] children)
            => new Element("article", children);

        /// <summary>
        /// Creates a new HTML &lt;aside&gt; element containing the specified child nodes.
        /// </summary>
        /// <remarks>Use this method to semantically group content that is tangentially related to the
        /// main content, such as sidebars or callouts, in accordance with HTML5 standards.</remarks>
        /// <param name="children">An array of child nodes to include within the &lt;aside&gt; element. Can be empty to create an empty aside.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;aside&gt; element with the specified children.</returns>
        public static IHtmlNode Aside(params IHtmlNode[] children)
            => new Element("aside", children);

        /// <summary>
        /// Creates a new HTML &lt;figure&gt; element that contains the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the &lt;figure&gt; element. Can be empty to create an empty figure.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;figure&gt; element with the specified children.</returns>
        public static IHtmlNode Figure(params IHtmlNode[] children)
            => new Element("figure", children);

        /// <summary>
        /// Creates a new &lt;figcaption&gt; HTML element containing the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the &lt;figcaption&gt; element. Can be empty to create an empty element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;figcaption&gt; element with the specified children.</returns>
        public static IHtmlNode FigCaption(params IHtmlNode[] children)
            => new Element("figcaption", children);

        /// <summary>
        /// Creates a new HTML &lt;details&gt; element containing the specified child nodes.
        /// </summary>
        /// <remarks>The &lt;details&gt; element is used to create a disclosure widget in HTML, allowing users
        /// to show or hide additional content. This method simplifies the creation of such elements in a programmatic
        /// HTML generation context.</remarks>
        /// <param name="children">An array of child nodes to include within the &lt;details&gt; element. Can be empty to create an empty element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;details&gt; element with the provided children.</returns>
        public static IHtmlNode Details(params IHtmlNode[] children)
            => new Element("details", children);

        /// <summary>
        /// Creates a new HTML &lt;summary&gt; element with the specified child nodes.
        /// </summary>
        /// <remarks>Use this method to programmatically generate a &lt;summary&gt; element, typically as part
        /// of a &lt;details&gt; disclosure widget or similar HTML structure.</remarks>
        /// <param name="children">An array of child nodes to include within the &lt;summary&gt; element. Can be empty to create an empty element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;summary&gt; element containing the specified children.</returns>
        public static IHtmlNode Summary(params IHtmlNode[] children)
            => new Element("summary", children);

        /// <summary>
        /// Creates a new HTML &lt;dialog&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of <see cref="IHtmlNode"/> objects that represent the child elements to include within the &lt;dialog&gt;.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;dialog&gt; element containing the specified children.</returns>
        public static IHtmlNode Dialog(params IHtmlNode[] children)
            => new Element("dialog", children);

        /// <summary>
        /// Creates a new HTML &lt;blockquote&gt; element with the specified citation and child nodes.
        /// </summary>
        /// <remarks>Use this method to generate a &lt;blockquote&gt; element for quoting external sources in HTML
        /// markup. The cite attribute is optional and provides a reference to the source of the quoted
        /// content.</remarks>
        /// <param name="cite">The URL or reference that identifies the source of the quotation. If empty, the citation attribute is
        /// omitted.</param>
        /// <param name="children">An array of child nodes to include within the &lt;blockquote&gt; element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;blockquote&gt; element with the specified citation and
        /// children.</returns>
        public static IHtmlNode Blockquote(string cite = "", params IHtmlNode[] children)
            => new Element("blockquote", children).Attr("cite", cite);

        /// <summary>
        /// Creates a new HTML &lt;canvas&gt; element with the specified width, height, and child nodes.
        /// </summary>
        /// <remarks>If the width or height parameters are empty, the corresponding attribute is omitted
        /// from the &lt;canvas&gt; element. The children are added as content inside the &lt;canvas&gt; element.</remarks>
        /// <param name="width">The width attribute of the &lt;canvas&gt; element. Specify as a string representing the pixel width, or leave empty
        /// to omit the attribute.</param>
        /// <param name="height">The height attribute of the &lt;canvas&gt; element. Specify as a string representing the pixel height, or leave
        /// empty to omit the attribute.</param>
        /// <param name="children">An array of child nodes to be added as children of the &lt;canvas&gt; element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;canvas&gt; element with the specified attributes and
        /// children.</returns>
        public static IHtmlNode Canvas(string width = "", string height = "", params IHtmlNode[] children)
            => new Element("canvas", children)
                .Attr("width", width)
                .Attr("height", height);

        /// <summary>
        /// Creates a new HTML &lt;audio&gt; element with the specified source and supported media child tags.
        /// </summary>
        /// <remarks>Only supported audio child tags are accepted: &lt;source&gt; and &lt;track&gt;.</remarks>
        /// <param name="src">The URL of the audio file to embed. If empty, the &lt;audio&gt; element will not have a source set.</param>
        /// <param name="children">An array of supported audio child nodes implementing <see cref="IAudioContent"/>.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;audio&gt; element.</returns>
        public static IHtmlNode Audio(string src = "", params IAudioContent[] children)
            => new Element("audio", children).Attr("src", src);

        /// <summary>
        /// Creates a new HTML &lt;video&gt; element with the specified source and supported media child tags.
        /// </summary>
        /// <remarks>Only supported video child tags are accepted: &lt;source&gt; and &lt;track&gt;.</remarks>
        /// <param name="src">The URL of the video file to be used as the source for the &lt;video&gt; element. If empty, the element will not
        /// have a source attribute.</param>
        /// <param name="children">An array of supported video child nodes implementing <see cref="IVideoContent"/>.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;video&gt; element.</returns>
        public static IHtmlNode Video(string src = "", params IVideoContent[] children)
            => new Element("video", children).Attr("src", src);

        /// <summary>
        /// Creates a new HTML &lt;source&gt; element with the specified source URL and MIME type.
        /// </summary>
        /// <remarks>This node is valid for media composition in &lt;audio&gt;, &lt;video&gt;, and &lt;picture&gt; builders.</remarks>
        /// <param name="src">The URL of the media resource.</param>
        /// <param name="type">The MIME type of the media resource.</param>
        /// <returns>An <see cref="ISourceContent"/> representing the constructed &lt;source&gt; element.</returns>
        public static ISourceContent Source(string src = "", string type = "")
            => new SourceElement(src, type);

        /// <summary>
        /// Creates a new HTML &lt;track&gt; element for timed text tracks used by media elements.
        /// </summary>
        /// <remarks>This node is valid for media composition in &lt;audio&gt; and &lt;video&gt; builders.</remarks>
        /// <param name="kind">The kind of text track, such as subtitles or captions.</param>
        /// <param name="src">The URL of the track file.</param>
        /// <param name="srclang">The language tag of the track data.</param>
        /// <param name="label">A user-readable label for the track.</param>
        /// <returns>An <see cref="ITrackContent"/> representing the constructed &lt;track&gt; element.</returns>
        public static ITrackContent Track(string kind = "", string src = "", string srclang = "", string label = "")
            => new TrackElement(kind, src, srclang, label);

        /// <summary>
        /// Creates a new HTML &lt;picture&gt; element with supported picture child tags.
        /// </summary>
        /// <remarks>Only supported picture child tags are accepted: &lt;source&gt; and &lt;img&gt;.</remarks>
        /// <param name="children">An array of supported picture child nodes implementing <see cref="IPictureContent"/>.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;picture&gt; element.</returns>
        public static IHtmlNode Picture(params IPictureContent[] children)
            => new Element("picture", children);


        /// <summary>
        /// Creates a new HTML &lt;iframe&gt; element with the specified source URL and title attributes.
        /// </summary>
        /// <param name="src">The value to assign to the 'src' attribute of the &lt;iframe&gt; element. Represents the URL of the page to
        /// display. If not specified, the attribute will be set to an empty string.</param>
        /// <param name="title">The value to assign to the 'title' attribute of the &lt;iframe&gt; element. Used to provide a descriptive title
        /// for accessibility purposes. If not specified, the attribute will be set to an empty string.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;iframe&gt; element with the specified attributes.</returns>
        public static IHtmlNode IFrame(string src = "", string title = "")
            => new Element("iframe", new IHtmlNode[0]).Attr("src", src).Attr("title", title);

        /// <summary>
        /// Creates a new HTML &lt;embed&gt; element with the specified source and MIME type attributes.
        /// </summary>
        /// <remarks>The &lt;embed&gt; element is used to embed external content, such as multimedia or
        /// interactive resources, into an HTML document. The caller is responsible for providing valid attribute values
        /// as required by the embedded content.</remarks>
        /// <param name="src">The value for the 'src' attribute, specifying the URL of the embedded resource. If not specified, the
        /// attribute will be empty.</param>
        /// <param name="type">The value for the 'type' attribute, specifying the MIME type of the embedded resource. If not specified, the
        /// attribute will be empty.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;embed&gt; element with the specified attributes.</returns>
        public static IHtmlNode Embed(string src = "", string type = "")
            => new Element("embed", true).Attr("src", src).Attr("type", type);

        /// <summary>
        /// Creates a new HTML &lt;object&gt; element with the specified data and type attributes, containing optional child
        /// nodes.
        /// </summary>
        /// <param name="data">The URL of the resource to be used by the &lt;object&gt; element. If empty, the attribute is omitted.</param>
        /// <param name="type">The MIME type of the resource specified by <paramref name="data"/>. If empty, the attribute is omitted.</param>
        /// <param name="children">An array of child nodes to include within the &lt;object&gt; element, such as fallback content or &lt;param&gt;
        /// elements.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;object&gt; element.</returns>
        public static IHtmlNode Object(string data = "", string type = "", params IHtmlNode[] children)
            => new Element("object", children).Attr("data", data).Attr("type", type);

        /// <summary>
        /// Creates a new HTML &lt;param&gt; element with the specified name and value attributes.
        /// </summary>
        /// <param name="name">The parameter name to assign to the 'name' attribute.</param>
        /// <param name="value">The parameter value to assign to the 'value' attribute.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing a self-closing &lt;param&gt; element.</returns>
        public static IHtmlNode Param(string name = "", string value = "")
            => new Element("param", true).Attr("name", name).Attr("value", value);

        /// <summary>
        /// Creates a new HTML &lt;data&gt; element with the specified value attribute and child nodes.
        /// </summary>
        /// <param name="value">The machine-readable value to assign to the 'value' attribute.</param>
        /// <param name="children">An array of child nodes to include as the display content of the &lt;data&gt; element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;data&gt; element.</returns>
        public static IHtmlNode Data(string value = "", params IHtmlNode[] children)
            => new Element("data", children).Attr("value", value);

        /// <summary>
        /// Creates a new HTML &lt;meter&gt; element with value range attributes and optional child nodes.
        /// </summary>
        /// <param name="value">The current numeric value of the meter.</param>
        /// <param name="min">The minimum numeric value of the meter range.</param>
        /// <param name="max">The maximum numeric value of the meter range.</param>
        /// <param name="children">An array of child nodes to include as fallback or descriptive content.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;meter&gt; element.</returns>
        public static IHtmlNode Meter(string value = "", string min = "", string max = "", params IHtmlNode[] children)
            => new Element("meter", children)
                .Attr("value", value)
                .Attr("min", min)
                .Attr("max", max);

        /// <summary>
        /// Creates a new HTML &lt;progress&gt; element with the specified value and max attributes.
        /// </summary>
        /// <param name="value">The current progress value.</param>
        /// <param name="max">The maximum progress value.</param>
        /// <param name="children">An array of child nodes to include as fallback content.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;progress&gt; element.</returns>
        public static IHtmlNode Progress(string value = "", string max = "", params IHtmlNode[] children)
            => new Element("progress", children)
                .Attr("value", value)
                .Attr("max", max);

        /// <summary>
        /// Creates a new HTML &lt;dl&gt; (description list) element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes, typically containing &lt;dt&gt; and &lt;dd&gt; elements.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;dl&gt; element.</returns>
        public static IHtmlNode Dl(params IHtmlNode[] children)
            => new Element("dl", children);

        /// <summary>
        /// Creates a new HTML &lt;dt&gt; (description term) element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the term element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;dt&gt; element.</returns>
        public static IHtmlNode Dt(params IHtmlNode[] children)
            => new Element("dt", children);

        /// <summary>
        /// Creates a new HTML &lt;dd&gt; (description details) element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the details element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;dd&gt; element.</returns>
        public static IHtmlNode Dd(params IHtmlNode[] children)
            => new Element("dd", children);

        /// <summary>
        /// Creates a new HTML &lt;fieldset&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the fieldset.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;fieldset&gt; element.</returns>
        public static IHtmlNode Fieldset(params IHtmlNode[] children)
            => new Element("fieldset", children);

        /// <summary>
        /// Creates a new HTML &lt;legend&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the legend.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;legend&gt; element.</returns>
        public static IHtmlNode Legend(params IHtmlNode[] children)
            => new Element("legend", children);

        /// <summary>
        /// Creates a new HTML &lt;select&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes, typically containing &lt;option&gt; or &lt;optgroup&gt; elements.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;select&gt; element.</returns>
        public static IHtmlNode Select(params IHtmlNode[] children)
            => new Element("select", children);

        /// <summary>
        /// Creates a new HTML &lt;option&gt; element with the specified value attribute and child nodes.
        /// </summary>
        /// <param name="value">The machine-readable value for the option.</param>
        /// <param name="children">An array of child nodes representing the display text/content of the option.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;option&gt; element.</returns>
        public static IHtmlNode Option(string value = "", params IHtmlNode[] children)
            => new Element("option", children).Attr("value", value);

        /// <summary>
        /// Creates a new HTML &lt;optgroup&gt; element with the specified label and child option nodes.
        /// </summary>
        /// <param name="label">The group label to assign to the 'label' attribute.</param>
        /// <param name="children">An array of child nodes, typically containing &lt;option&gt; elements.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;optgroup&gt; element.</returns>
        public static IHtmlNode OptGroup(string label = "", params IHtmlNode[] children)
            => new Element("optgroup", children).Attr("label", label);

        /// <summary>
        /// Creates a new HTML &lt;datalist&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes, typically containing &lt;option&gt; elements.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;datalist&gt; element.</returns>
        public static IHtmlNode Datalist(params IHtmlNode[] children)
            => new Element("datalist", children);

        /// <summary>
        /// Creates a new HTML &lt;output&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the output element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;output&gt; element.</returns>
        public static IHtmlNode Output(params IHtmlNode[] children)
            => new Element("output", children);

        /// <summary>
        /// Creates a new HTML &lt;hr&gt; (thematic break) element node.
        /// </summary>
        /// <returns>An <see cref="IHtmlNode"/> representing a self-closing &lt;hr&gt; element.</returns>
        public static IHtmlNode Hr()
            => new Element("hr", true);

        /// <summary>
        /// Creates a heading element (&lt;h1&gt; through &lt;h6&gt;) with the specified child nodes.
        /// </summary>
        /// <param name="heading">The heading level to generate.</param>
        /// <param name="children">An array of child nodes to include within the heading element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed heading element.</returns>
        public static IHtmlNode Heading(Heading heading, params IHtmlNode[] children)
            => new Element(heading.ToTag(), children);

        /// <summary>
        /// Creates a new HTML &lt;form&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the form.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;form&gt; element.</returns>
        public static IHtmlNode Form(params IHtmlNode[] children)
            => new Element("form", children);

        /// <summary>
        /// Creates a new HTML &lt;ul&gt; (unordered list) element with the specified list item children.
        /// </summary>
        /// <param name="children">An array of list elements to include in the unordered list.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;ul&gt; element.</returns>
        public static IHtmlNode Ul(params IListElement[] children)
            => new Ul(children);

        /// <summary>
        /// Creates a new HTML &lt;ol&gt; (ordered list) element with the specified list item children.
        /// </summary>
        /// <param name="children">An array of list elements to include in the ordered list.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;ol&gt; element.</returns>
        public static IHtmlNode Ol(params IListElement[] children)
            => new Ol(children);

        /// <summary>
        /// Creates a new HTML &lt;li&gt; (list item) element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the list item.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;li&gt; element.</returns>
        public static IHtmlNode Li(params IHtmlNode[] children)
            => new Li(children);

        /// <summary>
        /// Creates a new HTML &lt;nav&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the navigation element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;nav&gt; element.</returns>
        public static IHtmlNode Nav(params IHtmlNode[] children)
            => new Element("nav", children);

        /// <summary>
        /// Creates a new HTML &lt;main&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the main content element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;main&gt; element.</returns>
        public static IHtmlNode Main(params IHtmlNode[] children)
            => new Element("main", children);

        /// <summary>
        /// Creates a new HTML &lt;footer&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the footer element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;footer&gt; element.</returns>
        public static IHtmlNode Footer(params IHtmlNode[] children)
            => new Element("footer", children);

        /// <summary>
        /// Creates a new HTML &lt;header&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the header element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;header&gt; element.</returns>
        public static IHtmlNode Header(params IHtmlNode[] children)
            => new Element("header", children);

        /// <summary>
        /// Creates a new HTML &lt;search&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the search element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;search&gt; element.</returns>
        public static IHtmlNode Search(params IHtmlNode[] children)
            => new Element("search", children);

        #endregion

        /// <summary>
        /// Creates a custom HTML element with the specified tag name, self-closing behavior, and child nodes.
        /// </summary>
        /// <param name="tag">The HTML tag name to create.</param>
        /// <param name="isSelfClosing">True to render the element as self-closing; otherwise false.</param>
        /// <param name="children">An array of child nodes to include within the custom element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed custom element.</returns>
        public static IHtmlNode Custom(
                string tag,
                bool isSelfClosing = false,
                params IHtmlNode[] children
            )
                => new Element(tag, isSelfClosing, children);

        /// <summary>
        /// Creates a complete HTML document node containing the specified top-level children.
        /// </summary>
        /// <param name="children">An array of child nodes, typically including &lt;head&gt; and &lt;body&gt; elements.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed HTML document.</returns>
        public static IHtmlNode HtmlDocument(params IHtmlNode[] children)
            => new HtmlDocument(children);

        /// <summary>
        /// Creates a new HTML &lt;body&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the body element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;body&gt; element.</returns>
        public static IHtmlNode Body(params IHtmlNode[] children)
            => new Element("body", children);

        /// <summary>
        /// Creates a new HTML &lt;head&gt; element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the head element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;head&gt; element.</returns>
        public static IHtmlNode Head(params IHtmlNode[] children)
            => new Element("head", children);

        /// <summary>
        /// Creates a new HTML &lt;title&gt; element containing the specified text.
        /// </summary>
        /// <param name="text">The title text to include in the element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;title&gt; element.</returns>
        public static IHtmlNode Title(string text)
            => new Element("title", new TextNode(text));

        /// <summary>
        /// Creates a new HTML &lt;meta&gt; element with the specified name and content attributes.
        /// </summary>
        /// <param name="name">The metadata name.</param>
        /// <param name="content">The metadata content value.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing a self-closing &lt;meta&gt; element.</returns>
        public static IHtmlNode Meta(string name, string content)
            => new Element("meta", true).Attr("name", name).Attr("content", content);

        /// <summary>
        /// Creates a new HTML &lt;link&gt; element with the specified href and rel attributes.
        /// </summary>
        /// <param name="href">The resource URL to assign to the href attribute.</param>
        /// <param name="rel">The relationship type to assign to the rel attribute.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing a self-closing &lt;link&gt; element.</returns>
        public static IHtmlNode Link(string href, string rel = "stylesheet")
            => new Element("link", true).Attr("href", href).Attr("rel", rel);

        /// <summary>
        /// Creates a new HTML &lt;style&gt; element containing the specified CSS content.
        /// </summary>
        /// <param name="style">The CSS text to include in the style element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;style&gt; element.</returns>
        public static IHtmlNode Style(string style) => new Element("style", new TextNode(style));

        /// <summary>
        /// Creates a new HTML &lt;script&gt; element with inline script content and optional src attribute.
        /// </summary>
        /// <param name="script">The inline JavaScript content.</param>
        /// <param name="src">The external script URL to assign to the src attribute.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed &lt;script&gt; element.</returns>
        public static IHtmlNode Script(string script, string src = "")
            => new Element("script", new TextNode(script)).Attr("src", src);

        /// <summary>
        /// Creates a new HTML text node containing the specified text content.
        /// </summary>
        /// <param name="text">The text content to include in the HTML text node. This value can be null or empty, in which case the node
        /// will represent empty text.</param>
        /// <returns>An <see cref="IHtmlNode"/> instance representing a text node with the specified content.</returns>
        public static IHtmlNode TextNode(string text) => new TextNode(text);

    }
}