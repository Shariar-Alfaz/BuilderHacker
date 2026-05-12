# BuilderHacker - Multi-Framework Support Roadmap

## Current Status
- **net10.0**: ✅ Supported (Console)
- **netstandard2.0**: ✅ Fully supported (Core, Abstraction)
- **net6.0+**: ✅ Supported (Generator via Roslyn)
- **.NET Framework 4.5+**: 🔄 In Progress
- **.NET Core 2.0-5.0**: 🔄 In Progress

## Architecture

### Projects

#### 1. **BuilderHacker.Abstraction** (netstandard2.0)
- Contains: `IBuilder<T>`, `GenerateBuilderHackerAttribute`
- No external dependencies
- **Why netstandard2.0**: Provides broad compatibility for both modern and legacy runtimes
- **Future**: Will target `net452;net461;netstandard2.0;netstandard2.1;net5.0;net6.0;net7.0;net8.0;net9.0;net10.0`

#### 2. **BuilderHacker.Core** (netstandard2.0)
- Contains: `EntityBuilder<T>` (reflection-based builder for any framework)
- No Roslyn dependencies - pure runtime code
- **Why netstandard2.0**: Runtime package can be consumed by a wide range of target frameworks
- **Future**: Will target `net452;net461;netstandard2.0;netstandard2.1;net5.0;net6.0;net7.0;net8.0;net9.0;net10.0`

#### 3. **BuilderHacker.Generator** (net6.0)
- Contains: `BuilderGenerator` (Roslyn-based incremental source generator)
- Requires: .NET 6.0+ (due to `IIncrementalGenerator`)
- **.NET Framework & Core 2-5**: Use `EntityBuilder<T>` from Core project instead
- **Future**: Can optionally provide legacy generator for .NET 4.5+ projects

#### 4. **BuilderHacker.Console** (net10.0)
- Example consumer application
- **Future**: Will have multi-targeted version

## Framework Compatibility Considerations

### ✅ Already Implemented
1. **No switch expressions** - Replaced with traditional if/else
2. **No pattern matching** - Using `as` operator and type checks
3. **String.Format() instead of interpolation** - Where needed for Framework support
4. **Proper null handling** - No null-coalescing operators that fail on older versions
5. **No .NET 7+ LINQ methods** - DistinctBy replaced with HashSet approach

### 🔄 To Be Implemented for Multi-Framework

#### Phase 1: NetStandard 2.0/2.1 Support
```xml
<TargetFrameworks>netstandard2.0;netstandard2.1;net6.0;net10.0</TargetFrameworks>
```
**Changes needed:**
- Remove `#nullable enable` from older target groups
- Use conditional compilation for nullable reference types
- Ensure no .NET 6+ APIs are used

#### Phase 2: .NET 5.0 Support
```xml
<TargetFrameworks>net5.0;netstandard2.1;net6.0;net10.0</TargetFrameworks>
```
**Changes needed:**
- Same as Phase 1
- net5.0 has better generics support than netstandard2.1

#### Phase 3: .NET Core 3.1 Support
```xml
<TargetFrameworks>netcoreapp3.1;netstandard2.1;net6.0;net10.0</TargetFrameworks>
```
**Changes needed:**
- Same compatibility as Phase 1-2

#### Phase 4: .NET Core 2.0-3.0 Support
```xml
<TargetFrameworks>netcoreapp2.0;netcoreapp2.1;netcoreapp3.0;netstandard2.0;net6.0;net10.0</TargetFrameworks>
```
**Changes needed:**
- Conditional compilation for features
- Avoid .NET Core 3.1+ features

#### Phase 5: .NET Framework 4.5+ Support
```xml
<TargetFrameworks>net452;net461;netstandard2.0;net6.0;net10.0</TargetFrameworks>
```
**Changes needed:**
- Major refactoring required - no expressions, limited generics
- May need separate legacy projects
- Recommend using `EntityBuilder<T>` instead of source generator

## Code Patterns for Multi-Framework

### ❌ Don't Use
```csharp
// C# 8.0+ - Switch expressions
return type switch
{
    PropertyInfo prop => prop,
    _ => null
};

// C# 7.0 - Pattern matching in out variable
if (GetMember() is PropertyInfo { CanRead: true } prop)

// C# 6.0 - Expression bodied members (use sparingly)
public static Builder Create() => new();

// .NET 7+ - DistinctBy
.DistinctBy(p => p.Name)

// String interpolation for .NET Framework
$"Property: {prop.Name}"
```

### ✅ Do Use
```csharp
// Traditional if/else - Works everywhere
PropertyInfo prop = obj as PropertyInfo;
if (prop != null)
{
    // use prop
}

// Type checks with 'as' operator
var field = member as FieldInfo;
if (field != null)
{
    // use field
}

// String.Format - Works on all frameworks
string.Format("Property: {0}", prop.Name)

// HashSet for deduplication - Works everywhere
var seen = new HashSet<string>();
if (seen.Add(name))
{
    // First occurrence
}

// Explicit null checks
if (expression == null)
    throw new ArgumentNullException(nameof(expression));
```

## Usage by Framework

### .NET 10.0 / .NET 9.0 / .NET 8.0 / .NET 7.0 / .NET 6.0
```csharp
// Use the source generator (recommended)
[GenerateBuilderHacker]
public partial class MyEntity { }

// Default generated standalone builder (Create() by default):
var builder = MyEntityBuilder.Create();
// Partial-mode alternative (if you used [GenerateBuilderHacker(true)]):
// var builder = MyEntity.Builder();
// OR use EntityBuilder directly
var builder = EntityBuilder<MyEntity>.Create();
```

### .NET 5.0 / .NET Core 3.1 / .NET Core 2.0-3.0
```csharp
// Use EntityBuilder<T> (reflection-based)
var builder = EntityBuilder<MyEntity>.Create()
    .Set(e => e.Name, "John")
    .Set(e => e.Age, 30)
    .Build();
```

### .NET Framework 4.5+
```csharp
// Use EntityBuilder<T> (reflection-based)
var builder = EntityBuilder<MyEntity>.Create()
    .StrictMode(true)
    .Set("Name", "John")
    .Set("Age", 30)
    .Build();
```

## Next Steps

1. **Add project file for multi-targeting tests**
   ```csharp
   dotnet new classlib -n BuilderHacker.Tests -f net6.0
   ```

2. **Use conditional compilation for Abstraction**
   ```csharp
   #if NET6_0_OR_GREATER
       // Modern code
   #elif NETSTANDARD2_1
       // .NET Standard 2.1 code
   #else
       // Legacy code
   #endif
   ```

3. **Test on all target frameworks**
   ```powershell
   dotnet test --framework net6.0
   dotnet test --framework net5.0
   ```

4. **Document framework-specific limitations**

## References
- [.NET Standard Compatibility](https://learn.microsoft.com/en-us/dotnet/standard/net-standard)
- [Multi-targeting in .NET](https://learn.microsoft.com/en-us/dotnet/standard/multitargeting)
- [C# Language Versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/configure-language-version)
