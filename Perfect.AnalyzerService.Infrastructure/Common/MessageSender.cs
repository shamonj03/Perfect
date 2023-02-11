using MassTransit;
using Perfect.AnalyzerService.Application.Common;

namespace Perfect.AnalyzerService.Infrastructure.Common
{
    public class MessageSender : IMessageSender
    {
        private readonly IPublishEndpoint _publishEndpointProvider;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public MessageSender(IPublishEndpoint publishEndpointProvider, ISendEndpointProvider sendEndpointProvider)
        {
            _publishEndpointProvider = publishEndpointProvider;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task PublishEventAsync<T>(T message, CancellationToken cancellationToken)
            where T : class
        {
            await _publishEndpointProvider.Publish(message, cancellationToken);
        }

        public async Task SendCommandAsync<T>(string uri, T message, CancellationToken cancellationToken)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(uri));
            }

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(uri));

            await endpoint.Send(message, cancellationToken);
        }
    }
}
