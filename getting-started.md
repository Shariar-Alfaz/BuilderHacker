# Getting Started with BuilderHacker

## Installation

### Step 1: Install NuGet Package

Choose based on your needs:

**For source generation (recommended for .NET 6+):**
```bash
dotnet add package SAProduction.BuilderHacker.Generator
```

**For runtime builders (all frameworks):**
```bash
dotnet add package SAProduction.BuilderHacker.Core
```

**For both:**
```bash
dotnet add package SAProduction.BuilderHacker.Generator
dotnet add package SAProduction.BuilderHacker.Core
```

**Current Version:** 1.1.0

## Your First Builder

### Option 1: Source Generator (Best Performance)

**Step 1: Create your class**
```csharp
using BuilderHacker.Abstraction.Attributes;

[GenerateBuilderHacker]
public partial class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
}
```

**Step 2: Use the generated builder**
```csharp
// Default (standalone) generated builder (default: Create())
var product = ProductBuilder.Create()
    .Name("Laptop")
    .Price(999.99m)
    .Description("High-performance laptop")
    .Build();

// Or, if you use the partial-mode flag: [GenerateBuilderHacker(true)]
// the generator generates a nested Builder() method on your type:
// [GenerateBuilderHacker(true)]
// public partial class Product { /* ... */ }
// Usage:
// var product = Product.Builder().Name(...).Price(...).Build();
```

That's it! The builder was generated at compile-time.

### Option 2: Runtime Builder (Universal)

**Step 1: No attribute needed**
```csharp
public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
}
```

**Step 2: Use EntityBuilder<T>**
```csharp
using BuilderHacker.Core;

var product = EntityBuilder<Product>.Create()
    .Set(p => p.Name, "Laptop")
    .Set(p => p.Price, 999.99m)
    .Set(p => p.Description, "High-performance laptop")
    .Build();
```

## Handling Complex Scenarios

### Private Setters
```csharp
[GenerateBuilderHacker]
public partial class User
{
    public string Email { get; private set; }  // Private setter
    
    public User(string email)
    {
        Email = email;
    }
}

// Builder still works!
// Default generated standalone builder usage:
var user = UserBuilder.Create()
    .Email("user@example.com")
    .Build();

// If you generated with partial-mode ([GenerateBuilderHacker(true)]),
// use `User.Builder()` instead:
// var user = User.Builder().Email("user@example.com").Build();
```

### Inherited Properties
```csharp
public abstract class Entity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
}

[GenerateBuilderHacker]
public partial class Article : Entity
{
    public string Title { get; set; }
    public string Content { get; set; }
}

// Builder includes inherited properties
// Default standalone mode:
var article = ArticleBuilder.Create()
    .Id(1)
    .CreatedAt(DateTime.Now)
    .Title("My Article")
    .Content("Article content")
    .Build();

// Partial-mode usage (if you used [GenerateBuilderHacker(true)]):
// var article = Article.Builder()...Build();
```

## Common Patterns

### Builder with Default Values
```csharp
[GenerateBuilderHacker]
public partial class Config
{
    public string Environment { get; set; } = "Production";
    public int Timeout { get; set; } = 30;
    public bool Debug { get; set; } = false;
}

// Override defaults as needed
var config = ConfigBuilder.Create()
    .Environment("Development")
    .Debug(true)
    .Build();
```

### Partial Class Entry Point
```csharp
[GenerateBuilderHacker(true)]  // true = generate in partial class
public partial class Settings
{
    public string ApiKey { get; set; }
    public int MaxRetries { get; set; }
    
    public static Settings CreateDefault() => Builder()
        .ApiKey("key123")
        .MaxRetries(3)
        .Build();
}
```

## Troubleshooting

**Builder not generated?**
- Ensure the class is marked `public`
- Ensure the class is marked `partial`
- Check that `[GenerateBuilderHacker]` attribute is present
- Rebuild the project (VS: Clean + Build)

**Can't access private properties?**
- BuilderHacker supports private setters and private fields
- Public getters are still required
- The builder will expose all members (even private ones)

## Framework Compatibility

| Approach | .NET Framework | .NET Core | .NET 5 | .NET 6+ |
|----------|---|---|---|---|
| **Source Generator** | ❌ | ❌ | ❌ | ✅ |
| **EntityBuilder<T>** | ✅ | ✅ | ✅ | ✅ |

## What's Next?

- **[Quick Reference](Src/BuilderHacker/QUICK_REFERENCE.md)** - Common patterns at a glance
- **[Full Documentation](Src/BuilderHacker/SUMMARY.md)** - Deep dive into features
- **[Development Guidelines](Src/BuilderHacker/DEVELOPMENT_GUIDELINES.md)** - Best practices
- **[Architecture](Src/BuilderHacker/ARCHITECTURE_DIAGRAM.md)** - How it all works