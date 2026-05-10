---

# EntityBuilder<T>

## Overview

`EntityBuilder<T>` is a lightweight, reflection-based generic builder designed for dynamically constructing entities at runtime.

It provides:

- Fluent API support
- Property and field mapping
- Optional strict mode
- Expression-based property configuration
- Runtime reflection support
- Cross-platform compatibility

Unlike source-generated builders, `EntityBuilder<T>` works without code generation and can be used immediately in any supported .NET project.

---

# Table of Contents

- [Overview](#overview)
- [Supported Platforms](#supported-platforms)
- [Creating a Builder](#creating-a-builder)
- [Strict Mode](#strict-mode)
- [Set By Property Name](#set-by-property-name)
- [Set By Expression](#set-by-expression)
- [Build Entity](#build-entity)
- [Complete Example](#complete-example)
- [.NET Framework Example](#net-framework-example)
- [.NET Core / .NET 6+ Example](#net-core--net-6-example)
- [.NET MAUI Example](#net-maui-example)
- [Blazor Example](#blazor-example)
- [Thread Safety](#thread-safety)
- [Best Practices](#best-practices)

---

# Supported Platforms

| Platform | Supported |
|---|---|
| .NET Framework 4.5+ | ✅ |
| .NET Standard 2.0+ | ✅ |
| .NET Core 2.0+ | ✅ |
| .NET 5+ | ✅ |
| .NET 6+ | ✅ |
| ASP.NET MVC | ✅ |
| ASP.NET Core | ✅ |
| .NET MAUI | ✅ |
| Blazor | ✅ |
| WPF | ✅ |
| WinForms | ✅ |
| Console Apps | ✅ |

---

# Creating a Builder

## Using Static Create()

```csharp
var builder = EntityBuilder<User>.Create();
```

---

# Strict Mode

Strict mode allows only properties to be set.

Fields are ignored and will throw exceptions if accessed.

## Enable Strict Mode

```csharp
var builder = EntityBuilder<User>
    .Create()
    .StrictMode();
```

---

## Disable Strict Mode

```csharp
var builder = EntityBuilder<User>
    .Create()
    .StrictMode(false);
```

---

# Set By Property Name

Dynamically set a property or field using its name.

## Example

```csharp
var user = EntityBuilder<User>
    .Create()
    .Set("Name", "John")
    .Set("Age", 25)
    .Build();
```

---

## Case Insensitive Support

```csharp
var user = EntityBuilder<User>
    .Create()
    .Set("name", "Alice")
    .Build();
```

---

# Set By Expression

Expression-based configuration provides compile-time safety.

## Example

```csharp
var user = EntityBuilder<User>
    .Create()
    .Set(x => x.Name, "Bob")
    .Set(x => x.Age, 30)
    .Build();
```

---

# Build Entity

Returns the constructed entity instance.

## Example

```csharp
var user = builder.Build();
```

---

# Complete Example

## Entity

```csharp
public class User
{
    public string Name { get; set; }

    public int Age { get; set; }

    public string Email;
}
```

---

## Usage

```csharp
var user = EntityBuilder<User>
    .Create()
    .Set(x => x.Name, "Shariar")
    .Set(x => x.Age, 25)
    .Set("Email", "admin@example.com")
    .Build();
```

---

# .NET Framework Example

## Install Package

```bash
Install-Package BuilderHacker.Core
```

---

## Usage

```csharp
using BuilderHacker.Core.EntityBuilder;

var user = EntityBuilder<User>
    .Create()
    .Set("Name", "Framework User")
    .Set("Age", 20)
    .Build();
```

---

# .NET Core / .NET 6+ Example

## Usage

```csharp
using BuilderHacker.Core.EntityBuilder;

var user = EntityBuilder<User>
    .Create()
    .Set(x => x.Name, "Core User")
    .Set(x => x.Age, 35)
    .Build();
```

---

# ASP.NET Core Web API Example

## Controller Example

```csharp
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var user = EntityBuilder<User>
            .Create()
            .Set(x => x.Name, "API User")
            .Set(x => x.Age, 22)
            .Build();

        return Ok(user);
    }
}
```

---

# .NET MAUI Example

## ViewModel Example

```csharp
public class MainViewModel
{
    public User CreateUser()
    {
        return EntityBuilder<User>
            .Create()
            .Set(x => x.Name, "MAUI User")
            .Set(x => x.Age, 28)
            .Build();
    }
}
```

---

# Blazor Example

## Razor Component

```razor
<button @onclick="CreateUser">
    Create User
</button>

@code {

    private User user;

    private void CreateUser()
    {
        user = EntityBuilder<User>
            .Create()
            .Set(x => x.Name, "Blazor User")
            .Set(x => x.Age, 18)
            .Build();
    }
}
```

---

# Console Application Example

```csharp
using BuilderHacker.Core.EntityBuilder;

class Program
{
    static void Main()
    {
        var user = EntityBuilder<User>
            .Create()
            .Set("Name", "Console User")
            .Set("Age", 40)
            .Build();

        Console.WriteLine(user.Name);
    }
}
```

---

# Reflection Support

`EntityBuilder<T>` uses reflection internally to dynamically locate and assign:

- Properties
- Fields
- Private setters
- Runtime members

Reflection member lookups are cached for better performance.

---

# Source Generator Recommendation

For modern .NET applications (`.NET 6+`), using source-generated builders with:

```csharp
[GenerateBuilderHacker]
```

is recommended for:

- Better performance
- Compile-time generation
- Strong typing
- Reduced reflection usage

---

# Error Handling

## Invalid Property

```csharp
EntityBuilder<User>
    .Create()
    .Set("UnknownProperty", "Value");
```

Throws:

```text
InvalidOperationException
```

---

## Strict Mode Field Access

```csharp
EntityBuilder<User>
    .Create()
    .StrictMode()
    .Set("Email", "test@example.com");
```

Throws:

```text
InvalidOperationException
```

if `Email` is a field.

---

# Thread Safety

> WARNING:
>
> `EntityBuilder<T>` is NOT thread-safe.

Each thread should use its own builder instance.

---

## Recommended Pattern

```csharp
var builder = EntityBuilder<User>.Create();
```

Avoid sharing the same builder across multiple threads.

---

# Best Practices

- Prefer expression-based setters
- Use strict mode in production environments
- Use source generators for high-performance applications
- Avoid sharing builders across threads
- Keep entities simple and focused

---

# Comparison

| Feature | EntityBuilder<T> | Source Generated Builder |
|---|---|---|
| Reflection Based | ✅ | ❌ |
| Runtime Dynamic | ✅ | ❌ |
| Compile-Time Safe | Partial | ✅ |
| Performance | Medium | High |
| Requires Generator | ❌ | ✅ |
| Works on .NET Framework | ✅ | Limited |

---

# Summary

`EntityBuilder<T>` provides a flexible and fluent way to dynamically construct entities across all supported .NET platforms without requiring source generation.

It is ideal for:

- Rapid development
- Dynamic object creation
- Legacy framework support
- Runtime mapping scenarios
- Reflection-driven systems


# DefaultBuilderHackerFactory

## Overview

`DefaultBuilderHackerFactory` is the default implementation of `IBuilderHackerFactory` in BuilderHacker.

It uses `IServiceProvider` to dynamically resolve registered builders from the dependency injection container.

Supports:

- .NET Framework
- .NET Core
- .NET 6+
- ASP.NET MVC
- ASP.NET Core
- .NET MAUI
- Blazor
- Console Apps
- Worker Services
- WPF
- WinForms

---

# Table of Contents

- [Installation](#installation)
- [Factory Overview](#factory-overview)
- [Methods](#methods)
- [Basic Builder Example](#basic-builder-example)
- [.NET Framework Example](#net-framework-example)
- [.NET Core / .NET 6+ Example](#net-core--net-6-example)
- [ASP.NET Core Web API Example](#aspnet-core-web-api-example)
- [.NET MAUI Example](#net-maui-example)
- [Blazor Example](#blazor-example)
- [Console Application Example](#console-application-example)
- [Best Practices](#best-practices)
- [Supported Platforms](#supported-platforms)

---

# Installation

## NuGet

```bash
Install-Package SAProduction.BuilderHacker.Core
```

Or using .NET CLI:

```bash
dotnet add package SAProduction.BuilderHacker.Core
```

---

# Factory Overview

```csharp
using BuilderHacker.Core.Builder;

public class DefaultBuilderHackerFactory : IBuilderHackerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DefaultBuilderHackerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider 
            ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public IBuilder<T> CreateBuilder<T>()
    {
        var builder = _serviceProvider.GetService(typeof(IBuilder<T>)) as IBuilder<T>;

        if (builder == null)
        {
            throw new InvalidOperationException(
                $"No builder registered for type {typeof(T).FullName}.");
        }

        return builder;
    }

    public TBuilder CreateBuilder<T, TBuilder>()
        where TBuilder : IBuilder<T>
    {
        var builder = _serviceProvider.GetService(typeof(TBuilder));

        if (builder == null)
        {
            builder = _serviceProvider.GetService(typeof(IBuilder<T>));
        }

        if (builder == null)
        {
            throw new InvalidOperationException(
                $"No builder registered for type {typeof(TBuilder).FullName}.");
        }

        if (!(builder is TBuilder))
        {
            throw new InvalidCastException(
                $"Registered builder is not of type {typeof(TBuilder).FullName}.");
        }

        return (TBuilder)builder;
    }
}
```

---

# Methods

## CreateBuilder<T>()

Resolves a builder registered as `IBuilder<T>`.

### Example

```csharp
var builder = factory.CreateBuilder<User>();
```

---

## CreateBuilder<T, TBuilder>()

Resolves a strongly typed builder implementation.

### Example

```csharp
var builder = factory.CreateBuilder<User, UserBuilder>();
```

---

# Basic Builder Example

## Entity

```csharp
public class User
{
    public string Name { get; set; }

    public int Age { get; set; }
}
```

---

## Builder Interface

```csharp
public interface IBuilder<T>
{
    T Build();
}
```

---

## Builder Implementation

```csharp
public class UserBuilder : IBuilder<User>
{
    private readonly User _user = new User();

    public UserBuilder WithName(string name)
    {
        _user.Name = name;
        return this;
    }

    public UserBuilder WithAge(int age)
    {
        _user.Age = age;
        return this;
    }

    public User Build()
    {
        return _user;
    }
}
```

---

# .NET Framework Example

## Install DI Package

```bash
Install-Package Microsoft.Extensions.DependencyInjection
```

---

## Configure Services

```csharp
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddTransient<IBuilder<User>, UserBuilder>();

services.AddSingleton<IBuilderHackerFactory, DefaultBuilderHackerFactory>();

var provider = services.BuildServiceProvider();
```

---

## Usage

```csharp
var factory = provider.GetRequiredService<IBuilderHackerFactory>();

var builder = factory.CreateBuilder<User, UserBuilder>();

var user = builder
    .WithName("John")
    .WithAge(25)
    .Build();
```

---

# .NET Core / .NET 6+ Example

## Program.cs

```csharp
builder.Services.AddTransient<IBuilder<User>, UserBuilder>();

builder.Services.AddSingleton<IBuilderHackerFactory, DefaultBuilderHackerFactory>();
```

---

## Service Usage

```csharp
public class UserService
{
    private readonly IBuilderHackerFactory _factory;

    public UserService(IBuilderHackerFactory factory)
    {
        _factory = factory;
    }

    public User Create()
    {
        var builder = _factory.CreateBuilder<User, UserBuilder>();

        return builder
            .WithName("Alice")
            .WithAge(30)
            .Build();
    }
}
```

---

# ASP.NET Core Web API Example

## Registration

```csharp
builder.Services.AddTransient<IBuilder<User>, UserBuilder>();

builder.Services.AddSingleton<IBuilderHackerFactory, DefaultBuilderHackerFactory>();
```

---

## Controller Example

```csharp
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IBuilderHackerFactory _factory;

    public UsersController(IBuilderHackerFactory factory)
    {
        _factory = factory;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var builder = _factory.CreateBuilder<User, UserBuilder>();

        var user = builder
            .WithName("API User")
            .WithAge(22)
            .Build();

        return Ok(user);
    }
}
```

---

# .NET MAUI Example

## MauiProgram.cs

```csharp
builder.Services.AddTransient<IBuilder<User>, UserBuilder>();

builder.Services.AddSingleton<IBuilderHackerFactory, DefaultBuilderHackerFactory>();
```

---

## ViewModel Example

```csharp
public class MainViewModel
{
    private readonly IBuilderHackerFactory _factory;

    public MainViewModel(IBuilderHackerFactory factory)
    {
        _factory = factory;
    }

    public User CreateUser()
    {
        var builder = _factory.CreateBuilder<User, UserBuilder>();

        return builder
            .WithName("MAUI User")
            .WithAge(28)
            .Build();
    }
}
```

---

# Blazor Example

## Registration

```csharp
builder.Services.AddTransient<IBuilder<User>, UserBuilder>();

builder.Services.AddSingleton<IBuilderHackerFactory, DefaultBuilderHackerFactory>();
```

---

## Razor Component

```razor
@inject IBuilderHackerFactory Factory

<button @onclick="CreateUser">Create User</button>

@code {
    private User user;

    private void CreateUser()
    {
        var builder = Factory.CreateBuilder<User, UserBuilder>();

        user = builder
            .WithName("Blazor User")
            .WithAge(18)
            .Build();
    }
}
```

---

# Console Application Example

```csharp
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddTransient<IBuilder<User>, UserBuilder>();

services.AddSingleton<IBuilderHackerFactory, DefaultBuilderHackerFactory>();

var provider = services.BuildServiceProvider();

var factory = provider.GetRequiredService<IBuilderHackerFactory>();

var builder = factory.CreateBuilder<User, UserBuilder>();

var user = builder
    .WithName("Console User")
    .WithAge(40)
    .Build();

Console.WriteLine(user.Name);
```

---

# Best Practices

- Register builders as `Transient`
- Keep builders stateless when possible
- Use constructor injection
- Use strongly typed builders for complex objects
- Prefer generic overloads for better type safety

---

# Supported Platforms

| Platform | Supported |
|---|---|
| .NET Framework | ✅ |
| .NET Core | ✅ |
| .NET 6+ | ✅ |
| ASP.NET MVC | ✅ |
| ASP.NET Core | ✅ |
| .NET MAUI | ✅ |
| Blazor | ✅ |
| Console Apps | ✅ |
| Worker Services | ✅ |
| WPF | ✅ |
| WinForms | ✅ |

---

# Summary

`DefaultBuilderHackerFactory` provides a flexible and dependency-injection-friendly way to resolve builders dynamically across all modern .NET platforms.