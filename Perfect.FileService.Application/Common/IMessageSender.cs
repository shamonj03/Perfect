namespace Perfect.FileService.Application.Common
{
    public interface IMessageSender
    {
        Task PublishEventAsync<T>(T message, CancellationToken cancellationToken)
            where T : class;

        Task SendCommandAsync<T>(string uri, T message, CancellationToken cancellationToken)
            where T : class;
    }
}
