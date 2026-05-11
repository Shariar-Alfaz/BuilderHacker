# SAProduction.BuilderHacker.Abstraction

Core abstractions for the BuilderHacker ecosystem.

This package contains shared contracts used by:

- `SAProduction.BuilderHacker.Core`
- `SAProduction.BuilderHacker.Generator`

---

## Install

```powershell
Install-Package SAProduction.BuilderHacker.Abstraction
```

---

## What This Package Provides

### Builder contracts

- `IBuilder<T>`
- `IBuilderHackerFactory`
- `[GenerateBuilderHacker]` attribute

### HTML builder contracts

- `IHtmlNode`
- Table contracts: `IBaseTable`, `ITableRow`, `IThOrTd`, `ITableColumnDef`
- List contracts: `IListElement`
- Media contracts: `IAudioContent`, `IVideoContent`, `IPictureContent`, `ISourceContent`, `ITrackContent`

These interfaces enable type-safe composition in the Core HTML builder API.

---

## Typical Usage

You usually reference this package indirectly via Core/Generator. If needed directly:

```csharp
using BuilderHacker.Abstraction.Attributes;

[GenerateBuilderHacker]
public partial class User
{
    public string Name { get; set; }
}
```

---

## Source & Issues

- Repository: https://github.com/Shariar-Alfaz/BuilderHacker
