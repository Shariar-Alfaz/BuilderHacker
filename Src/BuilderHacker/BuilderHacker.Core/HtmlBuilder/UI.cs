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
        /// Creates a new HTML <span> element containing the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the <span> element. Can be empty to create an empty <span>.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <span> element with the specified children.</returns>
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
        /// Creates a new HTML <b> element with the specified child nodes.
        /// </summary>
        /// <remarks>Use this method to programmatically generate bold text content in an HTML document by
        /// wrapping child nodes within a <b> element.</remarks>
        /// <param name="children">An array of <see cref="IHtmlNode"/> objects to be added as children of the <b> element. Can be empty to
        /// create an empty <b> element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <b> element containing the specified child nodes.</returns>
        public static IHtmlNode B(params IHtmlNode[] children) => new Element("b", children);

        /// <summary>
        /// Creates a new HTML <i> element with the specified child nodes.
        /// </summary>
        /// <remarks>The <i> element is typically used to render text in an alternate voice or style, such
        /// as italics. This method provides a convenient way to programmatically construct such elements with optional
        /// child content.</remarks>
        /// <param name="children">An array of child nodes to be added as the content of the <i> element. Can be empty to create an empty
        /// element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <i> element containing the specified child nodes.</returns>
        public static IHtmlNode I(params IHtmlNode[] children) => new Element("i", children);

        /// <summary>
        /// Creates a new HTML <u> element with the specified child nodes.
        /// </summary>
        /// <remarks>The <u> element represents text that should be stylistically different from normal
        /// text, typically rendered with an underline. This method is useful for programmatically building HTML
        /// content.</remarks>
        /// <param name="children">An array of child nodes to be added as the content of the <u> element. Can be empty to create an empty
        /// element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <u> element containing the specified children.</returns>
        public static IHtmlNode U(params IHtmlNode[] children) => new Element("u", children);

        /// <summary>
        /// Creates a new HTML <em> element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to be added as the content of the <em> element. Can be empty.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <em> element containing the specified children.</returns>
        public static IHtmlNode Em(params IHtmlNode[] children) => new Element("em", children);

        /// <summary>
        /// Creates a new HTML 'small' element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to be added as the content of the 'small' element. Can be empty.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed 'small' HTML element containing the specified
        /// children.</returns>
        public static IHtmlNode Small(params IHtmlNode[] children) => new Element("small", children);

        /// <summary>
        /// Creates a <mark> HTML element that highlights text, containing the specified child nodes.
        /// </summary>
        /// <remarks>Use the <mark> element to indicate text that should be highlighted or marked for
        /// reference. This method simplifies the creation of semantic HTML for text emphasis.</remarks>
        /// <param name="children">An array of child nodes to be included within the <mark> element. Can be empty to create an empty element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <mark> element with the specified children.</returns>
        public static IHtmlNode Mark(params IHtmlNode[] children) => new Element("mark", children);

        /// <summary>
        /// Creates a new HTML <del> element with the specified child nodes.
        /// </summary>
        /// <remarks>The <del> element is used to represent content that has been deleted from a document.
        /// This method provides a convenient way to programmatically construct such elements with any number of child
        /// nodes.</remarks>
        /// <param name="children">An array of child nodes to be added as the content of the <del> element. Can be empty to create an empty
        /// element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <del> element containing the specified children.</returns>
        public static IHtmlNode Del(params IHtmlNode[] children) => new Element("del", children);

        /// <summary>
        /// Creates a new HTML <ins> element with the specified child nodes.
        /// </summary>
        /// <remarks>The <ins> element represents inserted text in an HTML document. This method is
        /// typically used to programmatically build HTML structures.</remarks>
        /// <param name="children">An array of child nodes to be added as the content of the <ins> element. Can be empty.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <ins> element containing the specified children.</returns>
        public static IHtmlNode Ins(params IHtmlNode[] children) => new Element("ins", children);

        /// <summary>
        /// Creates an HTML <s> element that renders its child nodes with strikethrough formatting.
        /// </summary>
        /// <remarks>The <s> element is typically used to indicate text that is no longer accurate or
        /// relevant. Child nodes are rendered in the order provided.</remarks>
        /// <param name="children">An array of child nodes to be included within the <s> element. Can be empty to create an empty element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <s> element containing the specified child nodes.</returns>
        public static IHtmlNode S(params IHtmlNode[] children) => new Element("s", children);

        /// <summary>
        /// Creates an HTML <abbr> element with the specified title attribute and child nodes.
        /// </summary>
        /// <remarks>Use this method to semantically mark up abbreviations or acronyms in HTML, improving
        /// accessibility and providing additional context for users and assistive technologies.</remarks>
        /// <param name="title">The value for the 'title' attribute, providing an expanded description of the abbreviation. If empty, the
        /// attribute is omitted.</param>
        /// <param name="children">The child nodes to include within the <abbr> element. Can be text or other HTML nodes.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <abbr> element with the specified attributes and
        /// children.</returns>
        public static IHtmlNode Abbr(string title = "", params IHtmlNode[] children)
            => new Element("abbr", children).Attr("title", title);

        /// <summary>
        /// Creates a new HTML <time> element with the specified datetime attribute and child nodes.
        /// </summary>
        /// <remarks>Use this method to generate semantic HTML for representing dates and times, which can
        /// improve accessibility and machine-readability of your markup.</remarks>
        /// <param name="datetime">The value to assign to the 'datetime' attribute of the <time> element. This should be a valid date or time
        /// string, or an empty string if the attribute should be omitted.</param>
        /// <param name="children">An array of child nodes to be added as the content of the <time> element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <time> element with the specified attributes and
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
        /// Creates a <kbd> HTML element that represents user input, containing the specified child nodes.
        /// </summary>
        /// <remarks>The <kbd> element is typically used to denote keyboard input in HTML documents. Child
        /// nodes can include text or other HTML elements as needed.</remarks>
        /// <param name="children">An array of child nodes to be included within the <kbd> element. Can be empty to create an empty element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <kbd> element with the specified children.</returns>
        public static IHtmlNode Kbd(params IHtmlNode[] children) => new Element("kbd", children);

        /// <summary>
        /// Creates a <samp> HTML element that represents sample output from a program or computing system, containing
        /// the specified child nodes.
        /// </summary>
        /// <remarks>The <samp> element is typically used to display output from programs or commands in a
        /// way that distinguishes it from surrounding text. Child nodes can include text or other HTML elements as
        /// appropriate.</remarks>
        /// <param name="children">An array of child nodes to be included within the <samp> element. Can be empty to create an empty <samp>
        /// element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <samp> element with the specified children.</returns>
        public static IHtmlNode Samp(params IHtmlNode[] children) => new Element("samp", children);

        /// <summary>
        /// Creates a new HTML <var> element with the specified child nodes.
        /// </summary>
        /// <remarks>The <var> element is typically used to represent a variable in mathematical expressions
        /// or programming code within HTML content.</remarks>
        /// <param name="children">An array of child nodes to be added as the content of the <var> element. Can be empty.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <var> element containing the specified children.</returns>
        public static IHtmlNode Var(params IHtmlNode[] children) => new Element("var", children);

        /// <summary>
        /// Creates a new HTML <q> element with the specified cite attribute and child nodes.
        /// </summary>
        /// <param name="cite">The value of the cite attribute, specifying the source of the quotation. If empty, the cite attribute is
        /// omitted.</param>
        /// <param name="children">An array of child nodes to be added as the content of the <q> element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <q> element with the specified attributes and
        /// children.</returns>
        public static IHtmlNode Q(string cite = "", params IHtmlNode[] children)
            => new Element("q", children).Attr("cite", cite);

        /// <summary>
        /// Creates a new HTML <cite> element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to be added as the content of the <cite> element. Can be empty to create an empty
        /// element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <cite> element containing the specified child nodes.</returns>
        public static IHtmlNode Cite(params IHtmlNode[] children) => new Element("cite", children);

        /// <summary>
        /// Creates a new HTML <br> (line break) element node.
        /// </summary>
        /// <remarks>Use this method to insert a line break in generated HTML content. The returned node is
        /// self-closing and does not contain any child elements.</remarks>
        /// <returns>An <see cref="IHtmlNode"/> representing a self-closing <br> element.</returns>
        public static IHtmlNode Br() => new Element("br", true);

        /// <summary>
        /// Creates a new HTML <wbr> (word break opportunity) element node.
        /// </summary>
        /// <remarks>The <wbr> element is used to indicate a position within text where the browser may
        /// optionally break a line. This method is useful when generating HTML content that requires explicit word break
        /// opportunities.</remarks>
        /// <returns>An <see cref="IHtmlNode"/> representing a self-closing <wbr> element.</returns>
        public static IHtmlNode Wbr() => new Element("wbr", true);

        /// <summary>
        /// Creates a new HTML <button> element with the specified text content.
        /// </summary>
        /// <param name="text">The text to display inside the button element. Cannot be null.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the created button element containing the specified text.</returns>
        public static IHtmlNode Button(string text) => new Element("button", new TextNode(text));

        /// <summary>
        /// Creates a new HTML <strong> element that contains the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to be included within the <strong> element. Can be empty to create an empty element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <strong> element with the specified children.</returns>
        public static IHtmlNode Strong(params IHtmlNode[] children) => new Element("strong", children);


        /// <summary>
        /// Creates a new HTML <sub> element with the specified child nodes.
        /// </summary>
        /// <remarks>Use this method to generate subscript text in HTML documents by providing the desired
        /// child nodes. The resulting element can be further composed or rendered as part of a larger HTML
        /// structure.</remarks>
        /// <param name="children">An array of child nodes to be added as the content of the <sub> element. Can be empty to create an empty
        /// element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <sub> element containing the specified children.</returns>
        public static IHtmlNode Sub(params IHtmlNode[] children) => new Element("sub", children);


        /// <summary>
        /// Creates a new HTML <sup> element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to be added as the content of the <sup> element. Can be empty to create an empty
        /// element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <sup> element containing the specified children.</returns>
        public static IHtmlNode Sup(params IHtmlNode[] children) => new Element("sup", children);

        /// <summary>
        /// Creates an HTML <code> element containing the specified code text.
        /// </summary>
        /// <param name="code">The code text to be enclosed within the <code> element. Cannot be null.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing a <code> element with the provided code text as its content.</returns>
        public static IHtmlNode Code(string code) => new Element("code", new TextNode(code));

        /// <summary>
        /// Creates a new <pre> HTML element that contains the specified child nodes.
        /// </summary>
        /// <remarks>The <pre> element preserves both whitespace and line breaks in its content, making it
        /// suitable for displaying preformatted text.</remarks>
        /// <param name="children">An array of child nodes to include within the <pre> element. Can be empty to create an empty <pre> element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <pre> element with the specified children.</returns>
        public static IHtmlNode Pre(params IHtmlNode[] children) => new Element("pre", children);


        /// <summary>
        /// Creates a new HTML <label> element with the specified text content.
        /// </summary>
        /// <param name="label">The text to display within the label element. Cannot be null.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed label element containing the specified text.</returns>
        public static IHtmlNode Label(string label) => new Element("label", new TextNode(label));

        /// <summary>
        /// Creates a new HTML <input> element with the specified type and value attributes.
        /// </summary>
        /// <param name="type">The value to assign to the input element's 'type' attribute. Defaults to "text" if not specified.</param>
        /// <param name="value">The value to assign to the input element's 'value' attribute. Defaults to an empty string if not specified.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed input element with the specified attributes.</returns>
        public static IHtmlNode Input(string type = "text", string value = "")
            => new Element("input", true)
                .Attr("type", type)
                .Attr("value", value);

        /// <summary>
        /// Creates a new HTML <img> element with the specified source and alternate text.
        /// </summary>
        /// <param name="src">The URL of the image to display in the <img> element. This value is assigned to the 'src' attribute and
        /// cannot be null.</param>
        /// <param name="alt">The alternate text for the image, used for accessibility and displayed if the image cannot be loaded. This
        /// value is assigned to the 'alt' attribute. If not specified, an empty string is used.</param>
        /// <returns>An <see cref="IPictureContent"/> representing the constructed <img> element.</returns>
        public static IPictureContent Img(string src, string alt = "")
            => new ImgElement(src, alt);

        /// <summary>
        /// Creates a new HTML <textarea> element with the specified text content.
        /// </summary>
        /// <param name="text">The text to display within the textarea element. If null or empty, the textarea will be empty.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed textarea element containing the specified text.</returns>
        public static IHtmlNode TextArea(string text = "")
            => new Element("textarea", new TextNode(text));

        #endregion

        #region Block

        /// <summary>
        /// Creates a new HTML <div> element that contains the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the <div> element. Can be empty to create an empty <div>.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <div> element with the specified children.</returns>
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
        /// Creates a <thead> HTML table section element containing table row elements.
        /// </summary>
        /// <param name="children">An array of table rows to include in the header section.</param>
        /// <returns>An <see cref="IBaseTable"/> representing the constructed <thead> element.</returns>
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
        /// Creates a new HTML <th> element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to be added as the content of the <th> element. Can include text, elements, or other
        /// HTML nodes.</param>
        /// <returns>An object representing a <th> element containing the specified child nodes.</returns>
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
        /// Creates a new HTML <td> element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to be added as the contents of the <td> element. Can include text nodes, elements,
        /// or other HTML nodes. May be empty to create an empty cell.</param>
        /// <returns>An object representing a <td> HTML element containing the specified child nodes.</returns>
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
        /// <returns>An <see cref="IBaseTable"/> representing the constructed <tbody> element.</returns>
        public static IBaseTable TBody(params ITableRow[] children)
             => new TBody(children);

        /// <summary>
        /// Creates a table footer element (<tfoot>) containing table row elements.
        /// </summary>
        /// <param name="children">An array of table rows to include in the footer section.</param>
        /// <returns>An <see cref="IBaseTable"/> representing the constructed <tfoot> element.</returns>
        public static IBaseTable TFoot(params ITableRow[] children)
             => new TFoot(children);

        /// <summary>
        /// Creates a table column group element containing the specified column definitions.
        /// </summary>
        /// <remarks>Use this method to define a <colgroup> element for a table, grouping multiple columns
        /// together for styling or layout purposes.</remarks>
        /// <param name="children">An array of column definitions to include in the column group. Cannot be null.</param>
        /// <returns>A table column group element that contains the specified column definitions.</returns>
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
        /// Creates a new HTML <section> element with the specified child nodes.
        /// </summary>
        /// <remarks>Use this method to programmatically build a section element as part of an HTML
        /// document structure. The order of the child nodes is preserved in the resulting element.</remarks>
        /// <param name="children">An array of child nodes to include within the section element. Can be empty to create an empty section.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed section element containing the specified children.</returns>
        public static IHtmlNode Section(params IHtmlNode[] children)
            => new Element("section", children);

        /// <summary>
        /// Creates a new HTML <p> element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the paragraph element. Can be empty to create an empty paragraph.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <p> element containing the specified children.</returns>
        public static IHtmlNode P(params IHtmlNode[] children)
            => new Element("p", children);

        /// <summary>
        /// Creates a new HTML <address> element with the specified child nodes.
        /// </summary>
        /// <remarks>The <address> element is typically used to provide contact information for the
        /// enclosing section or document.</remarks>
        /// <param name="children">An array of child nodes to be included within the <address> element. Can be empty to create an empty
        /// element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <address> element containing the specified children.</returns>
        public static IHtmlNode Address(params IHtmlNode[] children)
            => new Element("address", children);

        /// <summary>
        /// Creates a new HTML <article> element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the <article> element. Can be empty to create an empty article.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <article> element containing the specified children.</returns>
        public static IHtmlNode Article(params IHtmlNode[] children)
            => new Element("article", children);

        /// <summary>
        /// Creates a new HTML <aside> element containing the specified child nodes.
        /// </summary>
        /// <remarks>Use this method to semantically group content that is tangentially related to the
        /// main content, such as sidebars or callouts, in accordance with HTML5 standards.</remarks>
        /// <param name="children">An array of child nodes to include within the <aside> element. Can be empty to create an empty aside.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <aside> element with the specified children.</returns>
        public static IHtmlNode Aside(params IHtmlNode[] children)
            => new Element("aside", children);

        /// <summary>
        /// Creates a new HTML <figure> element that contains the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the <figure> element. Can be empty to create an empty figure.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <figure> element with the specified children.</returns>
        public static IHtmlNode Figure(params IHtmlNode[] children)
            => new Element("figure", children);

        /// <summary>
        /// Creates a new <figcaption> HTML element containing the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the <figcaption> element. Can be empty to create an empty element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <figcaption> element with the specified children.</returns>
        public static IHtmlNode FigCaption(params IHtmlNode[] children)
            => new Element("figcaption", children);

        /// <summary>
        /// Creates a new HTML <details> element containing the specified child nodes.
        /// </summary>
        /// <remarks>The <details> element is used to create a disclosure widget in HTML, allowing users
        /// to show or hide additional content. This method simplifies the creation of such elements in a programmatic
        /// HTML generation context.</remarks>
        /// <param name="children">An array of child nodes to include within the <details> element. Can be empty to create an empty element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <details> element with the provided children.</returns>
        public static IHtmlNode Details(params IHtmlNode[] children)
            => new Element("details", children);

        /// <summary>
        /// Creates a new HTML <summary> element with the specified child nodes.
        /// </summary>
        /// <remarks>Use this method to programmatically generate a <summary> element, typically as part
        /// of a details disclosure widget or similar HTML structure.</remarks>
        /// <param name="children">An array of child nodes to include within the <summary> element. Can be empty to create an empty element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <summary> element containing the specified children.</returns>
        public static IHtmlNode Summary(params IHtmlNode[] children)
            => new Element("summary", children);

        /// <summary>
        /// Creates a new HTML <dialog> element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of <see cref="IHtmlNode"/> objects that represent the child elements to include within the dialog.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <dialog> element containing the specified children.</returns>
        public static IHtmlNode Dialog(params IHtmlNode[] children)
            => new Element("dialog", children);

        /// <summary>
        /// Creates a new HTML <blockquote> element with the specified citation and child nodes.
        /// </summary>
        /// <remarks>Use this method to generate a blockquote element for quoting external sources in HTML
        /// markup. The cite attribute is optional and provides a reference to the source of the quoted
        /// content.</remarks>
        /// <param name="cite">The URL or reference that identifies the source of the quotation. If empty, the citation attribute is
        /// omitted.</param>
        /// <param name="children">An array of child nodes to include within the blockquote element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed blockquote element with the specified citation and
        /// children.</returns>
        public static IHtmlNode Blockquote(string cite = "", params IHtmlNode[] children)
            => new Element("blockquote", children).Attr("cite", cite);

        /// <summary>
        /// Creates a new HTML <canvas> element with the specified width, height, and child nodes.
        /// </summary>
        /// <remarks>If the width or height parameters are empty, the corresponding attribute is omitted
        /// from the canvas element. The children are added as content inside the canvas element.</remarks>
        /// <param name="width">The width attribute of the canvas element. Specify as a string representing the pixel width, or leave empty
        /// to omit the attribute.</param>
        /// <param name="height">The height attribute of the canvas element. Specify as a string representing the pixel height, or leave
        /// empty to omit the attribute.</param>
        /// <param name="children">An array of child nodes to be added as children of the canvas element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed canvas element with the specified attributes and
        /// children.</returns>
        public static IHtmlNode Canvas(string width = "", string height = "", params IHtmlNode[] children)
            => new Element("canvas", children)
                .Attr("width", width)
                .Attr("height", height);

        /// <summary>
        /// Creates a new HTML <audio> element with the specified source and supported media child tags.
        /// </summary>
        /// <remarks>Only supported audio child tags are accepted: <source> and <track>.</remarks>
        /// <param name="src">The URL of the audio file to embed. If empty, the <audio> element will not have a source set.</param>
        /// <param name="children">An array of supported audio child nodes implementing <see cref="IAudioContent"/>.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <audio> element.</returns>
        public static IHtmlNode Audio(string src = "", params IAudioContent[] children)
            => new Element("audio", children).Attr("src", src);

        /// <summary>
        /// Creates a new HTML <video> element with the specified source and supported media child tags.
        /// </summary>
        /// <remarks>Only supported video child tags are accepted: <source> and <track>.</remarks>
        /// <param name="src">The URL of the video file to be used as the source for the <video> element. If empty, the element will not
        /// have a source attribute.</param>
        /// <param name="children">An array of supported video child nodes implementing <see cref="IVideoContent"/>.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <video> element.</returns>
        public static IHtmlNode Video(string src = "", params IVideoContent[] children)
            => new Element("video", children).Attr("src", src);

        /// <summary>
        /// Creates a new HTML <source> element with the specified source URL and MIME type.
        /// </summary>
        /// <remarks>This node is valid for media composition in <audio>, <video>, and <picture> builders.</remarks>
        /// <param name="src">The URL of the media resource.</param>
        /// <param name="type">The MIME type of the media resource.</param>
        /// <returns>An <see cref="ISourceContent"/> representing the constructed <source> element.</returns>
        public static ISourceContent Source(string src = "", string type = "")
            => new SourceElement(src, type);

        /// <summary>
        /// Creates a new HTML <track> element for timed text tracks used by media elements.
        /// </summary>
        /// <remarks>This node is valid for media composition in <audio> and <video> builders.</remarks>
        /// <param name="kind">The kind of text track, such as subtitles or captions.</param>
        /// <param name="src">The URL of the track file.</param>
        /// <param name="srclang">The language tag of the track data.</param>
        /// <param name="label">A user-readable label for the track.</param>
        /// <returns>An <see cref="ITrackContent"/> representing the constructed <track> element.</returns>
        public static ITrackContent Track(string kind = "", string src = "", string srclang = "", string label = "")
            => new TrackElement(kind, src, srclang, label);

        /// <summary>
        /// Creates a new HTML <picture> element with supported picture child tags.
        /// </summary>
        /// <remarks>Only supported picture child tags are accepted: <source> and <img>.</remarks>
        /// <param name="children">An array of supported picture child nodes implementing <see cref="IPictureContent"/>.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <picture> element.</returns>
        public static IHtmlNode Picture(params IPictureContent[] children)
            => new Element("picture", children);


        /// <summary>
        /// Creates a new HTML <iframe> element with the specified source URL and title attributes.
        /// </summary>
        /// <param name="src">The value to assign to the 'src' attribute of the <iframe> element. Represents the URL of the page to
        /// display. If not specified, the attribute will be set to an empty string.</param>
        /// <param name="title">The value to assign to the 'title' attribute of the <iframe> element. Used to provide a descriptive title
        /// for accessibility purposes. If not specified, the attribute will be set to an empty string.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <iframe> element with the specified attributes.</returns>
        public static IHtmlNode IFrame(string src = "", string title = "")
            => new Element("iframe", new IHtmlNode[0]).Attr("src", src).Attr("title", title);

        /// <summary>
        /// Creates a new HTML <embed> element with the specified source and MIME type attributes.
        /// </summary>
        /// <remarks>The <embed> element is used to embed external content, such as multimedia or
        /// interactive resources, into an HTML document. The caller is responsible for providing valid attribute values
        /// as required by the embedded content.</remarks>
        /// <param name="src">The value for the 'src' attribute, specifying the URL of the embedded resource. If not specified, the
        /// attribute will be empty.</param>
        /// <param name="type">The value for the 'type' attribute, specifying the MIME type of the embedded resource. If not specified, the
        /// attribute will be empty.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <embed> element with the specified attributes.</returns>
        public static IHtmlNode Embed(string src = "", string type = "")
            => new Element("embed", true).Attr("src", src).Attr("type", type);

        /// <summary>
        /// Creates a new HTML <object> element with the specified data and type attributes, containing optional child
        /// nodes.
        /// </summary>
        /// <param name="data">The URL of the resource to be used by the object element. If empty, the attribute is omitted.</param>
        /// <param name="type">The MIME type of the resource specified by <paramref name="data"/>. If empty, the attribute is omitted.</param>
        /// <param name="children">An array of child nodes to include within the object element, such as fallback content or <param>
        /// elements.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <object> element.</returns>
        public static IHtmlNode Object(string data = "", string type = "", params IHtmlNode[] children)
            => new Element("object", children).Attr("data", data).Attr("type", type);

        /// <summary>
        /// Creates a new HTML <param> element with the specified name and value attributes.
        /// </summary>
        /// <param name="name">The parameter name to assign to the 'name' attribute.</param>
        /// <param name="value">The parameter value to assign to the 'value' attribute.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing a self-closing <param> element.</returns>
        public static IHtmlNode Param(string name = "", string value = "")
            => new Element("param", true).Attr("name", name).Attr("value", value);

        /// <summary>
        /// Creates a new HTML <data> element with the specified value attribute and child nodes.
        /// </summary>
        /// <param name="value">The machine-readable value to assign to the 'value' attribute.</param>
        /// <param name="children">An array of child nodes to include as the display content of the data element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <data> element.</returns>
        public static IHtmlNode Data(string value = "", params IHtmlNode[] children)
            => new Element("data", children).Attr("value", value);

        /// <summary>
        /// Creates a new HTML <meter> element with value range attributes and optional child nodes.
        /// </summary>
        /// <param name="value">The current numeric value of the meter.</param>
        /// <param name="min">The minimum numeric value of the meter range.</param>
        /// <param name="max">The maximum numeric value of the meter range.</param>
        /// <param name="children">An array of child nodes to include as fallback or descriptive content.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <meter> element.</returns>
        public static IHtmlNode Meter(string value = "", string min = "", string max = "", params IHtmlNode[] children)
            => new Element("meter", children)
                .Attr("value", value)
                .Attr("min", min)
                .Attr("max", max);

        /// <summary>
        /// Creates a new HTML <progress> element with the specified value and max attributes.
        /// </summary>
        /// <param name="value">The current progress value.</param>
        /// <param name="max">The maximum progress value.</param>
        /// <param name="children">An array of child nodes to include as fallback content.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <progress> element.</returns>
        public static IHtmlNode Progress(string value = "", string max = "", params IHtmlNode[] children)
            => new Element("progress", children)
                .Attr("value", value)
                .Attr("max", max);

        /// <summary>
        /// Creates a new HTML <dl> (description list) element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes, typically containing <dt> and <dd> elements.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <dl> element.</returns>
        public static IHtmlNode Dl(params IHtmlNode[] children)
            => new Element("dl", children);

        /// <summary>
        /// Creates a new HTML <dt> (description term) element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the term element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <dt> element.</returns>
        public static IHtmlNode Dt(params IHtmlNode[] children)
            => new Element("dt", children);

        /// <summary>
        /// Creates a new HTML <dd> (description details) element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the details element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <dd> element.</returns>
        public static IHtmlNode Dd(params IHtmlNode[] children)
            => new Element("dd", children);

        /// <summary>
        /// Creates a new HTML <fieldset> element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the fieldset.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <fieldset> element.</returns>
        public static IHtmlNode Fieldset(params IHtmlNode[] children)
            => new Element("fieldset", children);

        /// <summary>
        /// Creates a new HTML <legend> element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the legend.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <legend> element.</returns>
        public static IHtmlNode Legend(params IHtmlNode[] children)
            => new Element("legend", children);

        /// <summary>
        /// Creates a new HTML <select> element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes, typically containing <option> or <optgroup> elements.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <select> element.</returns>
        public static IHtmlNode Select(params IHtmlNode[] children)
            => new Element("select", children);

        /// <summary>
        /// Creates a new HTML <option> element with the specified value attribute and child nodes.
        /// </summary>
        /// <param name="value">The machine-readable value for the option.</param>
        /// <param name="children">An array of child nodes representing the display text/content of the option.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <option> element.</returns>
        public static IHtmlNode Option(string value = "", params IHtmlNode[] children)
            => new Element("option", children).Attr("value", value);

        /// <summary>
        /// Creates a new HTML <optgroup> element with the specified label and child option nodes.
        /// </summary>
        /// <param name="label">The group label to assign to the 'label' attribute.</param>
        /// <param name="children">An array of child nodes, typically containing <option> elements.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <optgroup> element.</returns>
        public static IHtmlNode OptGroup(string label = "", params IHtmlNode[] children)
            => new Element("optgroup", children).Attr("label", label);

        /// <summary>
        /// Creates a new HTML <datalist> element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes, typically containing <option> elements.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <datalist> element.</returns>
        public static IHtmlNode Datalist(params IHtmlNode[] children)
            => new Element("datalist", children);

        /// <summary>
        /// Creates a new HTML <output> element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the output element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <output> element.</returns>
        public static IHtmlNode Output(params IHtmlNode[] children)
            => new Element("output", children);

        /// <summary>
        /// Creates a new HTML <hr> (thematic break) element node.
        /// </summary>
        /// <returns>An <see cref="IHtmlNode"/> representing a self-closing <hr> element.</returns>
        public static IHtmlNode Hr()
            => new Element("hr", true);

        /// <summary>
        /// Creates a heading element (<h1> through <h6>) with the specified child nodes.
        /// </summary>
        /// <param name="heading">The heading level to generate.</param>
        /// <param name="children">An array of child nodes to include within the heading element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed heading element.</returns>
        public static IHtmlNode Heading(Heading heading, params IHtmlNode[] children)
            => new Element(heading.ToTag(), children);

        /// <summary>
        /// Creates a new HTML <form> element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the form.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <form> element.</returns>
        public static IHtmlNode Form(params IHtmlNode[] children)
            => new Element("form", children);

        /// <summary>
        /// Creates a new HTML <ul> (unordered list) element with the specified list item children.
        /// </summary>
        /// <param name="children">An array of list elements to include in the unordered list.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <ul> element.</returns>
        public static IHtmlNode Ul(params IListElement[] children)
            => new Ul(children);

        /// <summary>
        /// Creates a new HTML <ol> (ordered list) element with the specified list item children.
        /// </summary>
        /// <param name="children">An array of list elements to include in the ordered list.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <ol> element.</returns>
        public static IHtmlNode Ol(params IListElement[] children)
            => new Ol(children);

        /// <summary>
        /// Creates a new HTML <li> (list item) element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the list item.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <li> element.</returns>
        public static IHtmlNode Li(params IHtmlNode[] children)
            => new Li(children);

        /// <summary>
        /// Creates a new HTML <nav> element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the navigation element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <nav> element.</returns>
        public static IHtmlNode Nav(params IHtmlNode[] children)
            => new Element("nav", children);

        /// <summary>
        /// Creates a new HTML <main> element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the main content element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <main> element.</returns>
        public static IHtmlNode Main(params IHtmlNode[] children)
            => new Element("main", children);

        /// <summary>
        /// Creates a new HTML <footer> element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the footer element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <footer> element.</returns>
        public static IHtmlNode Footer(params IHtmlNode[] children)
            => new Element("footer", children);

        /// <summary>
        /// Creates a new HTML <header> element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the header element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <header> element.</returns>
        public static IHtmlNode Header(params IHtmlNode[] children)
            => new Element("header", children);

        /// <summary>
        /// Creates a new HTML <search> element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the search element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <search> element.</returns>
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
        /// <param name="children">An array of child nodes, typically including <head> and <body> elements.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed HTML document.</returns>
        public static IHtmlNode HtmlDocument(params IHtmlNode[] children)
            => new HtmlDocument(children);

        /// <summary>
        /// Creates a new HTML <body> element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the body element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <body> element.</returns>
        public static IHtmlNode Body(params IHtmlNode[] children)
            => new Element("body", children);

        /// <summary>
        /// Creates a new HTML <head> element with the specified child nodes.
        /// </summary>
        /// <param name="children">An array of child nodes to include within the head element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <head> element.</returns>
        public static IHtmlNode Head(params IHtmlNode[] children)
            => new Element("head", children);

        /// <summary>
        /// Creates a new HTML <title> element containing the specified text.
        /// </summary>
        /// <param name="text">The title text to include in the element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <title> element.</returns>
        public static IHtmlNode Title(string text)
            => new Element("title", new TextNode(text));

        /// <summary>
        /// Creates a new HTML <meta> element with the specified name and content attributes.
        /// </summary>
        /// <param name="name">The metadata name.</param>
        /// <param name="content">The metadata content value.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing a self-closing <meta> element.</returns>
        public static IHtmlNode Meta(string name, string content)
            => new Element("meta", true).Attr("name", name).Attr("content", content);

        /// <summary>
        /// Creates a new HTML <link> element with the specified href and rel attributes.
        /// </summary>
        /// <param name="href">The resource URL to assign to the href attribute.</param>
        /// <param name="rel">The relationship type to assign to the rel attribute.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing a self-closing <link> element.</returns>
        public static IHtmlNode Link(string href, string rel = "stylesheet")
            => new Element("link", true).Attr("href", href).Attr("rel", rel);

        /// <summary>
        /// Creates a new HTML <style> element containing the specified CSS content.
        /// </summary>
        /// <param name="style">The CSS text to include in the style element.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <style> element.</returns>
        public static IHtmlNode Style(string style) => new Element("style", new TextNode(style));

        /// <summary>
        /// Creates a new HTML <script> element with inline script content and optional src attribute.
        /// </summary>
        /// <param name="script">The inline JavaScript content.</param>
        /// <param name="src">The external script URL to assign to the src attribute.</param>
        /// <returns>An <see cref="IHtmlNode"/> representing the constructed <script> element.</returns>
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