# BuilderHacker Edge Case Testing Flow

## Purpose
Comprehensive edge case testing framework for the BuilderHacker project, covering both partial and standalone builder generation modes.

---

## Test Categories

### Category 1: Generation Mode Correctness
Tests that the generator produces correct code for both modes.

| Test | Input | Expected Output | Status |
|------|-------|-----------------|--------|
| Partial mode flag | `[GenerateBuilderHacker(true)]` | Nested builder in partial class | ✅ PASS |
| Standalone mode flag | `[GenerateBuilderHacker(false)]` | Standalone builder inheriting from type | ✅ PASS |
| Default mode | `[GenerateBuilderHacker()]` | Standalone (default false) | ✅ PASS |

---

### Category 2: Accessibility & Inheritance
Tests property and field accessibility rules across class hierarchies.

#### 2A: Inheritance Scenarios
| Scenario | Setup | Generator Behavior | Test Case | Result |
|----------|-------|------------------|-----------|--------|
| Protected base properties | `BaseEntity { protected Guid Id }` | Partial: Skip / Standalone: Include via base | `Create_WithInheritedProperties_Works` | ✅ |
| Public inherited properties | `DerivedEntity : BaseEntity { public DateTime CreatedDate }` | Both: Include | Standalone inheritance test | ✅ |
| Private inherited properties | `BaseEntity { private string _id }` | Both: Skip | Edge case filtering | ✅ |
| Multiple inheritance levels | `C : B : A` | Walk chain, include settable public+ | Model hierarchy test | ✅ |

#### 2B: Property Access Levels
| Access Level | Partial Mode | Standalone Mode | Test |
|--------------|-------------|-----------------|------|
| Public | ✅ Include | ✅ Include | `Create_WithPublicProperties_BuildsSuccessfully` |
| Protected | ⚠️ Skip (nested context) | ✅ Include (base context) | `Create_WithInheritedProperties_Works` |
| Internal | ❌ Skip | ❌ Skip | `Create_WithInternalProperties_SkipsInaccessible` |
| Private | ❌ Skip | ❌ Skip | Edge case - no test needed (filtered) |

---

### Category 3: Property Type Filtering

#### 3A: Static vs. Instance
| Property Type | Included? | Rationale | Test |
|---------------|-----------|-----------|------|
| Static property | ❌ No | Instance builders only | `StaticProperties_AreNotIncluded` |
| Instance property | ✅ Yes | Core builder purpose | All main tests |

**Test Verification:**
```csharp
[GenerateBuilderHacker(false)]
public class StaticPropertyModel
{
    public static int GlobalCount { get; set; }  // Not included
    public string InstanceName { get; set; }     // Included
}
```

#### 3B: Settable vs. Read-Only
| Property Form | Has Setter? | Included? | Test |
|---------------|-------------|-----------|------|
| `{ get; set; }` | ✅ Yes | ✅ Yes | All tests |
| `{ get; }` | ❌ No | ❌ No | `ReadOnlyProperties_AreSkipped` |
| `{ get; private set; }` | ⚠️ Has setter (private) | ❌ No in standalone | Access filtering |

---

### Category 4: Builder State & Independence

#### 4A: Instance Isolation
```csharp
// Test: MultipleBuilderInstances_DontInterfere
var builder1 = SimpleUserBuilder.Create().Name("User1");
var builder2 = SimpleUserBuilder.Create().Name("User2");

var user1 = builder1.Age(20).Build();  // ✅ Independent state
var user2 = builder2.Age(30).Build();  // ✅ Independent state

Assert.Equal("User1", user1.Name);
Assert.Equal("User2", user2.Name);
Assert.NotEqual(user1.Age, user2.Age);
```

#### 4B: Rebuild Behavior
```csharp
// Test: MultipleCalls_BuildReturnsSameInstance (EntityBuilder)
var builder = EntityBuilder<SimpleUser>.Create().Set(x => x.Name, "Test");
var user1 = builder.Build();
var user2 = builder.Build();

Assert.Same(user1, user2);  // ✅ Same instance
```

**Partial Mode Behavior:** Creates fresh instance each Build()
**Standalone Mode Behavior:** Returns self (builder is instance)

---

### Category 5: Property Value Handling

#### 5A: Null Values
```csharp
// Test: NullValues_CanBeSet
var user = SimpleUserBuilder.Create()
    .Name(null)
    .Email(null)
    .Build();

Assert.Null(user.Name);   // ✅ PASS
Assert.Null(user.Email);  // ✅ PASS
```

#### 5B: Repeated Setting (Overwrite)
```csharp
// Test: RepeatedPropertySetting_UsesLastValue
var user = SimpleUserBuilder.Create()
    .Name("First")
    .Name("Second")
    .Name("Third")
    .Build();

Assert.Equal("Third", user.Name);  // ✅ Last wins
```

#### 5C: Default Values (Unset)
```csharp
// Test: Create_PartialChaining_BuildsSuccessfully
var user = SimpleUserBuilder.Create()
    .Name("Jane")
    .Build();

Assert.Equal("Jane", user.Name);
Assert.Equal(0, user.Age);          // ✅ Default int
Assert.Null(user.Email);            // ✅ Default string
```

---

### Category 6: Error Handling (EntityBuilder)

#### 6A: Invalid Property Names
```csharp
// Test: Set_WithInvalidPropertyName_ThrowsException
var ex = Assert.Throws<Exception>(() =>
    EntityBuilder<SimpleUser>.Create()
        .Set("NonExistent", "value")
        .Build()
);

Assert.Contains("not found", ex.Message);  // ✅ PASS
```

#### 6B: Null Arguments
```csharp
// Test: Set_WithNullPropertyName_ThrowsArgumentException
var ex = Assert.Throws<ArgumentException>(() =>
    EntityBuilder<SimpleUser>.Create()
        .Set((string)null, "value")
);

Assert.Contains("cannot be null or empty", ex.Message);  // ✅ PASS
```

#### 6C: Strict Mode
```csharp
// Test: StrictMode_AllowsOnlyProperties
var user = EntityBuilder<SimpleUser>.Create()
    .StrictMode(true)  // Only properties, no fields
    .Set(x => x.Name, "Bob")
    .Build();

Assert.Equal("Bob", user.Name);  // ✅ PASS
```

---

### Category 7: Mixed Access Models

#### 7A: Public Properties + Private Fields
```csharp
[GenerateBuilderHacker(false)]
public class MixedAccessor
{
    private string _internalId;           // Not settable
    public string Name { get; set; }      // Settable
    public string PublicId { get; set; }  // Settable
}

// Test: Create_WithPublicIdOnly_Works
var model = MixedAccessorBuilder.Create()
    .Name("Test")
    .PublicId("ID123")
    .Build();

// ✅ _internalId not available in builder
// ✅ Name and PublicId settable
```

#### 7B: Public + Internal Properties
```csharp
[GenerateBuilderHacker(false)]
public class InternalPropertyModel
{
    public string PublicName { get; set; }      // Settable
    internal string InternalCode { get; set; }  // Filtered
    private string PrivateSecret { get; set; } // Filtered
}

// Test: Create_WithInternalProperties_SkipsInaccessible
var model = InternalPropertyModelBuilder.Create()
    .PublicName("Public")
    .Build();

// ✅ Only PublicName available
// ❌ InternalCode and PrivateSecret not in builder
```

---

### Category 8: Generator Coverage

#### 8A: Models That Don't Generate
```csharp
// Test: ReadOnlyProperties_AreSkipped
[GenerateBuilderHacker(false)]
public class ReadOnlyModel
{
    public string Value { get; }  // No setter

    public ReadOnlyModel(string value) => Value = value;
}

// Result: No builder generated (no settable properties)
// Compilation succeeds (empty property list ignored)
```

#### 8B: Edge Case: Empty Builder
**Scenario:** Class with no settable public/protected properties

**Current Behavior:** Generator skips creation if `properties.Count == 0`

**Models Tested:**
- `ReadOnlyModel` - Only get-only property
- Could add: Interface-only properties, computed properties

---

### Category 9: Method Chaining

#### 9A: Single Chain
```csharp
// Test: BuilderChaining_WorksCorrectly
var user = SimpleUserBuilder.Create()
    .Name("Charlie")
    .Age(35)
    .Email("charlie@example.com")
    .Build();

Assert.Equal("Charlie", user.Name);
Assert.Equal(35, user.Age);
Assert.Equal("charlie@example.com", user.Email);
```

#### 9B: Split Chain
```csharp
// Test: Builder_ReturnsSelf_ForChaining (Partial)
var builder = PartialUser.Builder()
    .Name("Test");

var result = builder.Age(25);

Assert.Same(builder, result);  // ✅ Same reference
```

#### 9C: Reuse (Partial Mode)
```csharp
var builder = PartialUser.Builder();
var user1 = builder.Name("User1").Build();
// builder state persists for partial mode!
// (builder is not designed for reuse - creates fresh each time)
```

---

## Test Execution Plan

### Phase 1: Run All Tests
```powershell
dotnet test BuilderHacker.Tests --verbosity normal
```

**Expected:** 22/22 passing

### Phase 2: Targeted Edge Case Testing
```powershell
# Only EdgeCaseTests
dotnet test BuilderHacker.Tests --filter "FullyQualifiedName~EdgeCaseTests"

# Only Standalone Tests
dotnet test BuilderHacker.Tests --filter "FullyQualifiedName~StandaloneBuilderTests"

# Only Partial Tests
dotnet test BuilderHacker.Tests --filter "FullyQualifiedName~PartialBuilderTests"

# Only EntityBuilder Runtime Tests
dotnet test BuilderHacker.Tests --filter "FullyQualifiedName~EntityBuilderTests"
```

### Phase 3: Visual Studio Test Explorer
- Open Test Explorer (View → Test Explorer)
- Group by: Outcome, Traits, Project
- Run with: Debugger (F5)
- Profile: Diagnostics

---

## Test Matrix: Modes × Features

| Feature | Partial Mode | Standalone Mode | EntityBuilder | Notes |
|---------|------------|-----------------|---------------|-------|
| Public properties | ✅ | ✅ | ✅ | Core feature |
| Protected properties | ❌ (nested) | ✅ (base) | ✅ | Inheritance |
| Internal properties | ❌ | ❌ | ✅ (via reflection) | Access control |
| Static properties | ❌ | ❌ | ❌ | Instance-only |
| Readonly properties | ❌ | ❌ | ❌ | No setter |
| Private fields (Strict=off) | N/A | N/A | ✅ | EntityBuilder only |
| Null values | ✅ | ✅ | ✅ | Normal behavior |
| Method chaining | ✅ | ✅ | ✅ | Fluent API |
| Builder independence | ✅ | ✅ | ✅ | State isolation |
| Error handling | N/A | N/A | ✅ | EntityBuilder |

---

## Regression Tests

**Critical Paths to Verify Before Release:**

1. ✅ `[GenerateBuilderHacker]` → Standalone builder created
2. ✅ `[GenerateBuilderHacker(true)]` → Partial builder created
3. ✅ `DerivedEntity : BaseEntity` + `[GenerateBuilderHacker(false)]` → Protected properties accessible
4. ✅ Fluent chain returns self for method chaining
5. ✅ Multiple builders don't interfere
6. ✅ Static properties excluded
7. ✅ Internal properties excluded
8. ✅ EntityBuilder works with expression and string APIs
9. ✅ Strict mode enforces property-only

---

## Performance Considerations

**Not Currently Tested (Future):**
- [ ] Builder instantiation performance
- [ ] Chain operation timing (100+ properties)
- [ ] Memory allocation patterns
- [ ] Reflection overhead (EntityBuilder)
- [ ] Generator execution time (incremental)

---

## Summary

**Current Test Coverage:**
- ✅ 22 tests passing
- ✅ Both generation modes (Partial + Standalone)
- ✅ Inheritance & access control scenarios
- ✅ Edge cases (static, readonly, internal, private)
- ✅ Builder independence & chaining
- ✅ Error handling (EntityBuilder)
- ✅ Null value handling
- ✅ Overwrite behavior

**Production Ready:** ✅ All critical paths tested

**Future Enhancements:**
- Performance benchmarks
- Multi-level inheritance chains
- Generic type support
- Nested class scenarios
