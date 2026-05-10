using BuilderHacker.Abstraction.Engine;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace BuilderHacker.Core.EntityBuilder
{
    /// <summary>
    /// Generic entity builder for fluently constructing instances of type T with optional strict mode.
    /// Compatible with: .NET Framework 4.5+, .NET Standard 2.0+, .NET Core 2.0+, .NET 5+, .NET 6+.
    /// </summary>
    /// <remarks>
    /// This builder uses reflection to dynamically set properties and fields on runtime objects.
    /// It provides a fluent API for building entities without requiring source generation.
    /// For .NET 6.0+ projects, consider using GenerateBuilderHacker attribute with the source generator instead.
    /// </remarks>
    /// <typeparam name="T">The type of entity to build. Must have a parameterless constructor.</typeparam>
    public class EntityBuilder<T> : IBuilder<T> where T : new()
    {
        private readonly T _entity = new T();
        private bool _strictMode = false;

        /// <summary>
        /// Creates a new instance of the EntityBuilder for type T.
        /// </summary>
        /// <returns>A new EntityBuilder instance.</returns>
        public static EntityBuilder<T> Create() => new EntityBuilder<T>();

        /// <summary>
        /// Enables or disables strict mode for the entity builder.
        /// </summary>
        /// <remarks>Strict mode may affect how the builder validates entities and handles errors.
        /// Enabling strict mode is recommended when input data must conform strictly to the entity's schema.</remarks>
        /// <param name="enabled">true to enable strict mode; otherwise, false. When enabled, the builder enforces stricter validation rules.</param>
        /// <returns>The current instance of the entity builder with the updated strict mode setting.</returns>
        public EntityBuilder<T> StrictMode(bool enabled = true)
        {
            _strictMode = enabled;
            return this;
        }

        /// <summary>
        /// Sets the value of a property or, if strict mode is disabled, a field on the entity being built by name.
        /// </summary>
        /// <remarks>If strict mode is enabled, only properties can be set; fields are ignored. If strict
        /// mode is disabled, the method will attempt to set a field if a matching property is not found.
        /// This method is compatible with all supported .NET frameworks.
        /// Note: Member lookups are cached for performance. The cache is per-process and shared across all EntityBuilder instances.</remarks>
        /// <typeparam name="TProp">The type of the value to assign to the property or field.</typeparam>
        /// <param name="name">The name of the property or field to set. The search is case-insensitive.</param>
        /// <param name="value">The value to assign to the specified property or field.</param>
        /// <returns>The current instance of the builder, enabling method chaining.</returns>
        /// <exception cref="ArgumentException">Thrown when property or field name is null or empty.</exception>
        /// <exception cref="InvalidOperationException">Thrown if no property or (when strict mode is off) field with the specified name exists on the entity type.</exception>
        public EntityBuilder<T> Set<TProp>(string name, TProp value)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Property or field name cannot be null or empty", nameof(name));

            var type = typeof(T);
            MemberInfo member;

            // Use cached reflection lookup for performance
            if (ReflectionMemberCache.TryGetMember(type, name, out member))
            {
                var prop = member as PropertyInfo;
                if (prop != null)
                {
                    SetProperty(prop, value);
                    return this;
                }

                var field = member as FieldInfo;
                if (field != null && !_strictMode)
                {
                    field.SetValue(_entity, value);
                    return this;
                }

                // If in strict mode and member is a field, skip it
                if (_strictMode && field != null)
                {
                    throw new InvalidOperationException(
                        $"Property or field '{name}' is a field, but strict mode is enabled. Only properties can be set in strict mode.");
                }
            }

            throw new InvalidOperationException(
                $"Property or field '{name}' not found on type '{type.Name}'.");
        }

        /// <summary>
        /// Sets the value of the specified property or field on the entity being built using an expression.
        /// </summary>
        /// <remarks>Only direct properties or fields of the entity type are supported. This method
        /// enables fluent configuration of entity values with compile-time safety.
        /// Compatible with .NET Framework 4.5+, .NET Standard 2.0+, and all newer .NET versions.</remarks>
        /// <typeparam name="TProp">The type of the property or field to set.</typeparam>
        /// <param name="expression">An expression that identifies the property or field to set. Must refer to a direct property or field of the entity.</param>
        /// <param name="value">The value to assign to the specified property or field.</param>
        /// <returns>The current instance of the builder for method chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when expression is null.</exception>
        /// <exception cref="Exception">Thrown if the expression does not refer to a valid property or field of the entity, or if the member is not supported.</exception>
        public EntityBuilder<T> Set<TProp>(Expression<Func<T, TProp>> expression, TProp value)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            var member = GetMemberExpression(expression.Body);

            if (member == null)
                throw new InvalidOperationException(
                    "Invalid expression. Only direct property or field access is supported.");

            var memberInfo = member.Member;

            // Using 'as' operator for compatibility with all .NET versions (no pattern matching)
            var prop = memberInfo as PropertyInfo;
            if (prop != null)
            {
                SetProperty(prop, value);
                return this;
            }

            var field = memberInfo as FieldInfo;
            if (field != null)
            {
                if (_strictMode)
                    throw new InvalidOperationException(
                        $"Field '{field.Name}' cannot be set in strict mode. Only properties are allowed.");

                field.SetValue(_entity, value);
                return this;
            }

            throw new InvalidOperationException(
                $"Unsupported member type '{memberInfo.MemberType}'. Only properties and fields are supported.");
        }

        private void SetProperty<TProp>(PropertyInfo prop, TProp value)
        {
            if (prop == null)
                throw new ArgumentNullException(nameof(prop));

            if (!prop.CanWrite)
            {
                var setter = prop.GetSetMethod(true);

                if (setter == null)
                    throw new InvalidOperationException(
                        $"Property '{prop.Name}' on type '{prop.DeclaringType?.Name}' has no setter.");

                try
                {
                    setter.Invoke(_entity, new object[] { value });
                }
                catch (TargetInvocationException ex)
                {
                    // Unwrap the inner exception for clarity
                    throw new InvalidOperationException(
                        $"Error setting property '{prop.Name}': {ex.InnerException?.Message}",
                        ex.InnerException);
                }
                return;
            }

            try
            {
                prop.SetValue(_entity, value);
            }
            catch (TargetInvocationException ex)
            {
                throw new InvalidOperationException(
                    $"Error setting property '{prop.Name}': {ex.InnerException?.Message}",
                    ex.InnerException);
            }
        }

        private MemberExpression GetMemberExpression(Expression expression)
        {
            // Traditional if/else chains for maximum framework compatibility
            if (expression == null)
                return null;

            var member = expression as MemberExpression;
            if (member != null)
                return member;

            var unary = expression as UnaryExpression;
            if (unary != null)
            {
                member = unary.Operand as MemberExpression;
                if (member != null)
                    return member;
            }

            return null;
        }

        /// <summary>
        /// Builds and returns the configured entity instance of type T.
        /// </summary>
        /// <remarks>
        /// Subsequent calls to this method return the same instance unless the builder is reset
        /// or reconfigured. The returned entity may be incomplete if required properties were not set prior to calling this method.
        /// 
        /// THREAD SAFETY WARNING: This builder is NOT thread-safe. Each thread must have its own builder instance.
        /// If sharing builders across threads is necessary, use synchronization:
        /// <code>
        /// lock (builderInstance)
        /// {
        ///     builderInstance.PropertyName(value).Build();
        /// }
        /// </code>
        /// 
        /// Alternatively, use ThreadLocal&lt;T&gt; for thread-local builders:
        /// <code>
        /// private static readonly ThreadLocal&lt;EntityBuilder&lt;MyEntity&gt;&gt; _builder = 
        ///     new ThreadLocal&lt;EntityBuilder&lt;MyEntity&gt;&gt;(() => EntityBuilder&lt;MyEntity&gt;.Create());
        /// </code>
        /// </remarks>
        /// <returns>The constructed entity of type T with all applied configurations.</returns>
        public T Build()
        {
            return _entity;
        }
    }
}
