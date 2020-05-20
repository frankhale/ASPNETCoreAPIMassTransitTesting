using FluentAssertions;
using MassTransit;
using Messages;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace API1.Integration.Tests
{
    public class API1Tests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client;
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly FakeMassTransitBus _fakeBus;

        public API1Tests(WebApplicationFactory<Startup> factory)
        {
            _fakeBus = new FakeMassTransitBus();

            _factory = factory.WithWebHostBuilder(config =>
            {
                config.ConfigureServices(services =>
                {
                    services.AddSingleton<IBus>(_fakeBus);
                });
            });

            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true
            });
        }

        [Fact]
        public async Task Get_GetDefaultEndPoint_ReturnsOk()
        {
            // Arrange
            // Act
            var apiResults = await _client.GetAsync("/api/default");
            var apiResponse = await apiResults.Content.ReadAsStringAsync();

            // Assert
            apiResults.StatusCode.Should().Be(HttpStatusCode.OK);
            apiResponse.Should().Be("API1");

            _fakeBus.Messages.Count().Should().Be(1);
            _fakeBus.Messages.FirstOrDefault().As<MyMessage>().Value.Should().Be("Message sent from API1");

            _fakeBus.ClearMessages();
        }
    }
}
