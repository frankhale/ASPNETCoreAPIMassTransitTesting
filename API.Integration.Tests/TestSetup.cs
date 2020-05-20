using GreenPipes;
using MassTransit;
using MassTransit.EndpointConfigurators;
using MassTransit.Topology;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API1.Integration.Tests
{
    public class FakeMassTransitBus : IBusControl
    {
        public List<object> Messages { get; } = new List<object>();

        public void ClearMessages()
        {
            Messages.Clear();
        }

        public Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : class
        {
            Messages.Add(message);
            return Task.CompletedTask;
        }

        #region YO HOLMES, FAKE YOU LATER...
        public Uri Address => throw new NotImplementedException();

        public IBusTopology Topology => throw new NotImplementedException();

        public ConnectHandle ConnectConsumeMessageObserver<T>(IConsumeMessageObserver<T> observer) where T : class
        {
            throw new NotImplementedException();
        }

        public ConnectHandle ConnectConsumeObserver(IConsumeObserver observer)
        {
            throw new NotImplementedException();
        }

        public ConnectHandle ConnectConsumePipe<T>(IPipe<ConsumeContext<T>> pipe) where T : class
        {
            throw new NotImplementedException();
        }

        public ConnectHandle ConnectEndpointConfigurationObserver(IEndpointConfigurationObserver observer)
        {
            throw new NotImplementedException();
        }

        public ConnectHandle ConnectPublishObserver(IPublishObserver observer)
        {
            throw new NotImplementedException();
        }

        public HostReceiveEndpointHandle ConnectReceiveEndpoint(IEndpointDefinition definition, IEndpointNameFormatter endpointNameFormatter, Action<IReceiveEndpointConfigurator> configureEndpoint = null)
        {
            throw new NotImplementedException();
        }

        public HostReceiveEndpointHandle ConnectReceiveEndpoint(string queueName, Action<IReceiveEndpointConfigurator> configureEndpoint)
        {
            throw new NotImplementedException();
        }

        public ConnectHandle ConnectReceiveEndpointObserver(IReceiveEndpointObserver observer)
        {
            throw new NotImplementedException();
        }

        public ConnectHandle ConnectReceiveObserver(IReceiveObserver observer)
        {
            throw new NotImplementedException();
        }

        public ConnectHandle ConnectRequestPipe<T>(Guid requestId, IPipe<ConsumeContext<T>> pipe) where T : class
        {
            throw new NotImplementedException();
        }

        public ConnectHandle ConnectSendObserver(ISendObserver observer)
        {
            throw new NotImplementedException();
        }

        public Task<ISendEndpoint> GetPublishSendEndpoint<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public Task<ISendEndpoint> GetSendEndpoint(Uri address)
        {
            throw new NotImplementedException();
        }

        public void Probe(ProbeContext context)
        {
            throw new NotImplementedException();
        }

        public Task<BusHandle> StartAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Publish<T>(T message, IPipe<PublishContext<T>> publishPipe, CancellationToken cancellationToken = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task Publish<T>(T message, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task Publish(object message, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Publish(object message, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Publish(object message, Type messageType, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Publish(object message, Type messageType, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Publish<T>(object values, CancellationToken cancellationToken = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task Publish<T>(object values, IPipe<PublishContext<T>> publishPipe, CancellationToken cancellationToken = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task Publish<T>(object values, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default) where T : class
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
