# BuilderHacker

[![.NET](https://img.shields.io/badge/.NET-Standard%202.0%20%7C%20.NET%2010-blue)](https://dotnet.microsoft.com/)
[![Build](https://img.shields.io/badge/build-passing-brightgreen)](./)
[![License](https://img.shields.io/badge/license-unlicense-lightgrey)](./LICENSE)

BuilderHacker is a .NET builder generator and runtime builder library.

## Overview

Use BuilderHacker to generate fluent builders from an attribute, or use the runtime `EntityBuilder<T>` for reflection-based scenarios.

## Features

- Generates fluent builders from `[GenerateBuilderHacker]`
- Supports private fields and private setters
- Traverses inherited instance members
- Skips static classes and recursive getters with diagnostics
- Includes a runtime `EntityBuilder<T>` for reflection-based scenarios

## Projects

- `BuilderHacker.Abstraction` - shared attribute and interfaces
- `BuilderHacker.Core` - source generator and runtime builder
- `BuilderHacker.Console` - sample application

## Usage

### Attribute-based builder

```csharp
using BuilderHacker.Abstraction.Attributes;

[GenerateBuilderHacker]
public class TestClass
{
    public string Name { get; set; }
    public int Age { get; set; }
}

var obj = TestClassBuilder.Create()
    .SetName("John")
    .SetAge(30)
    .Build();
```

### Runtime builder

```csharp
using BuilderHacker.Core.EntityBuilder;

var obj = EntityBuilder<TestClass>.Create()
    .Set(x => x.Name, "John")
    .Set(x => x.Age, 30)
    .Build();
```

## Requirements

- .NET Standard 2.0 for shared libraries
- .NET 10 for the sample app and build environment

## Build

```powershell
dotnet build
```

## Notes

- Recursive getters are reported by the generator and skipped.
- Static classes are skipped by the generator with a warning diagnostic.
