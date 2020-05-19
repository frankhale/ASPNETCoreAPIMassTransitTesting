using MassTransit;
using System;
using System.Threading.Tasks;

namespace Messages
{
    public class MyMessage
    {
        public string Value { get; set; }
    }
        
    public class MyMessageConsumer : IConsumer<MyMessage>
    {
        public Task Consume(ConsumeContext<MyMessage> context)
        {
            Console.WriteLine(context.Message.Value);
            return Task.CompletedTask;
        }
    }
}
