# Builder Factory (`IBuilderHackerFactory`)

BuilderHacker includes a lightweight factory abstraction that allows applications to resolve builders dynamically through Dependency Injection (DI).

This is especially useful when:

- Builders are generated automatically
- Consumers should not manually instantiate builders
- Applications use ASP.NET Core DI / IoC containers
- Different builder implementations may exist for the same model
- Builders need runtime resolution inside services

The factory acts as a centralized builder resolver that integrates cleanly with `IServiceProvider`.

---

# Core Components

## `IBuilderHackerFactory`

Main abstraction for resolving builders.

```csharp
public interface IBuilderHackerFactory
{
    IBuilder<T> CreateBuilder<T>();
    TBuilder CreateBuilder<T, TBuilder>() where TBuilder : IBuilder<T>;
}
```

---

## `DefaultBuilderHackerFactory`

Default implementation that uses `IServiceProvider` internally.

Typical behavior:

1. Resolve the requested builder from DI
2. Validate the resolved type
3. Return the builder instance
4. Throw informative exceptions when registration is missing

---

# Why Use a Factory?

Without a factory:

```csharp
var builder = new PaymentBuilder();
```

Problems:

- Tight coupling
- Hard to mock in tests
- Cannot swap implementations
- Breaks DI patterns
- Difficult to manage generated builders dynamically

With BuilderHacker factory:

```csharp
var builder = factory.CreateBuilder<Payment>();
```

Benefits:

- Fully DI-friendly
- Supports generated builders
- Easier testing/mocking
- Centralized builder resolution
- Runtime flexibility
- Cleaner architecture

---

# Typical Registration

Register the factory once:

```csharp
services.AddSingleton<IBuilderHackerFactory, DefaultBuilderHackerFactory>();
```

Register builders as `Transient` because builders usually hold mutable state.

```csharp
services.AddTransient<IBuilder<Payment>, PaymentBuilder>();
services.AddTransient<PaymentBuilder>();
```

---

# Example: Payment Service

## Payment Model

```csharp
public class Payment
{
    public string TransactionId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

---

## Payment Builder

```csharp
public class PaymentBuilder : IBuilder<Payment>
{
    private readonly Payment _payment = new();

    public PaymentBuilder TransactionId(string id)
    {
        _payment.TransactionId = id;
        return this;
    }

    public PaymentBuilder Amount(decimal amount)
    {
        _payment.Amount = amount;
        return this;
    }

    public PaymentBuilder Currency(string currency)
    {
        _payment.Currency = currency;
        return this;
    }

    public PaymentBuilder CreatedNow()
    {
        _payment.CreatedAt = DateTime.UtcNow;
        return this;
    }

    public Payment Build()
    {
        return _payment;
    }
}
```

---

# Using the Factory Inside a Service

Instead of directly creating builders, services can request the factory.

## Payment Service

```csharp
public class PaymentService
{
    private readonly IBuilderHackerFactory _factory;

    public PaymentService(IBuilderHackerFactory factory)
    {
        _factory = factory;
    }

    public Payment CreatePayment(decimal amount)
    {
        var builder = _factory.CreateBuilder<Payment, PaymentBuilder>();

        return builder
            .TransactionId(Guid.NewGuid().ToString())
            .Amount(amount)
            .Currency("USD")
            .CreatedNow()
            .Build();
    }
}
```

This keeps the service:

- Decoupled
- Testable
- Clean
- Builder-agnostic

---

# ASP.NET Core Registration Example

```csharp
builder.Services.AddSingleton<IBuilderHackerFactory, DefaultBuilderHackerFactory>();

builder.Services.AddTransient<IBuilder<Payment>, PaymentBuilder>();
builder.Services.AddTransient<PaymentBuilder>();

builder.Services.AddScoped<PaymentService>();
```

---

# Resolving Generic Builders

Sometimes consumers only know the model type.

```csharp
var builder = factory.CreateBuilder<Payment>();

var payment = builder.Build();
```

This resolves:

```csharp
IBuilder<Payment>
```

from the container.

---

# Resolving Concrete Builder Types

Preferred when fluent APIs exist on the concrete builder.

```csharp
var paymentBuilder =
    factory.CreateBuilder<Payment, PaymentBuilder>();

var payment = paymentBuilder
    .Amount(200)
    .Currency("EUR")
    .Build();
```

This provides:

- Full IntelliSense
- Fluent builder methods
- Strong typing
- Generated API support

---

# Multiple Builder Implementations

The factory also allows swapping implementations.

Example:

```csharp
services.AddTransient<IBuilder<Payment>, StripePaymentBuilder>();
```

Later:

```csharp
var builder = factory.CreateBuilder<Payment>();
```

The consumer does not need to know which builder implementation is used.

This is useful for:

- Multi-tenant systems
- Payment providers
- Testing
- Feature flags
- Environment-specific behavior

---

# Example: Order Builder

## Order Model

```csharp
public class Order
{
    public int Id { get; set; }
    public string Customer { get; set; }
    public decimal Total { get; set; }
}
```

---

## Order Builder

```csharp
public class OrderBuilder : IBuilder<Order>
{
    private readonly Order _order = new();

    public OrderBuilder Id(int id)
    {
        _order.Id = id;
        return this;
    }

    public OrderBuilder Customer(string name)
    {
        _order.Customer = name;
        return this;
    }

    public OrderBuilder Total(decimal total)
    {
        _order.Total = total;
        return this;
    }

    public Order Build()
    {
        return _order;
    }
}
```

---

## Usage

```csharp
var orderBuilder =
    factory.CreateBuilder<Order, OrderBuilder>();

var order = orderBuilder
    .Id(1001)
    .Customer("Alice")
    .Total(450)
    .Build();
```

---

# How `DefaultBuilderHackerFactory` Works Internally

Simplified implementation:

```csharp
public class DefaultBuilderHackerFactory : IBuilderHackerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DefaultBuilderHackerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IBuilder<T> CreateBuilder<T>()
    {
        return _serviceProvider.GetRequiredService<IBuilder<T>>();
    }

    public TBuilder CreateBuilder<T, TBuilder>()
        where TBuilder : IBuilder<T>
    {
        var concrete = _serviceProvider.GetService<TBuilder>();

        if (concrete != null)
            return concrete;

        var generic = _serviceProvider.GetService<IBuilder<T>>();

        if (generic is TBuilder typed)
            return typed;

        throw new InvalidOperationException(
            $"Unable to resolve builder of type {typeof(TBuilder).Name}");
    }
}
```

---

# Builder Lifetime Recommendations

## Recommended: `Transient`

```csharp
services.AddTransient<PaymentBuilder>();
```

Why?

Builders are usually:

- Mutable
- Stateful
- Fluent
- Temporary

A fresh instance prevents accidental shared state.

---

# Testing Benefits

Mocking becomes simple.

```csharp
var factoryMock = new Mock<IBuilderHackerFactory>();
```

You can inject fake builders during tests without modifying application logic.

---

# Best Practices

## Register Builders as Transient

```csharp
services.AddTransient<UserBuilder>();
```

Avoid singleton builders because state may leak between requests.

---

## Prefer Concrete Builder Resolution

Prefer:

```csharp
CreateBuilder<User, UserBuilder>()
```

when fluent methods exist.

This gives better IntelliSense and stronger typing.

---

## Keep Builders Focused

Builders should only:

- Construct objects
- Apply configuration
- Handle defaults

Avoid putting business logic inside builders.

---

# Common Use Cases

Builder factories work especially well for:

- Generated builders
- DTO construction
- Test data generation
- Payment systems
- Complex object graphs
- API request builders
- Domain model creation
- Multi-provider systems
- Dynamic runtime resolution

---

# Example: Dynamic Payment Provider

```csharp
public interface IPaymentBuilder : IBuilder<Payment>
{
}
```

```csharp
public class StripePaymentBuilder : IPaymentBuilder
{
}
```

```csharp
public class PaypalPaymentBuilder : IPaymentBuilder
{
}
```

At runtime:

```csharp
IPaymentBuilder builder =
    isStripe
        ? factory.CreateBuilder<Payment, StripePaymentBuilder>()
        : factory.CreateBuilder<Payment, PaypalPaymentBuilder>();
```

This pattern is extremely useful in enterprise systems.

---

# Summary

`IBuilderHackerFactory` provides:

- Runtime builder resolution
- Full DI integration
- Cleaner architecture
- Better testing support
- Flexible generated builder usage
- Decoupled services
- Strong typing for fluent builders

It is especially powerful when combined with generated builders and modern ASP.NET Core dependency injection patterns.