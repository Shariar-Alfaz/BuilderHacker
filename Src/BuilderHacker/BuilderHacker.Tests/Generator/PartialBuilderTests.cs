using BuilderHacker.Tests.Models;
using Xunit;

namespace BuilderHacker.Tests.Generator
{
    /// <summary>
    /// Tests for partial class (nested) builder generation mode.
    /// </summary>
    public class PartialBuilderTests
    {
        [Fact]
        public void Builder_WithPublicProperties_BuildsSuccessfully()
        {
            var user = PartialUser.Builder()
                .Name("Alice")
                .Age(28)
                .CreatedAt(System.DateTime.Now)
                .Build();

            Assert.NotNull(user);
            Assert.Equal("Alice", user.Name);
            Assert.Equal(28, user.Age);
            Assert.NotEqual(default(System.DateTime), user.CreatedAt);
        }

        [Fact]
        public void Builder_PartialChaining_BuildsSuccessfully()
        {
            var user = PartialUser.Builder()
                .Name("Bob")
                .Build();

            Assert.NotNull(user);
            Assert.Equal("Bob", user.Name);
            Assert.Equal(0, user.Age);
        }

        [Fact]
        public void Builder_MultipleInstances_AreIndependent()
        {
            var user1 = PartialUser.Builder()
                .Name("User1")
                .Age(20)
                .Build();

            var user2 = PartialUser.Builder()
                .Name("User2")
                .Age(30)
                .Build();

            Assert.NotEqual(user1.Name, user2.Name);
            Assert.NotEqual(user1.Age, user2.Age);
        }

        [Fact]
        public void Builder_ReturnsSelf_ForChaining()
        {
            var builder = PartialUser.Builder()
                .Name("Test");

            var result = builder.Age(25);

            Assert.Same(builder, result);
        }
    }
}
