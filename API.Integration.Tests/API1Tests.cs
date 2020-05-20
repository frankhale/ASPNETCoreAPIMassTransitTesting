using FluentAssertions;
using MassTransit.Testing;
using Messages;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace API1.Integration.Tests
{
    public class API1Tests : IClassFixture<API1ServiceSetup<Startup>>
    {
        private readonly API1ServiceSetup<Startup> _factory;
        private HttpClient _client;

        public API1Tests(API1ServiceSetup<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true
            });
        }

        [Fact]
        public async Task Get_GetDefaultEndPoint_ReturnsOk()
        {
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<MyMessageConsumer>("mymessage-endpoint");

            await harness.Start();
            try
            {
                // Setting up the MassTransit Test Harness looks ok but I still have the issue
                // with setting up the WebApplicationFactory (see TestSetup.cs for more details)
                // https://masstransit-project.com/usage/testing.html#test-harness

                // Arrange
                // Act
                var apiResults = await _client.GetAsync("/api/default");
                var apiResponse = await apiResults.Content.ReadAsStringAsync();

                // Assert
                apiResults.StatusCode.Should().Be(HttpStatusCode.OK);
                apiResponse.Should().Be("API1");

                consumerHarness.Consumed.Select<MyMessage>().FirstOrDefault().Should().NotBeNull();
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}
