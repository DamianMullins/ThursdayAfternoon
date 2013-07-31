using Nancy;
using NUnit.Framework;
using Nancy.Testing;
using ThursdayAfternoon.Nancy;
using ThursdayAfternoon.Nancy.Modules;

namespace ThursdayAfternoon.Tests.Modules
{
    [TestFixture]
    public class IndexModuleTests
    {
        private readonly Bootstrapper _bootstrapper;

        public IndexModuleTests()
        {
            _bootstrapper = new Bootstrapper();
        }

        [Test]
        public void Should_return_status_ok()
        {
            // Arrange
            var bootstrapper = new ConfigurableBootstrapper(with => with.Module<IndexModule>());
            var browser = new Browser(bootstrapper);

            // Act
            var result = browser.Get("/", with => with.HttpRequest());

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
