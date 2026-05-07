using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using BuilderHacker.Abstraction.Engine;

namespace BuilderHacker.Core.EntityBuilder
{
    public class EntityBuilder<T> : IBuilder<T> where T : new()
    {
        private readonly T _entity = new T();

        private bool _strictMode = false;
        public static EntityBuilder<T> Create() => new();

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
        /// mode is disabled, the method will attempt to set a field if a matching property is not found.</remarks>
        /// <typeparam name="TProp">The type of the value to assign to the property or field.</typeparam>
        /// <param name="name">The name of the property or field to set. The search is case-insensitive.</param>
        /// <param name="value">The value to assign to the specified property or field.</param>
        /// <returns>The current instance of the builder, enabling method chaining.</returns>
        /// <exception cref="Exception">Thrown if no property or (when strict mode is off) field with the specified name exists on the entity type.</exception>
        public EntityBuilder<T> Set<TProp>(string name, TProp value)
        {
            var type = typeof(T);

            // 1. Try property first
            var prop = type.GetProperty(name,
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.IgnoreCase);

            if (prop != null)
            {
                SetProperty(prop, value);
                return this;
            }

            // 2. Try field (only if strict mode is OFF)
            if (!_strictMode)
            {
                var field = type.GetField(name,
                    BindingFlags.NonPublic |
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.IgnoreCase);

                if (field != null)
                {
                    field.SetValue(_entity, value);
                    return this;
                }
            }

            throw new Exception($"Property or field '{name}' not found on {type.Name}");
        }


        /// <summary>
        /// Sets the value of the specified property or field on the entity being built.
        /// </summary>
        /// <remarks>Only direct properties or fields of the entity type are supported. This method
        /// enables fluent configuration of entity values.</remarks>
        /// <typeparam name="TProp">The type of the property or field to set.</typeparam>
        /// <param name="expression">An expression that identifies the property or field to set. Must refer to a direct property or field of the
        /// entity.</param>
        /// <param name="value">The value to assign to the specified property or field.</param>
        /// <returns>The current instance of the builder for method chaining.</returns>
        /// <exception cref="Exception">Thrown if the expression does not refer to a valid property or field of the entity, or if the member is not
        /// supported.</exception>
        public EntityBuilder<T> Set<TProp>(
            Expression<Func<T, TProp>> expression,
            TProp value)
        {
            var member = GetMemberExpression(expression);

            if (member == null)
                throw new Exception("Invalid expression");

            var memberInfo = member.Member;

            if (memberInfo is PropertyInfo prop)
            {
                SetProperty(prop, value);
                return this;
            }

            if (memberInfo is FieldInfo field)
            {
                field.SetValue(_entity, value);
                return this;
            }

            throw new Exception("Only fields or properties are supported");
        }

        private void SetProperty<TProp>(System.Reflection.PropertyInfo prop, TProp value)
        {
            if (!prop.CanWrite)
            {
                // try private setter
                var setter = prop.GetSetMethod(true);

                if (setter == null)
                    throw new Exception($"Property '{prop.Name}' has no setter");

                setter.Invoke(_entity, new object[] { value });
                return;
            }

            prop.SetValue(_entity, value);
        }

        private MemberExpression GetMemberExpression(Expression expression)
        {
            return expression switch
            {
                MemberExpression member => member,

                UnaryExpression unary when unary.Operand is MemberExpression member =>
                    member,

                _ => null
            };
        }

        /// <summary>
        /// Builds and returns the configured entity instance of type T.
        /// </summary>
        /// <remarks>Subsequent calls to this method return the same instance unless the builder is reset
        /// or reconfigured. The returned entity may be incomplete if required properties were not set prior to calling
        /// this method.</remarks>
        /// <returns>The constructed entity of type T with all applied configurations.</returns>
        public T Build() => _entity;
    }
}
