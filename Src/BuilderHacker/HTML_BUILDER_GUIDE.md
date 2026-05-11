# HTML Builder Guide

This guide covers the new HTML Builder capabilities in `BuilderHacker.Core.HtmlBuilder.UI`.

## What’s New

## 1) Broader Tag Coverage
The UI now includes many additional semantic and media/form tags, including:

- Inline/text: `Em`, `Small`, `Mark`, `Del`, `Ins`, `S`, `Abbr`, `Time`, `Dfn`, `Kbd`, `Samp`, `Var`, `Q`, `Cite`, `Wbr`, `Pre`
- Structure/content: `Figure`, `FigCaption`, `Details`, `Summary`, `Dialog`, `Search`
- Media/embed: `Audio`, `Video`, `Source`, `Track`, `Picture`, `IFrame`, `Embed`, `Object`, `Param`
- Data/form: `Data`, `Meter`, `Progress`, `Dl`, `Dt`, `Dd`, `Fieldset`, `Legend`, `Select`, `Option`, `OptGroup`, `Datalist`, `Output`

## 2) Type-Safe Child Validation (Interface-Based)
Like table composition, media composition now uses interfaces to restrict valid child tags.

### Table
- `ITableRow : IBaseTable`
- `Tr(...)` returns `ITableRow`
- `THead(...)`, `TBody(...)`, `TFoot(...)` accept only `ITableRow[]`

### Media
- `IAudioContent : IHtmlNode`
- `IVideoContent : IHtmlNode`
- `IPictureContent : IHtmlNode`
- `ISourceContent : IVideoContent, IAudioContent, IPictureContent`
- `ITrackContent : IVideoContent, IAudioContent`

This means:
- `Video(...)` accepts only `IVideoContent` (`Source`, `Track`)
- `Audio(...)` accepts only `IAudioContent` (`Source`, `Track`)
- `Picture(...)` accepts only `IPictureContent` (`Source`, `Img`)

---

## Usage Examples

## Full Table (Type-Safe)
```csharp
using BuilderHacker.Core.HtmlBuilder;

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

## Video with Valid Children Only
```csharp
var video = UI.Video(
    "video.mp4",
    UI.Source("video.webm", "video/webm"),
    UI.Track("subtitles", "captions.vtt", "en", "English")
);

string html = video.Render();
```

## Audio with Valid Children Only
```csharp
var audio = UI.Audio(
    "audio.mp3",
    UI.Source("audio.ogg", "audio/ogg"),
    UI.Track("captions", "audio.vtt", "en", "English")
);

string html = audio.Render();
```

## Picture with Valid Children Only
```csharp
var picture = UI.Picture(
    UI.Source("banner.webp", "image/webp"),
    UI.Img("banner.png", "Main banner")
);

string html = picture.Render();
```

## Semantic + Form Example
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

string html = form.Render();
```

---

## Why This Design Is Useful

- **Compile-time guidance**: invalid child compositions are prevented by method signatures.
- **Cleaner API contracts**: each container advertises what children it supports.
- **Safer refactoring**: interface boundaries reduce accidental misuse.
- **Consistent model**: table and media now follow the same typed composition principle.

---

## Test Status

HTML Builder tests include full rendering and composition scenarios.

- HTML builder tests passing: **71/71**
- Build status: **Success**
