using MassTransit;
using Perfect.Messages.Events;

namespace Perfect.SagaService.Host.Consumers
{
    internal class FileReceivedConsumer : IConsumer<FileReceivedEvent>
    {
        public Task Consume(ConsumeContext<FileReceivedEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
