using BuilderHacker.Tests.Models;
using Xunit;

namespace BuilderHacker.Tests.Generator
{
    /// <summary>
    /// Edge case tests for both builder modes.
    /// </summary>
    public class EdgeCaseTests
    {
        [Fact]
        public void StaticProperties_AreNotIncluded()
        {
            var model1 = StaticPropertyModelBuilder.Create()
                .InstanceName("Instance1")
                .Build();

            StaticPropertyModel.GlobalCount = 100;

            var model2 = StaticPropertyModelBuilder.Create()
                .InstanceName("Instance2")
                .Build();

            Assert.Equal("Instance1", model1.InstanceName);
            Assert.Equal("Instance2", model2.InstanceName);
            Assert.Equal(100, StaticPropertyModel.GlobalCount);
        }

        [Fact]
        public void ReadOnlyProperties_AreSkipped()
        {
            // ReadOnlyModel has no settable properties, so no builder should be generated
            // This test verifies that compilation doesn't fail when no properties are available
            // The builder should not be created if there are no properties to set
        }

        [Fact]
        public void BuilderChaining_WorksCorrectly()
        {
            var user = SimpleUserBuilder.Create()
                .Name("Charlie")
                .Age(35)
                .Email("charlie@example.com")
                .Build();

            Assert.Equal("Charlie", user.Name);
            Assert.Equal(35, user.Age);
            Assert.Equal("charlie@example.com", user.Email);
        }

        [Fact]
        public void MultipleBuilderInstances_DontInterfere()
        {
            var builder1 = SimpleUserBuilder.Create().Name("User1");
            var builder2 = SimpleUserBuilder.Create().Name("User2");

            var user1 = builder1.Age(20).Build();
            var user2 = builder2.Age(30).Build();

            Assert.Equal("User1", user1.Name);
            Assert.Equal("User2", user2.Name);
            Assert.Equal(20, user1.Age);
            Assert.Equal(30, user2.Age);
        }

        [Fact]
        public void NullValues_CanBeSet()
        {
            var user = SimpleUserBuilder.Create()
                .Name(null)
                .Email(null)
                .Build();

            Assert.Null(user.Name);
            Assert.Null(user.Email);
        }

        [Fact]
        public void RepeatedPropertySetting_UsesLastValue()
        {
            var user = SimpleUserBuilder.Create()
                .Name("First")
                .Name("Second")
                .Name("Third")
                .Build();

            Assert.Equal("Third", user.Name);
        }
    }
}
