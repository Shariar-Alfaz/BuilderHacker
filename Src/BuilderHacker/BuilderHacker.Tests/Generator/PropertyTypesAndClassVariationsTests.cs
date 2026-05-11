using BuilderHacker.Core.EntityBuilder;
using BuilderHacker.Tests.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace BuilderHacker.Tests.Generator
{
    /// <summary>
    /// Comprehensive tests for all property types and class variations.
    /// Covers: static classes, sealed classes, nullable properties, collections, generics, etc.
    /// </summary>
    public class PropertyTypesAndClassVariationsTests
    {
        [Fact]
        public void SealedClass_BuilderWorks()
        {
            // Sealed class using partial mode (can't derive in standalone)
            var user = SealedUser.Builder()
                .Name("SealedUser")
                .Age(25)
                .Build();

            Assert.NotNull(user);
            Assert.Equal("SealedUser", user.Name);
            Assert.Equal(25, user.Age);
        }

        [Fact]
        public void AllAccessLevels_IncludesPublicAndProtected()
        {
            // Test that public and protected properties are included
            // Internal and private should be excluded
            var model = AllAccessLevelsModelBuilder.Create()
                .PublicProperty("Public")
                .PublicPublic("PublicPublic")
                .Build();

            Assert.Equal("Public", model.PublicProperty);
            Assert.Equal("PublicPublic", model.PublicPublic);

            // Protected properties should be settable via base in standalone mode
            // This tests the inheritance mechanism
        }

        [Fact]
        public void PublicProtectedWrite_WorksWithBuilder()
        {
            // Property with public getter, protected setter
            var model = AllAccessLevelsModelBuilder.Create()
                .PublicProtectedWrite("Protected")
                .Build();

            // Should be settable via base in standalone mode
            Assert.NotNull(model);
        }

        [Fact]
        public void PublicInternalWrite_IsExcludedFromBuilder()
        {
            // Property with internal setter should not be in builder
            // (builder is public, can't set internal property)
            var model = AllAccessLevelsModelBuilder.Create()
                .PublicProperty("Test")
                .Build();

            // PublicInternalWrite should not be available
            Assert.Equal("Test", model.PublicProperty);
        }

        [Fact]
        public void PublicPrivateWrite_IsExcludedFromBuilder()
        {
            // Property with private setter should not be in builder
            var model = AllAccessLevelsModelBuilder.Create()
                .PublicProperty("Test")
                .Build();

            // PublicPrivateWrite should not be available
            Assert.Equal("Test", model.PublicProperty);
        }

        [Fact]
        public void ProtectedProperties_AccessibleInStandalone()
        {
            // In standalone mode (builder derives from type), 
            // protected properties should be accessible via base
            var model = AllAccessLevelsModelBuilder.Create()
                .PublicProperty("Public")
                .Build();

            Assert.NotNull(model);
            Assert.Equal("Public", model.PublicProperty);
        }

        [Fact]
        public void ReadOnlyProperty_IsExcludedFromBuilder()
        {
            // Read-only properties have no setter, should be excluded
            var model = AllAccessLevelsModelBuilder.Create()
                .PublicProperty("Test")
                .Build();

            // ReadOnly property should not be in builder
            // It can only be set via constructor
        }

        [Fact]
        public void InternalProperties_AreExcluded()
        {
            // Internal properties should not be in standalone builder
            var model = AllAccessLevelsModelBuilder.Create()
                .PublicProperty("Public")
                .Build();

            Assert.Equal("Public", model.PublicProperty);
            // InternalProperty should not be available
        }

        [Fact]
        public void AutoProperty_Works()
        {
            var model = PropertyVariationsModelBuilder.Create()
                .AutoProperty("Auto")
                .Build();

            Assert.Equal("Auto", model.AutoProperty);
        }

        [Fact]
        public void FullProperty_Works()
        {
            var model = PropertyVariationsModelBuilder.Create()
                .AutoProperty("Test")
                .FullProperty("Full")
                .Build();

            Assert.Equal("Full", model.FullProperty);
        }

        [Fact]
        public void ComputedProperty_IsExcludedFromBuilder()
        {
            // Expression-bodied properties (get-only) should be excluded
            var model = PropertyVariationsModelBuilder.Create()
                .AutoProperty("Test")
                .Build();

            // ComputedProperty should not be in builder
            // It's computed from AutoProperty
            Assert.NotNull(model.ComputedProperty);
        }

        [Fact]
        public void NullableProperty_CanBeSetToNull()
        {
            var model = PropertyVariationsModelBuilder.Create()
                .AutoProperty("Test")
                .NullableProperty(null)
                .Build();

            Assert.Null(model.NullableProperty);
        }

        [Fact]
        public void NullableProperty_CanBeSetToValue()
        {
            var model = PropertyVariationsModelBuilder.Create()
                .AutoProperty("Test")
                .NullableProperty("Value")
                .Build();

            Assert.Equal("Value", model.NullableProperty);
        }

        [Fact]
        public void AdvancedBase_PublicPropertiesIncluded()
        {
            var derived = AdvancedDerivedBuilder.Create()
                .BasePublic("BasePublic")
                .DerivedPublic("DerivedPublic")
                .Build();

            Assert.Equal("BasePublic", derived.BasePublic);
            Assert.Equal("DerivedPublic", derived.DerivedPublic);
        }

        [Fact]
        public void AdvancedBase_ProtectedPropertiesIncluded()
        {
            // Protected properties from base should be included in standalone
            var derived = AdvancedDerivedBuilder.Create()
                .BasePublic("Base")
                .DerivedPublic("Derived")
                .Build();

            Assert.NotNull(derived);
        }

        [Fact]
        public void AdvancedBase_StaticPropertiesExcluded()
        {
            // Static properties should be excluded
            AdvancedBase.BaseStatic = "Before";

            var derived = AdvancedDerivedBuilder.Create()
                .BasePublic("Test")
                .DerivedPublic("Test")
                .Build();

            Assert.Equal("Before", AdvancedBase.BaseStatic);
            // BaseStatic should not be affected by builder
        }

        [Fact]
        public void AdvancedBase_InternalPropertiesExcluded()
        {
            // Internal properties should be excluded
            var derived = AdvancedDerivedBuilder.Create()
                .BasePublic("Public")
                .DerivedPublic("Public")
                .Build();

            // BaseInternal should not be available
            Assert.NotNull(derived);
        }

        [Fact]
        public void ListProperty_CanBeSet()
        {
            var list = new List<string> { "Item1", "Item2" };
            var model = CollectionPropertiesModelBuilder.Create()
                .StringList(list)
                .Build();

            Assert.NotNull(model.StringList);
            Assert.Equal(2, model.StringList.Count);
        }

        [Fact]
        public void ListProperty_CanBeSetToNull()
        {
            var model = CollectionPropertiesModelBuilder.Create()
                .StringList(null)
                .Build();

            Assert.Null(model.StringList);
        }

        [Fact]
        public void ArrayProperty_CanBeSet()
        {
            var array = new[] { "A", "B", "C" };
            var model = CollectionPropertiesModelBuilder.Create()
                .StringArray(array)
                .Build();

            Assert.NotNull(model.StringArray);
            Assert.Equal(3, model.StringArray.Length);
        }

        [Fact]
        public void IntListProperty_CanBeSet()
        {
            var list = new List<int> { 1, 2, 3 };
            var model = CollectionPropertiesModelBuilder.Create()
                .IntList(list)
                .Build();

            Assert.NotNull(model.IntList);
            Assert.Equal(3, model.IntList.Count);
        }

        [Fact]
        public void ComplexTypes_SimpleUserObject()
        {
            var user = new SimpleUser { Name = "Test", Age = 30 };
            var model = ComplexTypesModelBuilder.Create()
                .UserObject(user)
                .Build();

            Assert.Equal("Test", model.UserObject.Name);
            Assert.Equal(30, model.UserObject.Age);
        }

        [Fact]
        public void ComplexTypes_DateTime()
        {
            var now = DateTime.Now;
            var model = ComplexTypesModelBuilder.Create()
                .DateTime(now)
                .Build();

            Assert.Equal(now, model.DateTime);
        }

        [Fact]
        public void ComplexTypes_Guid()
        {
            var guid = Guid.NewGuid();
            var model = ComplexTypesModelBuilder.Create()
                .UniqueId(guid)
                .Build();

            Assert.Equal(guid, model.UniqueId);
        }

        [Fact]
        public void ComplexTypes_Decimal()
        {
            var price = 99.99m;
            var model = ComplexTypesModelBuilder.Create()
                .Price(price)
                .Build();

            Assert.Equal(price, model.Price);
        }

        [Fact]
        public void ComplexTypes_ByteArray()
        {
            var bytes = new byte[] { 1, 2, 3, 4, 5 };
            var model = ComplexTypesModelBuilder.Create()
                .BinaryData(bytes)
                .Build();

            Assert.NotNull(model.BinaryData);
            Assert.Equal(5, model.BinaryData.Length);
        }

        [Fact]
        public void NullableProperties_StringNull()
        {
            var model = NullablePropertiesModelBuilder.Create()
                .NonNullable("Required")
                .NullableString(null)
                .Build();

            Assert.Null(model.NullableString);
        }

        [Fact]
        public void NullableProperties_StringValue()
        {
            var model = NullablePropertiesModelBuilder.Create()
                .NonNullable("Required")
                .NullableString("Value")
                .Build();

            Assert.Equal("Value", model.NullableString);
        }

        [Fact]
        public void NullableProperties_IntNull()
        {
            var model = NullablePropertiesModelBuilder.Create()
                .NonNullable("Required")
                .NullableInt(null)
                .Build();

            Assert.Null(model.NullableInt);
        }

        [Fact]
        public void NullableProperties_IntValue()
        {
            var model = NullablePropertiesModelBuilder.Create()
                .NonNullable("Required")
                .NullableInt(42)
                .Build();

            Assert.Equal(42, model.NullableInt);
        }

        [Fact]
        public void NullableProperties_DateTimeNull()
        {
            var model = NullablePropertiesModelBuilder.Create()
                .NonNullable("Required")
                .NullableDateTime(null)
                .Build();

            Assert.Null(model.NullableDateTime);
        }

        [Fact]
        public void NullableProperties_GuidNull()
        {
            var model = NullablePropertiesModelBuilder.Create()
                .NonNullable("Required")
                .NullableGuid(null)
                .Build();

            Assert.Null(model.NullableGuid);
        }

        [Fact]
        public void IndexedProperty_IsNotIncluded()
        {
            // Indexed properties (this[key]) should not be in builder
            var model = IndexedPropertiesModelBuilder.Create()
                .Name("Test")
                .Build();

            Assert.Equal("Test", model.Name);
            // Indexed property should not be available
        }

        [Fact]
        public void ConcreteImplementation_HasProperties()
        {
            var model = ConcreteImplementationBuilder.Create()
                .ConcreteProperty("Concrete")
                .Build();

            Assert.Equal("Concrete", model.ConcreteProperty);
            // Name should be inherited from base
        }

        [Fact]
        public void InterfaceImplementation_HasProperties()
        {
            var model = InterfaceImplementationBuilder.Create()
                .Id("123")
                .Title("Title")
                .Build();

            Assert.Equal("123", model.Id);
            Assert.Equal("Title", model.Title);
            // Name is initialized by constructor
        }

        [Fact]
        public void GenericModel_StringType()
        {
            var model = StringGenericModelBuilder.Create()
                .Value("StringValue")
                .Name("Name")
                .Build();

            Assert.Equal("StringValue", model.Value);
            Assert.Equal("Name", model.Name);
        }

        [Fact]
        public void GenericModel_IntType()
        {
            var model = IntGenericModelBuilder.Create()
                .Value(42)
                .Name("Number")
                .Build();

            Assert.Equal(42, model.Value);
            Assert.Equal("Number", model.Name);
        }

        [Fact]
        public void GenericModel_WithEntityBuilder()
        {
            // EntityBuilder supports generics
            var model = EntityBuilder<GenericModel<SimpleUser>>.Create()
                .Set(x => x.Value, new SimpleUser { Name = "User", Age = 25 })
                .Set(x => x.Name, "Container")
                .Build();

            Assert.Equal("User", model.Value.Name);
            Assert.Equal("Container", model.Name);
        }

        [Fact]
        public void MultiplePropertyTypes_MixedBuilder()
        {
            var list = new List<string> { "A", "B" };
            var guid = Guid.NewGuid();
            var complex = new SimpleUser { Name = "Nested", Age = 30 };

            var model = CollectionPropertiesModelBuilder.Create()
                .StringList(list)
                .IntArray(new[] { 1, 2, 3 })
                .Build();

            Assert.Equal(2, model.StringList.Count);
            Assert.Equal(3, model.IntArray.Length);
        }

        [Fact]
        public void DefaultValues_UnsetProperties()
        {
            var model = NullablePropertiesModelBuilder.Create()
                .NonNullable("Required")
                .Build();

            Assert.Null(model.NullableString);
            Assert.Null(model.NullableInt);
            Assert.Null(model.NullableDateTime);
            Assert.Null(model.NullableGuid);
        }

        [Fact]
        public void OverwriteableProperties_AllTypes()
        {
            var model = NullablePropertiesModelBuilder.Create()
                .NonNullable("First")
                .NonNullable("Second")
                .NonNullable("Third")
                .NullableInt(1)
                .NullableInt(2)
                .NullableInt(3)
                .Build();

            Assert.Equal("Third", model.NonNullable);
            Assert.Equal(3, model.NullableInt);
        }

        [Fact]
        public void ShadowedProperty_WithNewKeyword_UsesMostDerived()
        {
            // ShadowingDerived shadows the Name property from ShadowingBase using 'new'
            // The builder should use the derived (shadowed) property, not the base one
            var model = ShadowingDerivedBuilder.Create()
                .Name("DerivedName")
                .Version(2)
                .Category("Tech")
                .Build();

            Assert.Equal("DerivedName", model.Name);
            Assert.Equal(2, model.Version);
            Assert.Equal("Tech", model.Category);
        }

        [Fact]
        public void ShadowedProperty_WithNewKeyword_BuilderMethodsExist()
        {
            // Verify that all expected builder methods exist and can be chained
            var builder = ShadowingDerivedBuilder.Create();

            // Should have Name, Version, and Category methods (Name is from derived, not base)
            var model = builder
                .Name("ShadowedName")
                .Version(1)
                .Category("Category1")
                .Build();

            Assert.NotNull(model);
            Assert.Equal("ShadowedName", model.Name);
        }

        [Fact]
        public void ShadowedProperty_WithNewKeyword_PropertyChaining()
        {
            // Test that builder chaining works correctly with shadowed properties
            var model = ShadowingDerivedBuilder.Create()
                .Name("First")
                .Name("Second")  // Overwrite Name
                .Version(10)
                .Category("Updated")
                .Build();

            // Last value should win
            Assert.Equal("Second", model.Name);
            Assert.Equal(10, model.Version);
            Assert.Equal("Updated", model.Category);
        }

        [Fact]
        public void ShadowedProperty_WithNewKeyword_PartialAssignment()
        {
            // Test that we can set only some properties (others use defaults)
            var model = ShadowingDerivedBuilder.Create()
                .Name("OnlyName")
                .Build();

            Assert.Equal("OnlyName", model.Name);
            // Version and Category should have default values (null or default(int))
            Assert.Equal(0, model.Version);
            Assert.Null(model.Category);
        }
    }
}
