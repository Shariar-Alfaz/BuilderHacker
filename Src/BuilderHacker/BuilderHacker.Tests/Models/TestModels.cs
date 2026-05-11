using BuilderHacker.Abstraction.Attributes;
using System;
using System.Collections.Generic;

namespace BuilderHacker.Tests.Models
{
    /// <summary>
    /// Simple model with only public properties for standalone builder testing.
    /// </summary>
    [GenerateBuilderHacker(false)]
    public class SimpleUser
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }

    /// <summary>
    /// Partial class generation test model.
    /// </summary>
    [GenerateBuilderHacker(true)]
    public partial class PartialUser
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Base class with protected property for inheritance testing.
    /// </summary>
    public class BaseEntity
    {
        protected Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    /// <summary>
    /// Derived class for testing inherited property accessibility.
    /// </summary>
    [GenerateBuilderHacker(false)]
    public class DerivedEntity : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// Model with public property and private field.
    /// </summary>
    [GenerateBuilderHacker(false)]
    public class MixedAccessor
    {
        private string _internalId;

        public string Name { get; set; }
        public string PublicId { get; set; }

        public string GetInternalId => _internalId;
    }

    /// <summary>
    /// Model with internal properties for testing access control.
    /// </summary>
    [GenerateBuilderHacker(false)]
    public class InternalPropertyModel
    {
        public string PublicName { get; set; }
        internal string InternalCode { get; set; }
        private string PrivateSecret { get; set; }
    }

    /// <summary>
    /// Model with no settable properties (edge case).
    /// </summary>
    [GenerateBuilderHacker(false)]
    public class ReadOnlyModel
    {
        public string Value { get; }

        public ReadOnlyModel(string value)
        {
            Value = value;
        }
    }

    /// <summary>
    /// Model with static and non-static properties.
    /// </summary>
    [GenerateBuilderHacker(false)]
    public class StaticPropertyModel
    {
        public static int GlobalCount { get; set; }
        public string InstanceName { get; set; }
    }

    /// <summary>
    /// Sealed class for testing sealed type support.
    /// Use partial mode since standalone can't derive from sealed class.
    /// </summary>
    [GenerateBuilderHacker(true)]
    public sealed partial class SealedUser
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    /// <summary>
    /// Static class with static properties (cannot be instantiated).
    /// </summary>
    public static class StaticOnlyClass
    {
        public static string ConfigValue { get; set; }
        public static int ConfigCount { get; set; }
    }

    /// <summary>
    /// Model with all property access levels.
    /// </summary>
    [GenerateBuilderHacker(false)]
    public class AllAccessLevelsModel
    {
        // Public (should be included)
        public string PublicProperty { get; set; }

        // Protected (should be included in standalone)
        protected string ProtectedProperty { get; set; }

        // Internal (should be excluded)
        internal string InternalProperty { get; set; }

        // Private (should be excluded)
        private string PrivateProperty { get; set; }

        // Public read, public write
        public string PublicPublic { get; set; }

        // Public read, protected write
        public string PublicProtectedWrite { get; protected set; }

        // Public read, internal write
        public string PublicInternalWrite { get; internal set; }

        // Public read, private write
        public string PublicPrivateWrite { get; private set; }

        // Protected read, protected write
        protected string ProtectedProtected { get; set; }

        // Internal read, internal write
        internal string InternalInternal { get; set; }

        // Public read-only (no setter)
        public string ReadOnly { get; }

        public AllAccessLevelsModel()
        {
            ReadOnly = "default";
        }
    }

    /// <summary>
    /// Model with auto-properties and expression-bodied properties.
    /// </summary>
    [GenerateBuilderHacker(false)]
    public class PropertyVariationsModel
    {
        // Auto-property
        public string AutoProperty { get; set; }

        // Full property with backing field
        private string _fullProperty;
        public string FullProperty
        {
            get { return _fullProperty; }
            set { _fullProperty = value; }
        }

        // Expression-bodied property (getter)
        public string ComputedProperty => $"Computed_{AutoProperty}";

        // Nullable reference type
        public string? NullableProperty { get; set; }
    }

    /// <summary>
    /// Base class with various property types for inheritance testing.
    /// </summary>
    public class AdvancedBase
    {
        public string BasePublic { get; set; }
        protected string BaseProtected { get; set; }
        internal string BaseInternal { get; set; }
        private string BasePrivate { get; set; }
        public static string BaseStatic { get; set; }
    }

    /// <summary>
    /// Derived class with property override.
    /// </summary>
    [GenerateBuilderHacker(false)]
    public class AdvancedDerived : AdvancedBase
    {
        public string DerivedPublic { get; set; }
        protected string DerivedProtected { get; set; }
        internal string DerivedInternal { get; set; }
    }

    /// <summary>
    /// Model with list and collection properties.
    /// </summary>
    [GenerateBuilderHacker(false)]
    public class CollectionPropertiesModel
    {
        public List<string> StringList { get; set; }
        public List<int> IntList { get; set; }
        public string[] StringArray { get; set; }
        public int[] IntArray { get; set; }
    }

    /// <summary>
    /// Model with nested/complex types.
    /// </summary>
    [GenerateBuilderHacker(false)]
    public class ComplexTypesModel
    {
        public SimpleUser UserObject { get; set; }
        public DateTime DateTime { get; set; }
        public Guid UniqueId { get; set; }
        public decimal Price { get; set; }
        public byte[] BinaryData { get; set; }
    }

    /// <summary>
    /// Model with nullable properties.
    /// </summary>
    [GenerateBuilderHacker(false)]
    public class NullablePropertiesModel
    {
        public string NonNullable { get; set; }
        public string? NullableString { get; set; }
        public int? NullableInt { get; set; }
        public DateTime? NullableDateTime { get; set; }
        public Guid? NullableGuid { get; set; }
    }

    /// <summary>
    /// Model with indexed properties.
    /// </summary>
    [GenerateBuilderHacker(false)]
    public class IndexedPropertiesModel
    {
        private Dictionary<string, string> _storage = new();

        public string Name { get; set; }

        // Note: Indexed properties are not included in builder generation
        // (they require parameters, builder methods don't support them)
    }

    /// <summary>
    /// Abstract class for testing (cannot be instantiated directly).
    /// </summary>
    public abstract class AbstractBase
    {
        public string Name { get; set; }
        public abstract string AbstractProperty { get; }
    }

    /// <summary>
    /// Concrete implementation of abstract class.
    /// </summary>
    [GenerateBuilderHacker(false)]
    public class ConcreteImplementation : AbstractBase
    {
        private string _abstractValue = "default";

        public override string AbstractProperty => _abstractValue;

        public string ConcreteProperty { get; set; }

        public void SetAbstractValue(string value)
        {
            _abstractValue = value;
        }
    }

    /// <summary>
    /// Model with interface implementation.
    /// </summary>
    public interface IEntity
    {
        string Id { get; set; }
        string Name { get; }
    }

    [GenerateBuilderHacker(false)]
    public class InterfaceImplementation : IEntity
    {
        public string Id { get; set; }
        public string Name { get; private set; }
        public string Title { get; set; }

        public InterfaceImplementation()
        {
            Name = "default";
        }
    }

    /// <summary>
    /// Generic model (with type parameter).
    /// Note: Source generator doesn't generate builders for generic types.
    /// Use EntityBuilder for generic scenarios or create specific instances.
    /// </summary>
    public class GenericModel<T>
    {
        public T Value { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// Specific string-based generic instance for testing.
    /// </summary>
    [GenerateBuilderHacker(false)]
    public class StringGenericModel
    {
        public string Value { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// Specific int-based generic instance for testing.
    /// </summary>
    [GenerateBuilderHacker(false)]
    public class IntGenericModel
    {
        public int Value { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// Base class with properties that will be shadowed in derived class.
    /// </summary>
    public class ShadowingBase
    {
        public string Name { get; set; }
        public int Version { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// Derived class that shadows inherited properties using 'new' keyword.
    /// The generator should use the derived (shadowing) properties, not the base ones.
    /// </summary>
    [GenerateBuilderHacker(false)]
    public class ShadowingDerived : ShadowingBase
    {
        // Shadowing the base Name property with new keyword
        public new string Name { get; set; }

        // Not shadowed - inherited from base
        // public int Version { get; set; }

        // Adding a new property at this level
        public string Category { get; set; }
    }
}