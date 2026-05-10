using BuilderHacker.Tests.Models;
using Xunit;

namespace BuilderHacker.Tests.Generator
{
    /// <summary>
    /// Tests for standalone (derived) builder generation mode.
    /// </summary>
    public class StandaloneBuilderTests
    {
        [Fact]
        public void Create_WithPublicProperties_BuildsSuccessfully()
        {
            var user = SimpleUserBuilder.Create()
                .Name("John")
                .Age(30)
                .Email("john@example.com")
                .Build();

            Assert.NotNull(user);
            Assert.Equal("John", user.Name);
            Assert.Equal(30, user.Age);
            Assert.Equal("john@example.com", user.Email);
        }

        [Fact]
        public void Create_PartialChaining_BuildsSuccessfully()
        {
            var user = SimpleUserBuilder.Create()
                .Name("Jane")
                .Build();

            Assert.NotNull(user);
            Assert.Equal("Jane", user.Name);
            Assert.Equal(0, user.Age);
            Assert.Null(user.Email);
        }

        [Fact]
        public void Create_MultipleInstances_AreIndependent()
        {
            var user1 = SimpleUserBuilder.Create()
                .Name("User1")
                .Age(20)
                .Build();

            var user2 = SimpleUserBuilder.Create()
                .Name("User2")
                .Age(30)
                .Build();

            Assert.NotEqual(user1.Name, user2.Name);
            Assert.NotEqual(user1.Age, user2.Age);
        }

        [Fact]
        public void Create_WithInheritedProperties_Works()
        {
            var entity = DerivedEntityBuilder.Create()
                .CreatedDate(System.DateTime.Now)
                .Title("Test Entity")
                .Description("A test entity instance")
                .Build();

            Assert.NotNull(entity);
            Assert.Equal("Test Entity", entity.Title);
            Assert.Equal("A test entity instance", entity.Description);
            Assert.NotEqual(default(System.DateTime), entity.CreatedDate);
        }

        [Fact]
        public void Create_WithPublicIdOnly_Works()
        {
            var model = MixedAccessorBuilder.Create()
                .Name("Test")
                .PublicId("ID123")
                .Build();

            Assert.Equal("Test", model.Name);
            Assert.Equal("ID123", model.PublicId);
        }

        [Fact]
        public void Create_WithInternalProperties_SkipsInaccessible()
        {
            var model = InternalPropertyModelBuilder.Create()
                .PublicName("Public")
                .Build();

            Assert.Equal("Public", model.PublicName);
        }
    }
}
