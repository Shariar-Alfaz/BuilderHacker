# Introduction to BuilderHacker

## What is BuilderHacker?

BuilderHacker is a modern .NET library that provides **fluent, type-safe builders** for constructing complex objects with ease. It offers two complementary approaches:

1. **Source Generation** (Compile-time) - For high performance with .NET 6+
2. **Reflection-based Runtime** (EntityBuilder<T>) - For universal compatibility

## Why Use Builders?

Builders solve the common challenges of object construction:

- **Immutable objects** - Build objects that cannot be modified after creation
- **Complex initialization** - Fluent API makes building objects readable and maintainable
- **Default values** - Builders can manage sensible defaults elegantly
- **Type safety** - Compile-time checking ensures correct usage

## Key Features

✨ **Generates fluent builders** from a single `[GenerateBuilderHacker]` attribute

🔒 **Supports private members** - Works with private fields and private setters

🎯 **Handles inheritance** - Traverses and includes inherited instance members

🔄 **Dual approach** - Choose source generation or runtime reflection

⚡ **Zero-cost** - Generated source code produces no reflection overhead

## How It Works

### Source Generator Approach (Recommended for .NET 6+)

```csharp
[GenerateBuilderHacker]
public partial class User
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}

// Generated at compile-time - no reflection!
// Default generated standalone builder (Create() by default):
var user = UserBuilder.Create()
    .Name("Alice")
    .Age(30)
    .Email("alice@example.com")
    .Build();

// If you used `[GenerateBuilderHacker(true)]` (partial-mode), you can use:
// var user = User.Builder().Name(...).Age(...).Build();
```

### Runtime Approach (Works Everywhere)

```csharp
var user = EntityBuilder<User>.Create()
    .Set(u => u.Name, "Alice")
    .Set(u => u.Age, 30)
    .Set(u => u.Email, "alice@example.com")
    .Build();
```

## NuGet Packages

Available on NuGet:
- **[SAProduction.BuilderHacker.Abstraction](https://www.nuget.org/packages/SAProduction.BuilderHacker.Abstraction/)** - Shared attributes and interfaces
- **[SAProduction.BuilderHacker.Core](https://www.nuget.org/packages/SAProduction.BuilderHacker.Core/)** - Runtime EntityBuilder<T> implementation
- **[SAProduction.BuilderHacker.Generator](https://www.nuget.org/packages/SAProduction.BuilderHacker.Generator/)** - Source generator for attribute-based builders

## Project Structure

| Project | Package ID | Purpose | Framework |
|---------|-----------|---------|-----------|
| **BuilderHacker.Abstraction** | SAProduction.BuilderHacker.Abstraction | Shared attributes and interfaces | netstandard2.0 |
| **BuilderHacker.Core** | SAProduction.BuilderHacker.Core | Runtime EntityBuilder<T> implementation | netstandard2.0 |
| **BuilderHacker.Generator** | SAProduction.BuilderHacker.Generator | Incremental source generator | netstandard2.0 |
| **BuilderHacker.Console** | (Sample only) | Example application demonstrating usage | net10.0 |

## Next Steps

- **[Getting Started](getting-started.md)** - Install and create your first builder
- **[Quick Reference](Src/BuilderHacker/QUICK_REFERENCE.md)** - API quick lookup
- **[Full Summary](Src/BuilderHacker/SUMMARY.md)** - Complete technical details