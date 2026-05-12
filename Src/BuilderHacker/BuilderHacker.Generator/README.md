# SAProduction.BuilderHacker.Generator

Roslyn incremental source generator for BuilderHacker.

Generates fluent builders at compile time for classes marked with `[GenerateBuilderHacker]`.

---

## Install

```powershell
Install-Package SAProduction.BuilderHacker.Generator
Install-Package SAProduction.BuilderHacker.Abstraction
```

---

## Features

- Incremental source generation
- Fluent builder generation
- Supports standalone and partial generation modes
- Handles inheritance and property shadowing scenarios
- Zero runtime reflection overhead for generated builders

---

## Quick Start

```csharp
using BuilderHacker.Abstraction.Attributes;

[GenerateBuilderHacker]
public partial class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

After build, use generated API:

```csharp
// Default generated standalone builder (Create() by default):
var product = ProductBuilder.Create()
    .Name("Widget")
    .Price(19.99m)
    .Build();

// Partial-mode alternative (use `[GenerateBuilderHacker(true)]`):
// var product = Product.Builder().Name("Widget").Price(19.99m).Build();
```

---

## Notes

- Works at compile time through analyzer/source-generator pipeline.
- Pair with `SAProduction.BuilderHacker.Core` when you need runtime `EntityBuilder<T>` and HTML builder utilities.

---

## Builder Factory Pattern

If you resolve builders through dependency injection, see the factory guide:

- Full factory guide: https://shariar-alfaz.github.io/BuilderHacker/Src/BuilderHacker/FACTORY.html

This is the recommended pattern when you want to keep builder creation centralized and testable.

---

## Source & Issues

- Repository: https://github.com/Shariar-Alfaz/BuilderHacker
- Documentation: https://shariar-alfaz.github.io/BuilderHacker/
