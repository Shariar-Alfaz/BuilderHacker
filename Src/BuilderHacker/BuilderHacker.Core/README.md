# SAProduction.BuilderHacker.Core

`SAProduction.BuilderHacker.Core` provides two main capabilities:

1. **EntityBuilder<T>** (runtime, reflection-based)
2. **HTML Builder (UI)** (fluent HTML composition with type-safe child validation for table/media)

---

## Install

```powershell
Install-Package SAProduction.BuilderHacker.Core
```

---

## Supported Targets

- .NET Standard 2.0+
- .NET Core / .NET 5+
- .NET Framework (via compatible target graphs)

---

## 1) EntityBuilder<T>

A runtime builder for dynamic object construction.

### Features

- Fluent API
- Set by expression (`Set(x => x.Name, value)`) for compile-time safety
- Set by property/field name (`Set("Name", value)`) for dynamic scenarios
- Optional strict mode (property-only assignments)
- Reflection member caching for better repeated performance

### Example (Expression-based)

```csharp
using BuilderHacker.Core.EntityBuilder;

var user = EntityBuilder<User>.Create()
    .Set(x => x.Name, "Alice")
    .Set(x => x.Age, 30)
    .Build();
```

### Example (String-based)

```csharp
var user = EntityBuilder<User>.Create()
    .Set("Name", "Bob")
    .Set("Age", 25)
    .Build();
```

### Strict mode

```csharp
var user = EntityBuilder<User>.Create()
    .StrictMode(true)
    .Set(x => x.Name, "Safe")
    .Build();
```

---

## 2) HTML Builder (UI)

Use `BuilderHacker.Core.HtmlBuilder.UI` to create HTML trees fluently.

### Highlights

- Broad HTML tag coverage (inline, semantic, form, media, embed, table)
- Type-safe composition for tags that require specific children
- Render to string with `Render()`

### Type-safe composition rules

#### Table

- `UI.Tr(...)` returns `ITableRow`
- `UI.THead(...)`, `UI.TBody(...)`, `UI.TFoot(...)` accept only `ITableRow[]`

#### Media

- `UI.Video(...)` accepts only `IVideoContent` children (`UI.Source`, `UI.Track`)
- `UI.Audio(...)` accepts only `IAudioContent` children (`UI.Source`, `UI.Track`)
- `UI.Picture(...)` accepts only `IPictureContent` children (`UI.Source`, `UI.Img`)

### Full table example

```csharp
using BuilderHacker.Core.HtmlBuilder;

var table = UI.Table(
    UI.TableCaption(UI.TextNode("Employee Directory")),
    UI.THead(UI.Tr(UI.Th("Name"), UI.Th("Age"), UI.Th("Department"))),
    UI.TBody(
        UI.Tr(UI.Td("Alice"), UI.Td("31"), UI.Td("Engineering")),
        UI.Tr(UI.Td("Bob"), UI.Td("28"), UI.Td("Product"))
    ),
    UI.TFoot(UI.Tr(UI.Td("Total"), UI.Td("2"), UI.Td("Employees")))
);

string html = table.Render();
```

### Video example

```csharp
var video = UI.Video(
    "video.mp4",
    UI.Source("video.webm", "video/webm"),
    UI.Track("subtitles", "captions.vtt", "en", "English")
);

string html = video.Render();
```

### Picture example

```csharp
var picture = UI.Picture(
    UI.Source("banner.webp", "image/webp"),
    UI.Img("banner.png", "Banner")
);
```

---

## 3) Builder Factory Pattern

Use `IBuilderHackerFactory` when you want to resolve builders from dependency injection.

### What it does

- Resolves `IBuilder<T>` instances from an `IServiceProvider`
- Resolves a specific `TBuilder` when you know the generated builder type
- Falls back to `IBuilder<T>` if the concrete builder type is not registered

### Typical usage

```csharp
services.AddSingleton<IBuilderHackerFactory, DefaultBuilderHackerFactory>();
services.AddTransient<IBuilder<SimpleUser>, SimpleUserBuilder>();

var factory = serviceProvider.GetRequiredService<IBuilderHackerFactory>();
var builder = factory.CreateBuilder<SimpleUser>();
var user = builder.Name("Sam").Age(25).Build();
```

### Documentation

- Full factory guide: https://shariar-alfaz.github.io/BuilderHacker/Src/BuilderHacker/FACTORY.html

---

## Thread Safety

`EntityBuilder<T>` instances are **not thread-safe**. Use one builder instance per thread/request.

---

## Related Packages

- `SAProduction.BuilderHacker.Abstraction`
- `SAProduction.BuilderHacker.Generator`

---

## Source & Issues

- Repository: https://github.com/Shariar-Alfaz/BuilderHacker
- Documentation: https://shariar-alfaz.github.io/BuilderHacker/
