namespace Perfect.Messages.Events
{
    public record FileReceivedEvent
    {
        public string FileName { get; init; } = string.Empty;
    }
}
