# Multi-Framework Development Guidelines

## Quick Reference

### ✅ DO THIS - Cross-Framework Compatible

```csharp
// 1. Type Checks
PropertyInfo prop = member as PropertyInfo;
if (prop != null)
{
    // Use prop
}

// 2. String Operations
string message = string.Format("Property: {0}", propName);
string[] parts = name.Split(new[] { ',' }, StringSplitOptions.None);

// 3. Collections - HashSet for deduplication
var seen = new HashSet<string>();
foreach (var item in items)
{
    if (seen.Add(item.Name))
    {
        // First occurrence only
    }
}

// 4. Null Checks
if (expression == null)
    throw new ArgumentNullException(nameof(expression));

// 5. Conditionals
if (condition1)
{
    // Do something
}
else if (condition2)
{
    // Do something else
}
else
{
    // Default
}

// 6. LINQ - Stick to basics
var result = items
    .Where(x => x.IsValid)
    .OrderBy(x => x.Name)
    .ToList();

// 7. Exceptions
try
{
    // Something
}
catch (Exception ex)
{
    throw new Exception("Detailed message: " + ex.Message, ex);
}

// 8. Method signatures (no default parameters in some contexts)
public void SetValue(string name, object value)
{
    // Use overloads instead
}

public void SetValue(string name)
{
    SetValue(name, null);
}

// 9. XML Documentation
/// <summary>
/// Brief description.
/// </summary>
/// <remarks>
/// Detailed notes about cross-framework compatibility.
/// </remarks>
/// <param name="name">Parameter description.</param>
/// <returns>Return value description.</returns>
public void DoSomething(string name) { }

// 10. Reflection - The universal approach
var type = typeof(T);
var properties = type.GetProperties(
    BindingFlags.Public |
    BindingFlags.NonPublic |
    BindingFlags.Instance);

foreach (var prop in properties)
{
    if (prop.CanRead && prop.CanWrite)
    {
        // Use property
    }
}
```

### ❌ DON'T DO THIS - Framework-Specific

```csharp
// ❌ Pattern Matching (C# 7.0+)
if (member is PropertyInfo prop)
{
    // This won't work on .NET Framework
}

// ❌ Switch Expressions (C# 8.0+)
var result = obj switch
{
    PropertyInfo => "property",
    FieldInfo => "field",
    _ => "unknown"
};

// ❌ String Interpolation (when .NET Framework needed)
string message = $"Property: {propName}"; // Use string.Format instead

// ❌ DistinctBy (LINQ, .NET 7+)
items.DistinctBy(x => x.Name);

// ❌ Nullable Reference Types in declarations (C# 8+)
#nullable enable
public string? Name { get; set; }

// ❌ Target-Typed New (C# 9+)
IEnumerable<int> numbers = new() { 1, 2, 3 };

// ❌ Records (C# 9+)
public record Person(string Name, int Age);

// ❌ Init-only Properties (C# 9+)
public string Name { get; init; }

// ❌ Using Directives (C# 10+)
using Disposable d = GetDisposable();

// ❌ File-Scoped Types (C# 11+)
file class PrivateClass { }

// ❌ Union Types / Discriminated Unions (C# 12+)
public record Result = Success(object Value) | Failure(string Error);
```

## Conditional Compilation Guide

### Simple Framework Check
```csharp
#if NET6_0_OR_GREATER
    // Use modern APIs here
    var result = items.DistinctBy(x => x.Id).ToList();
#else
    // Fallback for older frameworks
    var seen = new HashSet<int>();
    var result = items.Where(x => seen.Add(x.Id)).ToList();
#endif
```

### Multi-Framework Compatibility
```csharp
#if NET10_0 || NET9_0 || NET8_0 || NET7_0 || NET6_0
    const int MinVersion = 6;
    // Modern .NET code
#elif NETCOREAPP3_1 || NETCOREAPP3_0
    const int MinVersion = 31;
    // .NET Core 3.x code
#elif NETCOREAPP2_1 || NETCOREAPP2_0
    const int MinVersion = 20;
    // .NET Core 2.x code
#elif NET461 || NET452
    const int MinVersion = 452;
    // .NET Framework code
#endif
```

### Nullable Reference Types
```csharp
#if NET5_0_OR_GREATER
    #nullable enable

    public class MyClass
    {
        public string? OptionalProperty { get; set; }
    }

    #nullable restore
#else
    public class MyClass
    {
        // No nullable reference type syntax
        public string OptionalProperty { get; set; }
    }
#endif
```

## Target Framework Symbols

Use these in conditional compilation:

**Modern .NET Versions:**
- `NET10_0` - .NET 10
- `NET9_0` - .NET 9
- `NET8_0` - .NET 8
- `NET7_0` - .NET 7
- `NET6_0` - .NET 6
- `NET5_0` - .NET 5

**.NET Core Versions:**
- `NETCOREAPP3_1` - .NET Core 3.1
- `NETCOREAPP3_0` - .NET Core 3.0
- `NETCOREAPP2_1` - .NET Core 2.1
- `NETCOREAPP2_0` - .NET Core 2.0

**.NET Framework:**
- `NET481` - .NET Framework 4.8.1
- `NET48` - .NET Framework 4.8
- `NET472` - .NET Framework 4.7.2
- `NET471` - .NET Framework 4.7.1
- `NET47` - .NET Framework 4.7
- `NET462` - .NET Framework 4.6.2
- `NET461` - .NET Framework 4.6.1
- `NET46` - .NET Framework 4.6
- `NET452` - .NET Framework 4.5.2

**.NET Standard:**
- `NETSTANDARD2_1` - .NET Standard 2.1
- `NETSTANDARD2_0` - .NET Standard 2.0
- `NETSTANDARD1_6` - .NET Standard 1.6

## Reflection Patterns (Works Everywhere!)

### Getting Properties
```csharp
// All properties
var allProps = type.GetProperties();

// Public properties only
var publicProps = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

// Including private
var allProps = type.GetProperties(
    BindingFlags.Public | BindingFlags.Private | BindingFlags.Instance);

// Case-insensitive search
var prop = type.GetProperty(name,
    BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
```

### Setting Property Values
```csharp
PropertyInfo prop = /* ... */;
object instance = /* ... */;

// Simple set
if (prop.CanWrite)
{
    prop.SetValue(instance, newValue);
}

// Set via private setter
if (!prop.CanWrite)
{
    var setter = prop.GetSetMethod(nonPublic: true);
    if (setter != null)
    {
        setter.Invoke(instance, new object[] { newValue });
    }
}
```

### Working with Expressions (LINQ Expressions)
```csharp
// Extract member from expression
public static MemberExpression GetMember(Expression expression)
{
    if (expression == null)
        return null;

    var member = expression as MemberExpression;
    if (member != null)
        return member;

    var unary = expression as UnaryExpression;
    if (unary != null)
    {
        return unary.Operand as MemberExpression;
    }

    return null;
}

// Use it
var member = GetMember(x => x.Property);
if (member != null && member.Member is PropertyInfo prop)
{
    // Use prop
}
```

## Best Practices

1. **Prefer reflection over language features** for .NET Framework compat
   - Reflection works on all versions
   - Language features vary by version

2. **Use string.Format() for older framework support**
   ```csharp
   // Good for compatibility
   string.Format("Value: {0}, Type: {1}", value, type)

   // Also works but simpler
   $"Value: {value}, Type: {type}"
   ```

3. **Test on multiple frameworks** (when multi-targeting)
   ```bash
   dotnet test --framework net10.0
   dotnet test --framework net6.0
   dotnet test --framework netstandard2.0
   ```

4. **Document framework requirements** in XML comments
   ```csharp
   /// <summary>
   /// Does something.
   /// </summary>
   /// <remarks>
   /// Compatible with .NET Framework 4.5+, .NET Standard 2.0+, and all modern .NET versions.
   /// </remarks>
   ```

5. **Use explicit types** instead of var (sometimes)
   ```csharp
   // Better for readability on older framework projects
   PropertyInfo prop = member as PropertyInfo;

   // vs implicit type (still works, but less clear in legacy contexts)
   var prop = member as PropertyInfo;
   ```

6. **Comment "why" not "what"**
   ```csharp
   // BAD: Comment says what code does (obvious)
   // Convert string to integer
   int value = int.Parse(input);

   // GOOD: Comment explains decision
   // Use int.Parse instead of Convert.ToInt32 for performance
   // and compatibility with .NET Framework 4.5
   int value = int.Parse(input);
   ```

## Common Issues & Solutions

### Issue: Generic Type Formatting
**Problem:** `prop.Type.ToString()` returns `System.Collections.Generic.List`1` (ugly)

**Solution:**
```csharp
// Use Roslyn's SymbolDisplayFormat
using Microsoft.CodeAnalysis;
string fullType = symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
// Output: System.Collections.Generic.List<System.String>
```

### Issue: Nullable Type Checking
**Problem:** Can't distinguish `string` from `string?` on .NET Framework

**Solution:**
```csharp
#if NET5_0_OR_GREATER
    bool isNullable = propertySymbol.NullableAnnotation == NullableAnnotation.Annotated;
#else
    // Assume nullable or use other detection method
    bool isNullable = true;
#endif
```

### Issue: LINQ Method Not Available
**Problem:** `.DistinctBy()` doesn't exist on .NET 6

**Solution:**
```csharp
// Instead of:
items.DistinctBy(x => x.Name)

// Use HashSet approach (works everywhere):
var seen = new HashSet<string>();
var result = items.Where(x => seen.Add(x.Name)).ToList();
```

## Version History

- **v1.1.0** - Multi-framework support added (source generator separated)
- **v1.0.0** - Initial release (net10.0 only)

---

**Remember:** When in doubt, use **reflection** and **traditional patterns**. They work everywhere! 🎯
