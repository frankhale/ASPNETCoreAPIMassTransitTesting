using GreenPipes;
using MassTransit;
using Messages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace API1.Integration.Tests
{
    public class API1ServiceSetup<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureServices(services =>
            {
                var transit = services.Where(d => d.ServiceType.Assembly.FullName.Contains("MassTransit")).ToList();
                if (transit != null)
                {
                    foreach (var t in transit)
                    {
                        services.Remove(t);
                    }
                }

                foreach(var hc in services.Where(x => x.ServiceType.FullName.Contains("HealthCheck")).ToList())
                {
                    services.Remove(hc);
                }

                // We need to replace the MassTransit config that is coming from the Startup 
                // from our API1.
                //
                // We want to configure MassTransit using an InMemory transport and be a
                // MyMessageConsumer so that we can intercept the message
                //
                // I don't know how to do that. I couldn't find any examples online to 
                // demonstrate how to properly do it.
                //
                // I tried doing a brute force remove of all services that's assembly started with 
                // MassTransit. Once I did that HealthChecks complained that I was configuring it twice.
                // 
                // I brute force removed HealthChecks and then I got an error saying the MassTransit bus
                // was already started. 

                // The help on Testing looks promising but I still don't know how to configure my WebApplicationFactory 
                // https://masstransit-project.com/usage/testing.html#test-harness

                services.AddMassTransit(x =>
                {
                    x.AddConsumer<MyMessageConsumer>();
                    x.AddBus(provider => Bus.Factory.CreateUsingInMemory(cfg =>
                    {
                        cfg.ReceiveEndpoint("mymessage-endpoint", ep =>
                        {
                            ep.ConfigureConsumer<MyMessageConsumer>(provider);
                        });
                    }));
                });
                services.AddMassTransitHostedService();
                services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            });
        }
    }
}

