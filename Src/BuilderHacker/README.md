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
- Supports source-generation and runtime reflection-based builder workflows
- Includes a runtime `EntityBuilder<T>` for reflection-based scenarios

## Projects

- `BuilderHacker.Abstraction` - shared attribute and interfaces
- `BuilderHacker.Core` - runtime `EntityBuilder<T>` library
- `BuilderHacker.Generator` - incremental source generator for attribute-based builders
- `BuilderHacker.Console` - sample application

## Usage

### Attribute-based builder

```csharp
using BuilderHacker.Abstraction.Attributes;

[GenerateBuilderHacker]
public partial class TestClass
{
    public string Name { get; set; }
    public int Age { get; set; }
}

var obj = TestClass.Builder()
    .Name("John")
    .Age(30)
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

- Types marked with `[GenerateBuilderHacker]` must be declared as `partial`.
- The generated API follows `YourType.Builder().PropertyName(value)...Build()`.
