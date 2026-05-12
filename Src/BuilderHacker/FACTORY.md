# Builder Factory (IBuilderHackerFactory)

BuilderHacker provides a small factory abstraction to resolve builders from an IoC container at runtime.
This is useful when builders are generated or registered and consumers need a DI-friendly way to obtain them.

## Key Interfaces

- `IBuilderHackerFactory` — factory abstraction
- `DefaultBuilderHackerFactory` — default implementation that resolves builders from `IServiceProvider`

## API

```csharp
public interface IBuilderHackerFactory
{
    IBuilder<T> CreateBuilder<T>();
    TBuilder CreateBuilder<T, TBuilder>() where TBuilder : IBuilder<T>;
}
```

`CreateBuilder<T>()` resolves an `IBuilder<T>` instance.
`CreateBuilder<T, TBuilder>()` resolves a specific builder type (or falls back to resolving `IBuilder<T>`).

## Typical DI Registration

Register the default factory and your builders in `Startup` / `Program`:

```csharp
services.AddSingleton<IBuilderHackerFactory, DefaultBuilderHackerFactory>();

// Register generated or handwritten builders
services.AddTransient<IBuilder<SimpleUser>, SimpleUserBuilder>();
// or register the concrete builder type
services.AddTransient<SimpleUserBuilder>();
```

`DefaultBuilderHackerFactory` tries to resolve the requested `TBuilder` first; if not found, it will attempt to resolve `IBuilder<T>` and cast.

## Usage Examples

Resolve by model type (generic):

```csharp
var factory = serviceProvider.GetRequiredService<IBuilderHackerFactory>();
var builder = factory.CreateBuilder<SimpleUser>();
var user = builder.Name("Sam").Age(25).Build();
```

Resolve a concrete builder type (preferred when you know the generated builder type):

```csharp
var typed = factory.CreateBuilder<SimpleUser, SimpleUserBuilder>();
var user = typed.Name("Sam").Age(25).Build();
```

## Notes & Best Practices

- Register builders with the DI container so `DefaultBuilderHackerFactory` can resolve them.
- Register builders as `Transient` (or `Scoped`) because generated builders are stateful.
- When using generated builders, register the generated concrete type (e.g., `SimpleUserBuilder`) or the `IBuilder<T>` interface.
- `CreateBuilder<T, TBuilder>()` will throw an informative exception if no suitable registration is found or if the resolved instance cannot be cast to `TBuilder`.

## Tests

See `BuilderHacker.Tests.Core.FactoryTests` for example registration and resolution scenarios.
