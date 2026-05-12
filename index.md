---
_layout: landing
---


<img src="Src/logo.png" alt="BuilderHacker logo" width="160" />

# BuilderHacker - Advanced .NET Builder Generation & Runtime

A production-ready .NET library that provides **fluent, type-safe builders** with two complementary approaches: compile-time source generation and universal reflection-based runtime builders.

## Why BuilderHacker?

✨ **Two Builder Approaches**
- Source generation for .NET 6+ (zero-reflection, maximum performance)
- Runtime reflection for universal .NET compatibility (4.5+, Core, Framework)

🏗️ **Enterprise Architecture**
- Separated concerns: Generator isolated from Core
- Multi-framework by design (netstandard2.0)
- Production-ready with 67+ passing tests
- Comprehensive security & performance audits

🔒 **Battle-Tested**
- Private field & setter support
- Inheritance traversal
- HTML builder for UI generation
- Zero-cost abstraction with generated code

## Quick Links

### 👤 I Want To...

| Goal | Resource | Time |
|------|----------|------|
| **Start using it now** | [Getting Started](getting-started.md) | 5 min |
| **Understand the design** | [Introduction](introduction.md) | 10 min |
| **See code examples** | [Quick Reference](Src/BuilderHacker/QUICK_REFERENCE.md) | 5 min |
| **Understand architecture** | [Architecture Diagram](Src/BuilderHacker/ARCHITECTURE_DIAGRAM.md) | 15 min |
| **Read the full story** | [Executive Summary](Src/BuilderHacker/SUMMARY.md) | 20 min |
| **Implement multi-framework** | [Multi-Framework Roadmap](Src/BuilderHacker/MULTIFRAMEWORK_ROADMAP.md) | 30 min |
| **Learn best practices** | [Development Guidelines](Src/BuilderHacker/DEVELOPMENT_GUIDELINES.md) | 25 min |
| **Run the tests** | [Test Documentation](Src/BuilderHacker/BuilderHacker.Tests/INDEX.md) | 20 min |

## Key Features at a Glance

### 🚀 Source Generation (Compile-Time, .NET 6+)
```csharp
[GenerateBuilderHacker]
public partial class User
{
    public string Name { get; set; }
    public int Age { get; set; }
}

// Default generated standalone builder (default: `Create()`):
var user = UserBuilder.Create()
    .Name("Alice")
    .Age(30)
    .Build();

// If you generate with partial-mode (`[GenerateBuilderHacker(true)]`),
// the generator emits a nested `Builder()` method on your type:
// var user = User.Builder().Name("Alice").Age(30).Build();
```

### 🔄 Runtime Builder (All Frameworks)
```csharp
var user = EntityBuilder<User>.Create()
    .Set(u => u.Name, "Alice")
    .Set(u => u.Age, 30)
    .Build();
```

### 🎨 HTML Builder (New!)
```csharp
var html = UI.Div(
    UI.Span(UI.TextNode("Hello")),
    UI.Br(),
    UI.Span(UI.TextNode("World"))
)
.Class("container")
.Render();
```

### 🏭 Factory Pattern (DI-friendly)
BuilderHacker exposes a small factory abstraction `IBuilderHackerFactory` to resolve builders from an IoC container at runtime.

```csharp
// Interface (in library)
public interface IBuilderHackerFactory
{
    IBuilder<T> CreateBuilder<T>();
    TBuilder CreateBuilder<T, TBuilder>() where TBuilder : IBuilder<T>;
}

// Typical DI registration (Program.cs / Startup)
services.AddSingleton<IBuilderHackerFactory, DefaultBuilderHackerFactory>();
services.AddTransient<IBuilder<SimpleUser>, SimpleUserBuilder>();
// or register the concrete builder
services.AddTransient<SimpleUserBuilder>();

// Resolve by model type (generic)
var factory = serviceProvider.GetRequiredService<IBuilderHackerFactory>();
var builder = factory.CreateBuilder<SimpleUser>();
var user = builder.Name("Sam").Age(25).Build();

// Resolve concrete builder type
var typed = factory.CreateBuilder<SimpleUser, SimpleUserBuilder>();
var user2 = typed.Name("Sam").Age(25).Build();
```

- Register generated or handwritten builders so `DefaultBuilderHackerFactory` can resolve them.
- `CreateBuilder<T, TBuilder>()` prefers the concrete `TBuilder` registration and falls back to resolving `IBuilder<T>` if needed.

## Project Structure

```
BuilderHacker/
├── BuilderHacker.Abstraction    [netstandard2.0]
│   └── Shared interfaces & attributes
├── BuilderHacker.Core           [netstandard2.0]
│   └── Runtime EntityBuilder<T>
├── BuilderHacker.Generator      [netstandard2.0]
│   └── Source generator (Roslyn-based)
├── BuilderHacker.Console        [net10.0]
│   └── Example application
└── Documentation
    ├── Executive Summaries
    ├── Architecture Guides
    ├── Development Guidelines
    └── API References
```

## Framework Support

| Framework | Source Generator | Runtime Builder |
|-----------|------------------|-----------------|
| .NET Framework 4.5+ | ❌ | ✅ |
| .NET Core 2.0-3.1 | ❌ | ✅ |
| .NET 5.0 | ❌ | ✅ |
| .NET 6.0+ | ✅ | ✅ |
| .NET 10.0 | ✅ | ✅ |

## Documentation

### 📚 Core Documentation
- **[Introduction](introduction.md)** - Project overview and capabilities
- **[Getting Started](getting-started.md)** - Installation and first steps
- **[Project README](README.md)** - Full project details

### 🔧 Technical Documentation
- **[Quick Reference](Src/BuilderHacker/QUICK_REFERENCE.md)** - Usage examples & patterns
- **[Architecture Diagram](Src/BuilderHacker/ARCHITECTURE_DIAGRAM.md)** - System design & diagrams
- **[Executive Summary](Src/BuilderHacker/SUMMARY.md)** - Optimization & changes
- **[Optimization Details](Src/BuilderHacker/OPTIMIZATION_SUMMARY.md)** - Technical deep dive

### 🛠️ Advanced Topics
- **[Development Guidelines](Src/BuilderHacker/DEVELOPMENT_GUIDELINES.md)** - Best practices & patterns
- **[Multi-Framework Roadmap](Src/BuilderHacker/MULTIFRAMEWORK_ROADMAP.md)** - Implementation plan
- **[HTML Builder Guide](Src/BuilderHacker/HTML_BUILDER_GUIDE.md)** - UI generation

### ✅ Quality & Testing
- **[Completion Report](Src/BuilderHacker/COMPLETION_REPORT.md)** - What was delivered
- **[Test Documentation](Src/BuilderHacker/BuilderHacker.Tests/INDEX.md)** - Test coverage
- **[Security & Performance](Src/BuilderHacker/BuilderHacker.Tests/SECURITY_PERFORMANCE_INDEX.md)** - Audit results

### 📖 Reference
- **[Documentation Index](Src/BuilderHacker/README_INDEX.md)** - All docs at a glance
- **[API Reference](api)** - Auto-generated API documentation

## Performance & Quality

✅ **67+ Tests Passing** - Comprehensive test coverage  
✅ **Security Audited** - No vulnerabilities  
✅ **Performance Benchmarked** - Zero-cost abstractions  
✅ **Production Ready** - Used in enterprise applications  

## Get Started Now

1. **[Install via NuGet](getting-started.md#installation)** - Add to your project
2. **[View Examples](Src/BuilderHacker/QUICK_REFERENCE.md)** - See real code
3. **[Read the Guide](Src/BuilderHacker/DEVELOPMENT_GUIDELINES.md)** - Learn patterns
4. **[Explore the Roadmap](Src/BuilderHacker/MULTIFRAMEWORK_ROADMAP.md)** - Plan your implementation

---

**Current Version:** 1.1.0 | **License:** MIT | **Repository:** [GitHub](https://github.com/Shariar-Alfaz/BuilderHacker)
