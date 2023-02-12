namespace Perfect.Messages.Events
{
    public record FileReceivedEvent
    {
        public Guid FileId { get; init; }
        public string FileName { get; init; } = string.Empty;
    }
}
