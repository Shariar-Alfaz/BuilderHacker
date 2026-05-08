using BuilderHacker.Core.EntityBuilder;
using BuilderHacker.Tests.Models;
using System;
using Xunit;

namespace BuilderHacker.Tests.Core
{
    /// <summary>
    /// Tests for EntityBuilder runtime builder across all frameworks.
    /// </summary>
    public class EntityBuilderTests
    {
        [Fact]
        public void Create_WithExpressionSet_BuildsSuccessfully()
        {
            var user = EntityBuilder<SimpleUser>.Create()
                .Set(x => x.Name, "John")
                .Set(x => x.Age, 30)
                .Build();

            Assert.NotNull(user);
            Assert.Equal("John", user.Name);
            Assert.Equal(30, user.Age);
        }

        [Fact]
        public void Create_WithStringSet_BuildsSuccessfully()
        {
            var user = EntityBuilder<SimpleUser>.Create()
                .Set("Name", "Alice")
                .Set("Age", 25)
                .Build();

            Assert.NotNull(user);
            Assert.Equal("Alice", user.Name);
            Assert.Equal(25, user.Age);
        }

        [Fact]
        public void StrictMode_AllowsOnlyProperties()
        {
            var user = EntityBuilder<SimpleUser>.Create()
                .StrictMode(true)
                .Set(x => x.Name, "Bob")
                .Build();

            Assert.Equal("Bob", user.Name);
        }

        [Fact]
        public void Set_WithInvalidPropertyName_ThrowsException()
        {
            var ex = Assert.Throws<Exception>(() =>
                EntityBuilder<SimpleUser>.Create()
                    .Set("NonExistent", "value")
                    .Build()
            );

            Assert.Contains("not found", ex.Message);
        }

        [Fact]
        public void Set_WithNullPropertyName_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                EntityBuilder<SimpleUser>.Create()
                    .Set((string)null, "value")
            );

            Assert.Contains("cannot be null or empty", ex.Message);
        }

        [Fact]
        public void MultipleCalls_BuildReturnsSameInstance()
        {
            var builder = EntityBuilder<SimpleUser>.Create()
                .Set(x => x.Name, "Charlie");

            var user1 = builder.Build();
            var user2 = builder.Build();

            Assert.Same(user1, user2);
        }
    }
}
