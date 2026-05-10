using BuilderHacker.Abstraction.Engine;
using BuilderHacker.Core.Builder;
using BuilderHacker.Tests.Models.ServiceFactoryModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace BuilderHacker.Tests.Core
{
    public class ServiceFactoryTests
    {
        private readonly IServiceFactory _factory;

        public ServiceFactoryTests()
        {
            _factory = BuildFactory();
        }

        private IServiceFactory BuildFactory()
        {
            var services = new ServiceCollection();

            services.AddTransient<IPaymentService, FakePaymentServiceOne>();
            services.AddTransient<IPaymentService, FakePaymentServiceTwo>();

            services.AddSingleton<IServiceFactory, DefaultServiceFactory>();

            var provider = services.BuildServiceProvider();

            return provider.GetRequiredService<IServiceFactory>();
        }

        [Fact]
        public void ServiceFactory_CanResolveService()
        {
            var paymentService = _factory.Create<IPaymentService>();

            Assert.NotNull(paymentService);
            Assert.IsAssignableFrom<IPaymentService>(paymentService);
        }

        [Fact]
        public void ServiceFactory_CanResolveSpecificImplementation()
        {
            var paymentServiceOne = _factory.Create<IPaymentService, FakePaymentServiceOne>();
            var paymentServiceTwo = _factory.Create<IPaymentService, FakePaymentServiceTwo>();

            Assert.NotNull(paymentServiceOne);
            Assert.NotNull(paymentServiceTwo);

            Assert.IsType<FakePaymentServiceOne>(paymentServiceOne);
            Assert.IsType<FakePaymentServiceTwo>(paymentServiceTwo);
        }

        [Fact]
        public void Create_ShouldThrow_WhenServiceNotRegistered()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IServiceFactory, DefaultServiceFactory>();

            var provider = services.BuildServiceProvider();
            var factory = provider.GetRequiredService<IServiceFactory>();

            Assert.Throws<InvalidOperationException>(() => { factory.Create<IPaymentService>(); });
        }

        [Fact]
        public void Create_ShouldResolve_Service_Correctly()
        {
            IPaymentService ps1 = _factory.Create<IPaymentService, FakePaymentServiceOne>();
            IPaymentService ps2 = _factory.Create<IPaymentService, FakePaymentServiceTwo>();

            int a1 = ps1.Pay(5);
            int a2 = ps2.Pay(5);

            Assert.NotNull(ps1);
            Assert.NotNull(ps2);

            Assert.Equal(5, a1);
            Assert.Equal(10, a2);
        }

        [Fact]
        public void Single_Service_Creation()
        {
            var sc = new ServiceCollection();
            sc.AddTransient<IPaymentService, FakePaymentServiceOne>();
            sc.AddSingleton<IServiceFactory, DefaultServiceFactory>();

            var provider = sc.BuildServiceProvider();
            var factory = provider.GetRequiredService<IServiceFactory>();

            var paymentService = factory.Create<IPaymentService>();
            var paymentServiceImp = factory.Create<IPaymentService, FakePaymentServiceOne>();
            Assert.NotNull(paymentService);
            Assert.IsType<FakePaymentServiceOne>(paymentService);
            Assert.NotNull(paymentServiceImp);
            Assert.IsType<FakePaymentServiceOne>(paymentServiceImp);
        }
    }
}