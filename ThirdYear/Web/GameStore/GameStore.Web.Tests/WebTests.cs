using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Web.Tests
{
    [TestClass]
    public class WebTests
    {
        [TestMethod]
        public void ConfigureServices_RegistersDependenciesCorrectly()
        {
            Mock<IConfigurationSection> configurationSectionStub = new Mock<IConfigurationSection>();
            configurationSectionStub.Setup(x => x["DefaultConnection"]).Returns("TestConnectionString");
            Mock<IConfiguration> configurationStub = new Mock<IConfiguration>();
            configurationStub.Setup(x => x.GetSection("ConnectionStrings")).Returns(configurationSectionStub.Object);
            configurationStub.Setup(x => x.GetSection("ResourceVariables:UrlsForBoxPaymant"))
            .Returns(new ConfigurationSection(
                new ConfigurationRoot(
                new List<IConfigurationProvider>()), "ResourceVariables:UrlsForBoxPaymant"));
            IServiceCollection services = new ServiceCollection();
            var target = new Startup(configurationStub.Object);

            target.ConfigureServices(services);

            services.AddTransient<TestController>();

            var serviceProvider = services.BuildServiceProvider();

            var controller = serviceProvider.GetService<TestController>();
            Assert.IsNotNull(controller);
        }

        public class TestController
        {
        }
    }
}
