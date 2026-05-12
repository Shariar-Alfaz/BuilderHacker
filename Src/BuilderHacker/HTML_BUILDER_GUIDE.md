# HTML Builder Guide

Back to the [home page](../../index.md).

This guide documents the current implementation of `BuilderHacker.Core.HtmlBuilder.UI` and the underlying HTML node model.

## Overview

`UI` is a static factory for building HTML nodes in a fluent, type-safe style. Every helper returns an `IHtmlNode` or a more specific interface such as `IBaseTable`, `ITableRow`, `IAudioContent`, or `IPictureContent`.

The rendering model is simple:

- nodes are created through `UI`
- attributes are assigned fluently with `Attr`, `Style`, and `Class`
- `Render()` returns the final HTML string
- self-closing nodes are rendered as `<tag ... />`

## Core Concepts

### Node API

The shared node contract is `IHtmlNode`, which exposes:

- `Attr(string key, string value)`
- `Style(string value)`
- `Class(string value)`
- `Render()`

### Validation Rules

The implementation validates common HTML metadata before rendering:

- attribute names must match the supported HTML attribute pattern
- `Class(...)` rejects invalid class syntax
- `Style(...)` rejects invalid CSS declaration syntax
- null attribute values are not accepted

### Rendering Model

The base `Element` renderer emits either:

- `<tag ...>children</tag>` for normal elements
- `<tag ... />` for self-closing elements

That means methods such as `Br`, `Hr`, `Meta`, `Link`, `Input`, `Img`, `Embed`, and `Param` are rendered as self-closing nodes, while content elements such as `Div`, `Section`, `Article`, `Table`, and `Form` render with opening and closing tags.

## Type-Safe Composition

The implementation uses interfaces to constrain valid children at compile time.

### Table Composition

- `ITableRow : IBaseTable`
- `Tr(...)` returns `ITableRow`
- `THead(...)`, `TBody(...)`, and `TFoot(...)` accept only `ITableRow[]`
- `Table(...)` accepts `IBaseTable[]`
- `ColGroup(...)` accepts `ITableColumnDef[]`

### Media Composition

- `IAudioContent : IHtmlNode`
- `IVideoContent : IHtmlNode`
- `IPictureContent : IHtmlNode`
- `ISourceContent : IVideoContent, IAudioContent, IPictureContent`
- `ITrackContent : IVideoContent, IAudioContent`

This means:

- `Audio(...)` accepts only `IAudioContent` children, such as `Source(...)` and `Track(...)`
- `Video(...)` accepts only `IVideoContent` children, such as `Source(...)` and `Track(...)`
- `Picture(...)` accepts only `IPictureContent` children, such as `Source(...)` and `Img(...)`

## Quick Start

```csharp
using BuilderHacker.Core.HtmlBuilder;

var page = UI.HtmlDocument(
    UI.Head(
        UI.Title("BuilderHacker Demo"),
        UI.Meta("viewport", "width=device-width, initial-scale=1"),
        UI.Link("styles.css")
    ),
    UI.Body(
        UI.Header(UI.H1("Hello from BuilderHacker")),
        UI.Main(
            UI.P(UI.TextNode("This page was built with the HTML builder."))
        ),
        UI.Footer(UI.TextNode("© BuilderHacker"))
    )
);

string html = page.Render();
```

## Custom Tag Reference

Use this section as a tag catalog for the current API surface.

### Inline and Text Tags

| Method | HTML tag | Notes |
|---|---|---|
| `Span(...)` | `span` | Generic inline container |
| `A(...)` | `a` | Supports `href` and `target` |
| `B(...)` | `b` | Bold text |
| `I(...)` | `i` | Italic text |
| `U(...)` | `u` | Underlined text |
| `Em(...)` | `em` | Emphasis |
| `Small(...)` | `small` | Smaller text |
| `Mark(...)` | `mark` | Highlighted text |
| `Del(...)` | `del` | Deleted text |
| `Ins(...)` | `ins` | Inserted text |
| `S(...)` | `s` | Strikethrough text |
| `Abbr(...)` | `abbr` | Supports `title` |
| `Time(...)` | `time` | Supports `datetime` |
| `Dfn(...)` | `dfn` | Definition term |
| `Kbd(...)` | `kbd` | Keyboard input |
| `Samp(...)` | `samp` | Sample output |
| `Var(...)` | `var` | Variable name |
| `Q(...)` | `q` | Supports `cite` |
| `Cite(...)` | `cite` | Citation reference |
| `Br()` | `br` | Self-closing line break |
| `Wbr()` | `wbr` | Word break opportunity |
| `Button(...)` | `button` | Wraps text in a button |
| `Strong(...)` | `strong` | Strong emphasis |
| `Sub(...)` | `sub` | Subscript |
| `Sup(...)` | `sup` | Superscript |
| `Code(...)` | `code` | Inline code |
| `Pre(...)` | `pre` | Preformatted block |
| `Label(...)` | `label` | Label text |
| `TextArea(...)` | `textarea` | Text area content |
| `TextNode(...)` | text node | Raw text node |

### Structural Tags

| Method | HTML tag | Notes |
|---|---|---|
| `Div(...)` | `div` | Generic block container |
| `Section(...)` | `section` | Sectioning content |
| `P(...)` | `p` | Paragraph |
| `Address(...)` | `address` | Contact information |
| `Article(...)` | `article` | Article content |
| `Aside(...)` | `aside` | Supporting content |
| `Figure(...)` | `figure` | Figure container |
| `FigCaption(...)` | `figcaption` | Figure caption |
| `Details(...)` | `details` | Disclosure widget |
| `Summary(...)` | `summary` | Summary for details |
| `Dialog(...)` | `dialog` | Dialog element |
| `Blockquote(...)` | `blockquote` | Supports `cite` |
| `Canvas(...)` | `canvas` | Supports width and height |
| `Hr()` | `hr` | Self-closing thematic break |
| `Heading(...)` | `h1`-`h6` | Uses the `Heading` enum |
| `Nav(...)` | `nav` | Navigation container |
| `Main(...)` | `main` | Primary page content |
| `Header(...)` | `header` | Header content |
| `Footer(...)` | `footer` | Footer content |
| `Search(...)` | `search` | Search landmark |

### List Tags

| Method | HTML tag | Notes |
|---|---|---|
| `Ul(...)` | `ul` | Accepts `IListElement[]` |
| `Ol(...)` | `ol` | Accepts `IListElement[]` |
| `Li(...)` | `li` | List item |

### Form and Data Tags

| Method | HTML tag | Notes |
|---|---|---|
| `Form(...)` | `form` | Generic form container |
| `Fieldset(...)` | `fieldset` | Form grouping |
| `Legend(...)` | `legend` | Fieldset caption |
| `Input(...)` | `input` | Self-closing input |
| `Select(...)` | `select` | Accepts options and groups |
| `Option(...)` | `option` | Supports `value` |
| `OptGroup(...)` | `optgroup` | Supports `label` |
| `Datalist(...)` | `datalist` | Suggestion list |
| `Output(...)` | `output` | Form output value |
| `Data(...)` | `data` | Machine-readable value |
| `Meter(...)` | `meter` | Supports value range attributes |
| `Progress(...)` | `progress` | Progress indicator |
| `Dl(...)` | `dl` | Description list |
| `Dt(...)` | `dt` | Description term |
| `Dd(...)` | `dd` | Description details |

### Media and Embed Tags

| Method | HTML tag | Notes |
|---|---|---|
| `Audio(...)` | `audio` | Accepts `IAudioContent[]` |
| `Video(...)` | `video` | Accepts `IVideoContent[]` |
| `Source(...)` | `source` | Works in media/picture composition |
| `Track(...)` | `track` | Text tracks for media |
| `Picture(...)` | `picture` | Accepts `IPictureContent[]` |
| `Img(...)` | `img` | Image element |
| `IFrame(...)` | `iframe` | Supports `src` and `title` |
| `Embed(...)` | `embed` | Self-closing embed element |
| `Object(...)` | `object` | Supports `data` and `type` |
| `Param(...)` | `param` | Self-closing parameter element |

### Document Tags

| Method | HTML tag | Notes |
|---|---|---|
| `HtmlDocument(...)` | document root | Wraps a full HTML document |
| `HtmlDocument` children | `head` + `body` | Use `Head(...)` and `Body(...)` |
| `Title(...)` | `title` | Document title |
| `Meta(...)` | `meta` | Self-closing metadata tag |
| `Link(...)` | `link` | Self-closing external resource link |
| `Style(...)` | `style` | Inline CSS block |
| `Script(...)` | `script` | Inline or external script |
| `Custom(...)` | any tag | Creates an arbitrary custom element |

## Detailed Examples

### Full Table Example

```csharp
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
        UI.Tr(UI.Td("Alice"), UI.Td("31"), UI.Td("Engineering")),
        UI.Tr(UI.Td("Bob"), UI.Td("28"), UI.Td("Product"))
    ),
    UI.TFoot(
        UI.Tr(UI.Td("Total"), UI.Td("2"), UI.Td("Employees"))
    )
);

string html = table.Render();
```

### Media Composition Example

```csharp
var video = UI.Video(
    "video.mp4",
    UI.Source("video.webm", "video/webm"),
    UI.Track("subtitles", "captions.vtt", "en", "English")
);

var audio = UI.Audio(
    "audio.mp3",
    UI.Source("audio.ogg", "audio/ogg"),
    UI.Track("captions", "audio.vtt", "en", "English")
);

var picture = UI.Picture(
    UI.Source("banner.webp", "image/webp"),
    UI.Img("banner.png", "Main banner")
);
```

### Semantic Form Example

```csharp
var form = UI.Form(
    UI.Fieldset(
        UI.Legend(UI.TextNode("Profile")),
        UI.Label("Name"),
        UI.Input("text", "Alice"),
        UI.Select(
            UI.OptGroup("Group A", UI.Option("1", UI.TextNode("One"))),
            UI.Option("2", UI.TextNode("Two"))
        ),
        UI.Output(UI.TextNode("Ready"))
    )
);
```

### Custom Tag Example

```csharp
var badge = UI.Custom(
    "span",
    children: new[] { UI.TextNode("New") }
).Class("badge badge-success");

var spacer = UI.Custom("hr", isSelfClosing: true);
```

### Attribute and Style Example

```csharp
var card = UI.Div(
    UI.Heading(Heading.H3, UI.TextNode("Card title")),
    UI.P(UI.TextNode("Card body"))
)
.Class("card shadow")
.Style("padding: 1rem; border-radius: 0.5rem;")
.Attr("data-role", "preview");
```

## Why This Design Is Useful

- **Compile-time guidance**: invalid table and media compositions are prevented by method signatures.
- **Clearer API contracts**: the return types show what each builder can participate in.
- **Safer refactoring**: the interface boundaries reduce accidental misuse.
- **Predictable rendering**: each node type renders through the same node model and validation rules.

## Related Documentation

- [Quick Reference](QUICK_REFERENCE.md)
- [README Index](README_INDEX.md)
- [Development Guidelines](DEVELOPMENT_GUIDELINES.md)

## Test Status

HTML Builder tests include rendering, attribute validation, table composition, media composition, and custom element scenarios.

- HTML builder tests passing: **71/71**
- Build status: **Success**
