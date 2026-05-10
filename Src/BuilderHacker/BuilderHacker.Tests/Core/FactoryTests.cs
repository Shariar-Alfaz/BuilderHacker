using BuilderHacker.Abstraction.Engine;
using BuilderHacker.Core.Builder;
using BuilderHacker.Tests.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace BuilderHacker.Tests.Core
{
    public class FactoryTests
    {
        [Fact]
        public void Factory_CanResolveGeneratedBuilder()
        {
            // Setup DI container
            var services = new ServiceCollection();
            
            // Register the generated builder as IBuilder<SimpleUser>
            // Note: SimpleUserBuilder is generated and now implements IBuilder<SimpleUser>
            services.AddTransient<IBuilder<SimpleUser>, SimpleUserBuilder>();
            
            // Register the factory
            services.AddSingleton<IBuilderHackerFactory, DefaultBuilderHackerFactory>();
            
            var serviceProvider = services.BuildServiceProvider();
            var factory = serviceProvider.GetRequiredService<IBuilderHackerFactory>();

            // Act
            var builder = factory.CreateBuilder<SimpleUser, SimpleUserBuilder>();
            var user = builder
                .Name("FactoryUser")
                .Age(25)
                .Build();

            // Assert
            Assert.NotNull(builder);
            Assert.IsType<SimpleUserBuilder>(builder);
            Assert.Equal("FactoryUser", user.Name);
            Assert.Equal(25, user.Age);
        }

        [Fact]
        public void Factory_ThrowsWhenBuilderNotRegistered()
        {
            // Setup DI container
            var services = new ServiceCollection();
            services.AddSingleton<IBuilderHackerFactory, DefaultBuilderHackerFactory>();
            
            var serviceProvider = services.BuildServiceProvider();
            var factory = serviceProvider.GetRequiredService<IBuilderHackerFactory>();

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => factory.CreateBuilder<PartialUser>());
            Assert.Contains("No builder registered", ex.Message);
        }
    }
}
